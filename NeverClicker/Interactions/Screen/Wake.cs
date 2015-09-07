using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	
	public static partial class Screen {
		[DllImport("user32.dll")]	
		static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		private const int SC_MONITORPOWER = 0xF170;
		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_CLOSE = 0x0F060;

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		public static void Wake(Interactor intr) {
			//intr.ExecuteStatement("PostMessage, 0x0112, 0xF170, -1,, A");
			// PostMessage, 0x0112, 0xF170, 2,, A 			; Turn off Display (-1 on, 1 low-pow, 2 off) -- unreliable
			//intr.ExecuteStatement("PostMessage, 0x0112, 0x0F060, 0,, A"); // ; 0x0112 is WM_SYSCOMMAND, 0x0F060 is SC_CLOSE -- turns off screensaver
			SendMessage(FindWindow(null, null), WM_SYSCOMMAND, SC_CLOSE, 0);
        }
	}
}




//[DllImport("user32.dll")]
//static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
 
//private int SC_MONITORPOWER = 0xF170;
//private int WM_SYSCOMMAND = 0x0112;
 
//enum MonitorState
//{
//    ON = -1,
//    OFF = 2,
//    STANDBY = 1
//}

	
//public void SetMonitorState(MonitorState state)
//{
//    SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, (int)state);
//}