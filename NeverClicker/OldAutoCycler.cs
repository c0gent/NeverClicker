using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker
{
    class OldAutoCycler
    {
        public static void Invoke(Alib.Interop.AlibEngine alibEng, IProgress<string> progress, CancellationToken cancelToken)
        {
            try
            {
                alibEng.ExecFunction("AutoLaunchInvoke");
            }
            catch
            {
                progress.Report("Error during AutoInvoke.");
            }
        }
    }
}
