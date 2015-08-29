using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Screen {

		public static bool WindowDetectExist(Interactor itr, string windowExe) {
			return WindowDetect(itr, windowExe, "Exist");
		}

		public static bool WindowDetectActive(Interactor itr, string windowExe) {
			return WindowDetect(itr, windowExe, "Active");
		}

		public static bool WindowDetect(Interactor itr, string windowExe, string flavor) {
			string detectionParam = string.Format("ahk_exe {0}", windowExe);
			var result = itr.EvaluateFunction("Win" + flavor, detectionParam);
			itr.Log("Interactions::Screen::WindowDetect" + flavor + "(): Win" + flavor + "(" + detectionParam + "): '" + result + "'", LogType.Detail);

			if ((result.Trim() == "0x0") || (string.IsNullOrWhiteSpace(result))) {
				return false;
			} else {
				return true;
			}
		}

		public static void WindowMinimize(Interactor itr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			itr.ExecuteStatement("WinMinimize, " + param);
        }

		public static void WindowActivate(Interactor itr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			itr.ExecuteStatement("WinActivate, " + param);
		}

		public static void WindowKill(Interactor itr, string windowExe) {
			string param = string.Format("ahk_exe {0}", windowExe);
			itr.ExecuteStatement("WinKill, " + param);
		}
	}

	//public enum WindowDetectionFlavor {
	//	Active,
	//	Exists,
	//}
}
