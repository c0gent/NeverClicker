using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker.Interactions.Sequences {
	static class InvokeCycler {
		public static void Start(
					Interactions.Interactor interactor,
					IProgress<string> progress,
					CancellationToken cancelToken,
					GameClient.Instance Game
		) {
			progress.Report("Beginning AutoInvokeAsync.");
			Task.Delay(500).Wait();

			// alibEng.Exec("SendEvent {Click 1200, 500, 0}");
			// Task.Delay(1000).Wait();

			//alibEng.Exec("ActivateNeverwinter()");
			Interactions.Sequences.Old.OldActivateNeverwinter(interactor, progress, cancelToken);
			Task.Delay(4000).Wait();

			//alibEng.Exec("AutoInvokeAsync()");
			Old.OldAutoInvoke(interactor, progress, cancelToken);

			//LogAppend("[Auto-Activated Invocation Complete]")
			progress.Report("AutoInvokeAsync complete.");

			//if (Game.CurrentState == GameClient.State.InWorld) 
			//{

			//}

			//if (alibEng.ExecFunction("FindLoggedIn") != "0") {
			//    progress.Report("Currently logged in.");

			//    progress.Report("Logging out.");
			//    alibEng.Exec("Logout()");
			//    Task.Delay(5000).Wait();
			//}

			//if (FindEwButton())
			//{
			//    FindAndClick(CsloImageFile)
			//    Sleep 3000
			//}



			//          if (FindLoginButton())
			//          {
			//              FindAndClick(LsebImageFile)
			//              }

			//          IfWinExist ahk_class CrypticWindowClass
			//             {
			//              WinKill
			//             }

			//          IfWinExist ahk_class #32770
			//{
			//              WinKill
			//      }



			//          IfWinExist ahk_class #327707
			//{
			//              WinKill
			//      }

			//          exitapp 0

		}

	}
}
