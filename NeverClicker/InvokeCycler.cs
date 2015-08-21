using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker
{
    class InvokeCycler
    {
        public static void Invoke(Alib.Interop.AlibEngine alibEng, IProgress<string> progress, CancellationToken cancelToken)
        {
            progress.Report("Beginning AutoInvoke.");
            Task.Delay(500).Wait();

            // alibEng.Exec("SendEvent {Click 1200, 500, 0}");
            // Task.Delay(1000).Wait();

            alibEng.Exec("ActivateNeverwinter()");       
            Task.Delay(4000).Wait();

            alibEng.Exec("AutoInvoke()");

            //LogAppend("[Auto-Activated Invocation Complete]")
            progress.Report("AutoInvoke complete.");

            if (alibEng.ExecFunction("FindLoggedIn") != "0") {
                progress.Report("Currently logged in.");

                progress.Report("Logging out.");
                alibEng.Exec("Logout()");
                Task.Delay(5000).Wait();
            }

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
