using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker.Interactions.Sequences {
	class Old {
		public static void OldActivateNeverwinter(Interactor interactor, IProgress<string> progress, CancellationToken cancelToken) {
			interactor.InitOldAutoCyclerScript(progress);
			interactor.EvaluateFunction(interactor, progress, cancelToken, "ActivateNeverwinter");
			interactor.Reload();
		}

		public static void OldAutoInvoke(Interactor interactor, IProgress<string> progress, CancellationToken cancelToken) {
			interactor.InitOldAutoCyclerScript(progress);
			interactor.EvaluateFunction(interactor, progress, cancelToken, "AutoInvoke");
			interactor.Reload();
		}

		public static void OldAutoLaunchInvoke(Interactor interactor, IProgress<string> progress, CancellationToken cancelToken) {
			interactor.InitOldAutoCyclerScript(progress);
			interactor.EvaluateFunction(interactor, progress, cancelToken, "AutoLaunchInvoke");
			interactor.Reload();
		}
	}
}
