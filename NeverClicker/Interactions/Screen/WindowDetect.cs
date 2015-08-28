using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Screen {

		public static bool WindowDetectExists(Interactor itr, string windowExe) {
			string detectionParam = String.Format("ahk_exe {0}", windowExe);
			var result = itr.EvaluateFunction("WinExist(" + detectionParam + ")");
			return result.Trim() != "0x0";
		}

		public static bool WindowDetectActive(Interactor itr, string windowExe) {
			string detectionParam = String.Format("ahk_exe {0}", windowExe);
			var result = itr.EvaluateFunction("WinActive", detectionParam);
			return result.Trim() != "0x0";
		}
	}
}
