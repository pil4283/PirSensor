using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntranceSensorForm
{
    public static class PotPlayerCmd
    {
        public static uint POT_COMMAND = 0x0400;
        public static uint POT_GET_VOLUME = 0x5000;

        public static uint POT_SET_VOLUME = 0x5001; // 0 ~ 100
        public static uint POT_GET_TOTAL_TIME = 0x5002; // ms unit
        public static uint POT_GET_PROGRESS_TIME = 0x5003; // ms unit
        public static uint POT_GET_CURRENT_TIME = 0x5004; // ms unit
        public static uint POT_SET_CURRENT_TIME = 0x5005; // ms unit
        public static uint POT_GET_PLAY_STATUS = 0x5006; // 0:Stopped, 1:Paused, 2:Running,  -1:Close
        //상태 설정
        public static uint POT_SET_PLAY_STATUS = 0x5007; // 0:Toggle, 1:Paused, 2:Running
        public static uint POT_SET_PLAY_ORDER = 0x5008; // 0:Prev, 1:Next
        //재생중인 동영상 닫기
        public static uint POT_SET_PLAY_CLOSE = 0x5009;
        //가상 키 보내기
        public static uint POT_SEND_VIRTUAL_KEY = 0x5010; // Virtual Key(VK_UP, VK_DOWN....)

        public static uint POT_GET_AVISYNTH_USE = 0x6000;
        public static uint POT_SET_AVISYNTH_USE = 0x6001; // 0: off, 1:on
        public static uint POT_GET_VAPOURSYNTH_USE = 0x6010;
        public static uint POT_SET_VAPOURSYNTH_USE = 0x6011; // 0: off, 1:on
        public static uint POT_GET_VIDEO_WIDTH = 0x6030;
        public static uint POT_GET_VIDEO_HEIGHT = 0x6031;
        public static uint POT_GET_VIDEO_FPS = 0x6032; // scale by 1000

        // String getting
        // Send(Post)Message(hWnd, POT_COMMAND, POT_GET_XXXXX, (WPARAM)ReceiveHWND);
        // then PotPlayer call SendMessage(ReceiveHWND, WM_COPY_DATA, string(utf8) data...
        // COPYDATASTRUCT::dwData is POT_GET_XXXXX
        public static uint POT_GET_AVISYNTH_SCRIPT = 0x6002;
        public static uint POT_GET_VAPOURSYNTH_SCRIPT = 0x6012;
        public static uint POT_GET_PLAYFILE_NAME = 0x6020;

        // String setting... Using WM_COPYDATA
        // COPYDATASTRUCT cds = { 0, };
        // cds.dwData = POT_SET_xxxxxxxx;
        // cds.cbData = urf8.GetLength();
        // cds.lpData = (void *)(LPCSTR)urf8;
        // SendMessage(hWnd, WM_COPYDATA, hwnd, (WPARAM)&cds); 
        public static uint POT_SET_AVISYNTH_SCRIPT = 0x6003;
        public static uint POT_SET_VAPOURSYNTH_SCRIPT = 0x6013;
        public static uint POT_SET_SHOW_MESSAGE = 0x6040;
    }
}
