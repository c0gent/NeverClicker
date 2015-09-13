using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Keyboard {
		private const string DefaultSendMode = "SendInput";

		public enum KeyMod {
			Ctrl,
			Alt,
			Shift,
			Win,
		}

		public enum SendMode {
			Input,
			Event,
		}

		public static string ResolveSendMode(SendMode mode) {
			switch (mode) {
				case SendMode.Input:
					return "SendInput ";
				case SendMode.Event:
					return "SendEvent ";
			}

			return DefaultSendMode;
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

		public static void SendKeyWithMod(Interactor intr, string keyMod, string key) {
			SendKeyWithMod(intr, keyMod, key, SendMode.Input);
		}

		// SendKeyWithMod <<<<< TODO: CREATE MODIFIER ENUM >>>>> <<<<< TODO: HANDLE DOUBLE MODIFIER (CTRL+ALT+X) >>>>>
		public static void SendKeyWithMod(Interactor intr, string keyMod, string key, SendMode sendMode) {
			var modeStr = ResolveSendMode(sendMode);
			var keySeq = "{" + keyMod + " down}{" + key + "}{" + keyMod + " up}";

			intr.ExecuteStatement(modeStr + keySeq);

			//SendEvent(intr, "{Shift down}{Tab}{Shift up}");

			//var seq = sendModeStr + @" { " + keyMod + @" down }
			//Sleep 60
			//" + sendModeStr + @" { " + key + @" down }
			//Sleep 60
			//" + sendModeStr + @" { " + key + @" up }
			//Sleep 60
			//" + sendModeStr + @" { " + keyMod + @" up }
			//Sleep 60
			//";

			//intr.ExecuteStatement(keySeq);
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
