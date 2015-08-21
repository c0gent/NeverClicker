using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NeverClicker
{
    class AutomationEngine
    {
        private FormMain FormMain;
        //Alib.Interop.AlibEngine AlibEng;
        //String AlibAutoInvokePath = "A:\\NW_Common.ahk";
        AlibCzar AlibCzar = new AlibCzar();
        
        //Thread InputThread = null;
        Task MouseMoveTask = null;
        CancellationTokenSource MouseMoveCancelToken;
        CancellationTokenSource AutoInvokeCancelToken;
        //bool InputThreadSuspended = false;

        public AutomationEngine(FormMain form)
        {
            FormMain = form;

        }

        public void Log(string message)
        {
            FormMain.WriteTextBox(message);
            // ADD FILE LOGGING
        }

        private Progress<string> GetLogCallback()
        {
            return new Progress<string>(s => Log(s));
        }

        

        public string GetVar(string variable)
        {
            return AlibCzar.GetAlibEngine().GetVar(variable);
        }

        public string ExecuteFunctionTest(
            string functionName,
            string param1 = null,
            string param2 = null,
            string param3 = null,
            string param4 = null
        ) {
            //return AlibCzar.ExecFunction(functionName, param1, param2, param3, param4);
            return AlibCzar.GetAlibEngine().ExecFunction(functionName, param1, param2, param3, param4);            
        }

        public void Stop()
        {
            //if (InputThread != null)
            //{
            //    InputThread.Abort();
            //    InputThread = null;
            //    //WriteTextBox("Mouse movement stopped.");
            //    
            //}
            //progress.Report("Stopping automation...");
            this.Log("Stopping automation...");

            try
            {
                MouseMoveCancelToken.Cancel();
                AutoInvokeCancelToken.Cancel();
                //AlibCzar.GetAlibEngine().Suspend();
                //Task.Delay(1500).Wait();
                //AlibCzar.GetAlibEngine().Terminate();
            }
            catch
            {
                this.Log("Task cancellation queued...");
            }
            finally
            {
                AlibCzar.ReloadEngine();
                this.Log("AutoInvoke cancelled.");
                this.Log("Automation functions reloaded.");
            }

            
        }

        public async void AutoInvoke()
        {
            this.Log("AutoInvoke activated.");
            AutoInvokeCancelToken = new CancellationTokenSource();
            Progress<string> progress = this.GetLogCallback();

            await Task.Factory.StartNew(
                () => OldAutoCycler.Invoke(AlibCzar.GetAlibEngine(), progress, AutoInvokeCancelToken.Token),
                TaskCreationOptions.LongRunning
            );
        }

        public async void MoveMouse()
        {
            //Thread thread = new Thread(() => download(filename));
            if (MouseMoveTask == null)
            {
                //InputThread = new Thread(() => MoveMouseThread(AlibCzar.GetAlibEngine(), () =>
                //{
                //    //WriteTextBox("Mouse movement complete.");
                //    Log("Mouse movement complete.");
                //}
                //));

                try
                {
                    this.Log("Mouse movement activated.");
                    MouseMoveCancelToken = new CancellationTokenSource();
                    Progress<string> progress = this.GetLogCallback();

                    await Task.Factory.StartNew(
                        () => MouseMover.Move(AlibCzar.GetAlibEngine(), progress, MouseMoveCancelToken.Token),
                        TaskCreationOptions.LongRunning
                    );
                }
                catch
                {
                    this.Log("Mouse movement cancelled.");
                }

            }
            else
            {
                //if (InputThread.ThreadState == ThreadState.Suspended)
                //{
                //    InputThread.Resume();
                //}

                //WriteTextBox("Mouse movement resumed.");
                this.Log("Mouse movement resumed.");
                return;
            }

            //InputThread.Start();
            //WriteTextBox("Mouse movement activated.");

            //InputThread.Join();
            //InputThread = null;
        }



    }

}
