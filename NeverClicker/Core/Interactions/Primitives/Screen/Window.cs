using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Screen {

		public static bool WindowDetectExist(Interactor intr, string windowExe) {
			return WindowDetect(intr, windowExe, "Exist");
		}

		public static bool WindowDetectActive(Interactor intr, string windowExe) {
			return WindowDetect(intr, windowExe, "Active");
		}

		public static bool WindowDetect(Interactor intr, string windowExe, string flavor) {
			string detectionParam = string.Format("ahk_exe {0}", windowExe);
			var result = intr.EvaluateFunction("Win" + flavor, detectionParam);
			intr.Log(LogEntryType.Trace, "Interactions::Screen::WindowDetect{0}(): Win{0}({1}): '{2}'",
				flavor, detectionParam, result);

			if ((result.Trim() == "0x0") || (string.IsNullOrWhiteSpace(result))) {
				return false;
			} else {
				return true;
			}
		}

		public static void WindowRun(Interactor intr, string windowExePath) {
			string runFolder = Path.GetDirectoryName(windowExePath);
			intr.ExecuteStatement("Run, " + windowExePath + ", " + runFolder);
		}

		public static void WindowMinimize(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinMinimize, " + param);
        }

		public static void WindowActivate(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinActivate, " + param);
		}

		// Need to figure these two out and simplify...
		public static void WindowKill(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinClose, " + param);
			intr.ExecuteStatement("WinClose, " + windowExe);
			intr.ExecuteStatement("WinKill, " + param);
			intr.ExecuteStatement("WinKill, " + windowExe);
		}

		public static void WindowKillClass(Interactor intr, string windowClass) {
			string param = string.Format("ahk_class {0}", windowClass);
			intr.ExecuteStatement("WinClose, " + param);
			intr.ExecuteStatement("WinClose, " + windowClass);
			intr.ExecuteStatement("WinKill, " + param);
			intr.ExecuteStatement("WinKill, " + windowClass);
		}

		public static void WindowKillTitle(Interactor intr, string windowTitle) {
			string param = string.Format("ahk_class {0}", windowTitle);
			intr.ExecuteStatement("WinClose, " + param);
			intr.ExecuteStatement("WinClose, " + windowTitle);
			intr.ExecuteStatement("WinKill, " + param);
			intr.ExecuteStatement("WinKill, " + windowTitle);
		}
	}

	//public enum WindowDetectionFlavor {
	//	Active,
	//	Exists,
	//}
}
