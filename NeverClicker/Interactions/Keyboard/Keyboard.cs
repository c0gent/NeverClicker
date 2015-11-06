using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Keyboard {
		private const string DefaultSendMode = "SendInput";
		static string[] KeyModList = { "^", "!", "+" };

		public enum KeyMod {
			Ctrl,
			Alt,
			Shift,
			Win,
			None,
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
		
		// Can handle modifiers if they belong to the list below:
		public static void SendKey(Interactor intr, string key) {
			if (KeyModList.Any(key.Contains)) {
				SendKeyWithMod(intr, "", key, SendMode.Event);
			} else {
				SendInput(intr, "{ " + key + " }");
			}		
		}

		public static void SendKey(Interactor intr, string keyMod, string key) {
			SendKeyWithMod(intr, keyMod, key, SendMode.Event);
		}

		public static void KeyPress(Interactor intr, string key) {
			KeyPress(intr, key, 10);
		}

		public static void KeyPress(Interactor intr, string key, int duration) {
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
			intr.Wait(duration * 3);
		}

		// TryParseKeyMod(): Really cheesy, needs to do more checking, etc.
		public static bool TryParseKeyMod(Interactor intr, string keyIn, out string keyStr, out string keyModStr) {
			if (keyIn.Contains("^")) {
				keyModStr = "Ctrl";
				keyStr = keyIn.Substring(keyIn.IndexOf("^") + 1);
			} else if (keyIn.Contains("!")) {
				keyModStr = "Alt";
				keyStr = keyIn.Substring(keyIn.IndexOf("!") + 1);
			} else if (keyIn.Contains("+")) {
				keyModStr = "Shift";
				keyStr = keyIn.Substring(keyIn.IndexOf("+") + 1);
			} else {
				keyModStr = "";
				keyStr = keyIn;
				return false;
			}

			return true;
		}

		// SendKeyWithMod <<<<< TODO: HANDLE DOUBLE MODIFIER (CTRL+ALT+X) >>>>>
		//		- Really cheesy and basically just hopes the caller has a valid keymod and key;
		public static void SendKeyWithMod(Interactor intr, string keyModIn, string keyIn, SendMode sendMode) {
			var modeStr = ResolveSendMode(sendMode);

			string keyModStr;
			string keyStr;
			if (!TryParseKeyMod(intr, keyIn, out keyStr, out keyModStr)) {
				keyModStr = keyModIn;
				keyStr = keyIn;
			}

			var keySeq = "{" + keyModStr + " down}{" + keyStr + "}{" + keyModStr + " up}";

			intr.Log("SendKeyWithMod(): Executing: '"+ modeStr + keySeq + "' ...", LogEntryType.Debug);
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
