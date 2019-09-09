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

namespace EntranceSensorForm
{
    public partial class Form1 : Form
    {
        SerialTransport serialTransport;
        StringBuilder sb = new StringBuilder();
        int[] times = { 3, 5, 10, 30, 60, 180, 300};
        int reInitTime = 3;
        bool isSensorActive = false;
        public static int hWnd;
        Thread thread;
        bool exitFlag = false;
        Stopwatch stopwatch = new Stopwatch();

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
        
        public Form1()
        {
            InitializeComponent();
            optionComboBox.Items.AddRange(new object[] {"3초","5초","10초","30초","60초", "180초", "300초"});
            optionComboBox.SelectedIndex = 0;

            Init();
        }

        ~Form1()
        {

        }

        private void Init()
        {
            try
            {
                serialTransport = new SerialTransport();
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
            }

            //팟플레이어 찾기
            hWnd = FindWindow("PotPlayer", null);
            if(hWnd == 0)
            {
                while (hWnd == 0)
                {
                    var result = MessageBox.Show("팟플레이어를 킨 후 확인버튼을 누르세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        hWnd = FindWindow("PotPlayer", null);
                    }
                    //Todo : 경고창을 띄운 뒤 설치마법사의 다시시도처럼 켜질때까지 경고창이 띄워지게 설정하기
                }
            }

            thread = new Thread(new ThreadStart(VideoThread));
            thread.Start();
        }

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

        public void Log(string logText)
        {
            sb.AppendLine(logText);
            logTextBox.Text = sb.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitFlag = true;
            thread.Join();
        }
    }
}
