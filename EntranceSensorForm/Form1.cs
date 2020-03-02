using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OscJack;

namespace EntranceSensorForm
{
    public partial class Form1 : Form
    {
        #region OSC

        OscClient oscClient;
        _PropertyInfo propertyInfo;

        string[] ipAddress;
        int slaveCnt;
        int udpPort;
        string oscAddress;

		#endregion

		#region Variable

		int baudRate = 9600;
        string portName = "COM7";
        SerialTransport serialTransport;
        StringBuilder sb = new StringBuilder();

        int[] times = { 3, 5, 10, 30, 60, 180, 300};
        int reInitTime = 3;
        bool isSensorActive = false;
        public static int hWnd;
        Thread thread;
        bool exitFlag = false;
        Stopwatch stopwatch = new Stopwatch();

		#endregion

		#region WIN32API

		/// <summary>
		/// 특정 윈도우 찾기
		/// </summary>
		/// <returns>PID or 0(없으면)</returns>
		[DllImport("user32")]
        public static extern int FindWindow(string processName, string titleName);
        /// <summary>
        /// 특정 윈도우에 메세지 보내기
        /// </summary>
        [DllImport("user32")]
        public static extern int SendMessage(int HWND, uint u, uint wParam, long lParam);

        [DllImport("user32")]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint size, string lpFileName);

		#endregion

		public Form1()
        {
            // Todo : 사용자의 입력을 받을 필요가 없으므로 콤보박스 삭제, 시작하자마자 숨기기 OR 콘솔응용프로그램으로 새로 만들기
            // 콘솔응용프로그램에 윈도우창 Hide기능이 있는가?

            InitializeComponent();
            optionComboBox.Items.AddRange(new object[] {"3초","5초","10초","30초","60초", "180초", "300초"});
            optionComboBox.SelectedIndex = 0;

            GetConfig();
            Init();
        }

        ~Form1()
        {

        }

        void GetConfig()
        {
            GetPrivateProfileString("setting", "portname", "COM3", "", 1, "config.ini");
        }

        /// <summary>
        /// 센서와 팟플레이어 초기화
        /// </summary>
        private void Init()
        {
            try
            {
                serialTransport = new SerialTransport(baudRate, portName);
                serialTransport.serial.DataReceived += (sender, e) =>
                {
                    SerialPort port = (SerialPort)sender;
                    string data = port.ReadExisting();

                    if (data.Equals("1") && !stopwatch.IsRunning)
                    {
                        isSensorActive = true;
                        nextVideoLabel.Text = "O";
                        Log(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ":센서에 동작이 감지됨");
                        stopwatch.Start();
                    }
                };
            }
            catch(Exception e)
            {
                Log(e.Message);
                Log("센서와 연결 중 오류가 감지됨\nconfig.ini파일을 확인");
            }

            //OSC초기화
            ipAddress = new string[slaveCnt];

            // Todo : 팟플레이어가 아닌 MadMapper로 변경, 찾는 윈도우를 MadMapper로, 메세지박스는 삭제하고 1초마다 확인하게 바꾸기
            // 팟플레이어 찾기
            hWnd = FindWindow("PotPlayer", null);
            if(hWnd == 0)
            {
                while (hWnd == 0)
                {
                    var result = MessageBox.Show("팟플레이어가 꺼져있습니다.\n팟플레이어를 킨 후 확인버튼을 누르세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        hWnd = FindWindow("PotPlayer", null);
                    }
                }
            }
            thread = new Thread(new ThreadStart(VideoThread));
            thread.Start();
        }

        /// <summary>
        /// SlavePC에 OSC메세지를 보냄, 메세지를 받은 PC는 해당 커맨드에 맞는 행동을 취함
        /// (MadMapper에서 미리 정의한 작업 실행)
        /// </summary>
        public void SendOSCCommand()
        {
            // Todo : IP랑 포트, 커맨드등은 Config(ex)파일을 읽은 뒤 받아옴
        }

        /// <summary>
        /// 팟플레이어 사용안함, 보관용
        /// </summary>
        public  void VideoThread()
        {
            //종료전까지 쭉
            while(!exitFlag)
            {
                int videoStat = SendMessage(hWnd, PotPlayerCmd.POT_COMMAND, PotPlayerCmd.POT_GET_PLAY_STATUS, 0);
                
                if (videoStat != 2)
                {
                    videoStatLabel.Text = "정지";
                    //센서에 반응이 오고 재생중이 아닐떄 동영상을 재생시킴
                    if (isSensorActive && !stopwatch.IsRunning)
                    {
                        Log("비디오 재생");
                        isSensorActive = false;
                        SendMessage(hWnd, PotPlayerCmd.POT_COMMAND, PotPlayerCmd.POT_SET_PLAY_STATUS, 2);

                        nextVideoLabel.Text = "X";
                    }
                }
                else
                    videoStatLabel.Text = "재생중";
                Thread.Sleep(100);
                if(reInitTime * 1000 < stopwatch.ElapsedMilliseconds && stopwatch.IsRunning)
                {
                    stopwatch.Reset();
                }
            }
        }

        private void OptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            reInitTime = times[comboBox.SelectedIndex];
        }

        /// <summary>
        /// Form의 로그탭에서 로그출력
        /// Todo : 파일로 저장하기?
        /// </summary>
        /// <param name="logText"></param>
        public void Log(string logText)
        {
            sb.AppendLine(logText);
            logTextBox.Text = sb.ToString();
            logTextBox.Select(logTextBox.Text.Length, 0);
            logTextBox.ScrollToCaret();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitFlag = true;
            thread.Join();
        }
    }
}