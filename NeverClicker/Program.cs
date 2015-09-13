using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeverClicker.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace NeverClicker {
	static class Program {
		[STAThread]
		static void Main(string[] args) {
			string thisprocessname = Process.GetCurrentProcess().ProcessName;

			if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1) {
				return;
			}

			Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}	
}


// *** USE BELOW IF RUNNING AS ADMINISTRATOR DOESN'T WORK ***
//static class Program {
//	//private const uint BCM_SETSHIELD = 0x160C;
//	//[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
//	//[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
//	//private static extern Int32 SendMessage(IntPtr hWnd, uint Msg, Int32 wParam, IntPtr lParam);

//	[STAThread]
//	static void Main(string[] args) {
//		//if (string.IsNullOrEmpty((from o in args where o == "--engage" select o).FirstOrDefault())) {
//		//	var btnElevate = new Button();
//		//	btnElevate.FlatStyle = FlatStyle.System;

//		//	SendMessage(btnElevate.Handle, BCM_SETSHIELD, 0, (IntPtr)1);

//		//	var processInfo = new ProcessStartInfo();
//		//	processInfo.Verb = "runas";
//		//	processInfo.FileName = Application.ExecutablePath;
//		//	processInfo.Arguments = string.Join(" ", args.Concat(new[] { "--engage" }).ToArray());
//		//	try {
//		//		Process p = Process.Start(processInfo);
//		//		p.WaitForExit();
//		//	} catch (Win32Exception) {
//		//		//Do nothing. Probably the user cancelled the UAC window or provided invalid credentials.
//		//	}

//		//	Application.Exit();
//		//} else {
//		// MAIN
//		Application.EnableVisualStyles();
//		Application.SetCompatibleTextRenderingDefault(false);
//		Application.Run(new MainForm());
//		//test1();
//		//}
//	}
//}