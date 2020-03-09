using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntranceSensorConsole
{
	public class ProgramManager
	{
		#region OSC

		OscSender[] oscSender;

		string[] ipAddress;
		int oscPort;
		string oscAddress;

		#endregion

		#region Variable

		int baudRate = 9600;
		string portName = "COM7";
		SerialTransport serialTransport;
		StringBuilder sb = new StringBuilder();

		bool isSensorActive = false;
		public static int hWnd;
		bool exitFlag = false;
		Thread thread;
		Stopwatch stopwatch = new Stopwatch();

		#endregion

		#region DLL
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;

		#endregion

		public ProgramManager()
		{
			Init();
		}

		/// <summary>
		/// if Config.ini is not exist, create Default ConfigFile
		/// </summary>
		void CreateDefaultConfig(string dir)
		{
			var ini = new IniFile();
			ini["SerialSetting"]["BaudRate"] = 9600;
			ini["SerialSetting"]["Port"] = "COM7";

			ini["IPSetting"]["PCCount"] = 3;
			ini["IPSetting"]["PC_0_IP"] = "192.168.0.1";
			ini["IPSetting"]["PC_1_IP"] = "192.168.0.2";
			ini["IPSetting"]["PC_2_IP"] = "192.168.0.3";
			ini["IPSetting"]["Port"] = 9000;

			ini["OSCSetting"]["OSCAddress"] = "/Sensor";
			ini.Save(dir);
			//var result = MessageBox.Show("Config.ini파일 생성완료\nC:/Davvero/Config.ini파일을 시스템 사양에 맞게 수정한 뒤 다시 실행하세요", "초기화 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Console.WriteLine("Config.ini파일 생성완료\nC:/Davvero/Config.ini파일을 시스템 사양에 맞게 수정한 뒤 다시 실행하세요");
			Environment.Exit(0);
		}

		void Init()
		{
			// 불러올 Config파일
			string dir = "C:\\Davvero\\Config.ini";

			// 처음 시작 시 기본값으로 Config파일 생성
			if (!File.Exists(dir))
			{
				CreateDefaultConfig(dir);
				return;
			}

			// OSC초기화
			var ini = new IniFile();

			ini.Load(dir);
			int slaveCnt = ini["IPSetting"]["PCCount"].ToInt();
			oscPort = ini["IPSetting"]["Port"].ToInt();
			oscAddress = ini["OSCSetting"]["OSCAddress"].ToString();
			//PC수 만큼 배열 지정
			ipAddress = new string[slaveCnt];
			oscSender = new OscSender[slaveCnt];

			for (int i = 0; i < slaveCnt; i++)
			{
				string pcName = string.Format("PC_{0}_IP", i);
				ipAddress[i] = ini["IPSetting"][pcName].ToString();
				oscSender[i] = new OscSender(ipAddress[i], oscPort, oscAddress);

				Log(ipAddress[i]);
			}

			// PIR센서 초기화
			while (serialTransport == null)
			{
				Thread.Sleep(1000);
				try
				{
					serialTransport = new SerialTransport(baudRate, portName);
					serialTransport.serial.DataReceived += (sender, e) =>
					{
						SerialPort port = (SerialPort)sender;
						string data = port.ReadExisting();

						// 움직임 감지 = 1, 비감지 0
						if (data.Equals("1") && !stopwatch.IsRunning)
						{
							isSensorActive = true;
							Log(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ":센서에 동작이 감지됨");
							stopwatch.Start();
						}
					};
				}
				catch (Exception e)
				{
					Console.Write(e.Message);
					Log(e.Message);
					Log("센서와 연결 중 오류가 감지됨\nconfig.ini파일을 확인");
				}
			}

			//StreamWriter log = new StreamWriter("C:\\Davvero\\Log.txt");
		}

		public void Log(string msg)
		{

		}
	}
}
