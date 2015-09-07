using System;
using System.Collections.Generic;
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
			intr.Log("Interactions::Screen::WindowDetect" + flavor + "(): Win" + flavor + "(" + detectionParam + "): '" + result + "'", LogEntryType.Debug);

			if ((result.Trim() == "0x0") || (string.IsNullOrWhiteSpace(result))) {
				return false;
			} else {
				return true;
			}
		}

		public static void WindowRun(Interactor intr, string windowExePath) {
			string param = string.Format("\"{0}\"", windowExePath);
			intr.ExecuteStatement("Run, " + param);
		}

		public static void WindowMinimize(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinMinimize, " + param);
        }

		public static void WindowActivate(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinActivate, " + param);
		}

		public static void WindowKill(Interactor intr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			intr.ExecuteStatement("WinKill, " + param);
		}
	}

	//public enum WindowDetectionFlavor {
	//	Active,
	//	Exists,
	//}
}
