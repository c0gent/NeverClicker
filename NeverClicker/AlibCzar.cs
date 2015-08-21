using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Alib.Interop;

namespace NeverClicker
{
    class AlibCzar
    {
        private AlibEngine AlibEng;
        String AlibAutoInvokeFileName = "A:\\NW_Common.ahk";
        static private uint MaxFileLoadAttempts = 5;

        public AlibCzar()
        {
            InitAlibEng();
        }

        private void InitAlibEng()
        {

            AlibEng = LoadFileOrDie(AlibAutoInvokeFileName);

            AlibEng.Exec("SetWorkingDir %A_ScriptDir%");
            AlibEng.Exec("A_ImagesDir = A:\\StandardImages");
            AlibEng.Exec("A_CommonDir = A:\\");
            AlibEng.Exec("NwFolder := \"C:\\Program Files (x86)\\Arc_Neverwinter\\Neverwinter_en\"");

            AlibEng.Exec("gcs_ini := A_CommonDir . \"\\nw_game_client_settings.ini\"");
            AlibEng.Exec("as_ini := A_CommonDir . \"\\nw_account_settings.ini\"");
            AlibEng.Exec("ai_log := A_CommonDir . \"\\nw_autoinvoke.log\"");

            AlibEng.Exec("^!=::Suspend");

            AlibEng.Exec("ToggleAfk := 0");
            AlibEng.Exec("ToggleMouseDragClick := 0");
            AlibEng.Exec("ToggleShit := 0");

            AlibEng.Exec("SendMode Input");
            AlibEng.Exec("CoordMode, Mouse, Screen");
            AlibEng.Exec("CoordMode, Pixel, Screen");
            AlibEng.Exec("SetMouseDelay, 55");
            AlibEng.Exec("SetKeyDelay, 55, 15");

            AlibEng.ExecFunction("init");

        }

        private AlibEngine LoadFileOrDie(string fileName)
        {
            AlibEngine alibEng = new Alib.Interop.AlibEngine();

            for (uint i = 0; i < MaxFileLoadAttempts; i++)
            {
                try
                {
                    alibEng.AddFile(AlibAutoInvokeFileName);
                }
                catch
                {
                    Console.WriteLine("Alib file contains errors or has loaded poorly. .");
                    alibEng = new AlibEngine();
                    continue;
                }

                if (i == (MaxFileLoadAttempts - 1))
                {
                    throw new Exception(String.Format("Failed to load AlibFile: {0}", fileName));
                }

                break;
            }



            return alibEng;
        }


        public void ReloadEngine()
        {
            AlibEng.Suspend();
            Task.Delay(1500).Wait();
            AlibEng.Terminate();

            InitAlibEng();
        }

        //public string ExecFunction(
        //    string functionName,
        //    string param1 = null,
        //    string param2 = null,
        //    string param3 = null,
        //    string param4 = null,
        //    string param5 = null,
        //    string param6 = null,
        //    string param7 = null,
        //    string param8 = null,
        //    string param9 = null,
        //    string param10 = null)
        //{
        //    return AlibEng.ExecFunction(functionName, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
        //}

        public AlibEngine GetAlibEngine()
        {
            return this.AlibEng;
        }

    }
}
