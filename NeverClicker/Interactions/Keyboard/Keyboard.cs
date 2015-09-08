using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Keyboard {
		public enum KeyMod {
			Ctrl,
			Alt,
			Shift,
			Win,
		}

		public static void Send(Interactor intr, string key) {
			SendInput(intr, key);
		}

		public static void SendKey(Interactor intr, string key) {
			SendInput(intr, "{ " + key + " }");
		}

		public static void KeyPress(Interactor intr, string key, uint duration) {
			//intr.ExecuteStatement("SendInput { " + key + " down }");
			//intr.Wait((int)duration + 180);
			//intr.ExecuteStatement("SendInput { " + key + " up }");
			//intr.Wait(20);

			string cmd = @"Send {" + key + @" down}
			Sleep " + duration.ToString() + @"
			Send {" + key + @" up}
			Sleep " + duration.ToString() + @"
			";

			intr.ExecuteStatement(cmd);
			intr.Wait((int)duration * 3);
		}

		// SendKeyWithMod <<<<< TODO: CREATE MODIFIER ENUM >>>>> <<<<< TODO: HANDLE DOUBLE MODIFIER (CTRL+ALT+X) >>>>>
		public static void SendKeyWithMod(Interactor intr, string key1, string keyMod) {			
			var seq = @"Send { " + keyMod + @" down }
			Sleep 20
			Send { " + key1 + @" down }
			Sleep 20
			Send { " + keyMod + @" up }
			Sleep 20
			Send { " + key1 + @" up }
			Sleep 20
			";

			intr.ExecuteStatement(seq);
		}

		public static void SendInput(Interactor intr, string key) {
			//intr.Wait(200);
			intr.ExecuteStatement("SendInput " + key);
			intr.Wait(20);
		}

		public static void SendPlay(Interactor intr, string keys) {
			intr.ExecuteStatement("SendPlay " + keys);
			intr.Wait(20);
		}

		public static void SendEvent(Interactor intr, string keys) {
			intr.ExecuteStatement("SendEvent " + keys);
			intr.Wait(20);
		}

		public static void SendTest(Interactor intr, string key) {
			intr.Wait(3000);
			SendInput(intr, key);
		}
	}
}
