using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker
{
    class MouseMover
    {
        public async static void Move(Alib.Interop.AlibEngine alibEng, IProgress<string> progress, CancellationToken cancelToken)
        {
            for (uint i = 0; i < 3; i++)
            {
                //WriteTextBox("Moving to (1, 1).");
                progress.Report("Moving to (1, 1).");
                alibEng.Exec("SendEvent {Click 1, 1, 0}");
                //alibEng.Exec("Sleep 3000");
                //Thread.Sleep(2000);                
                await Task.Delay(3000);
                //cancelToken.ThrowIfCancellationRequested();
                if (cancelToken.IsCancellationRequested) { break; }
                //WriteTextBox("Moving to (800, 800).");
                progress.Report("Moving to (800, 800).");
                alibEng.Exec("SendEvent {Click 800, 800, 0}");
                //alibEng.Exec("Sleep 3000");
                //Thread.Sleep(2000);
                await Task.Delay(3000);
                //cancelToken.ThrowIfCancellationRequested();
                if (cancelToken.IsCancellationRequested) { break; }
            }

            progress.Report("Mouse movement complete.");

        }
    }
}
