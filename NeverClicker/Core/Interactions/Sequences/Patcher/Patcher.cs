using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		//Potentially split into ProducePatcherState & Sub-functions
		public static bool PatcherLogin<TState>(Interactor intr, TState state) {
			//intr.ExecuteStatement("ActivateNeverwinter()");

			string gameUserName = intr.AccountSettings.GetSettingValOr("accountName", "general", "");
			string gamePassword = intr.AccountSettings.GetSettingValOr("password", "general", "");

			if (gameUserName == "enter_user_name" || gameUserName == ""
					|| gamePassword == "enter_password" || gamePassword == "")
			{
				intr.Log(LogEntryType.Fatal, "Username and/or Password not configured properly. Please edit " +
					"NeverClicker_GameAccount.ini and enter them there.");
				intr.CancelSource.Cancel();
				return false;
			}

			Mouse.Move(intr, 0, 0);

			// If patcher is already open, close it:
			if (States.DeterminePatcherState(intr) != PatcherState.None) {
				Screen.WindowKill(intr, States.GAMEPATCHEREXE);
				if (!intr.WaitUntil(15, PatcherState.None, States.IsPatcherState, PatcherKillFailure, 0)) { return false; }
			}

			// Open patcher:
			Screen.WindowRun(intr, Properties.Settings.Default.NeverwinterExePath);
			if (!intr.WaitUntil(90, GameState.Patcher, States.IsGameState, PatcherRunFailure, 0)) { return false; }

			// Set focus on patcher:
			intr.Wait(4000);
			Screen.WindowActivate(intr, States.GAMEPATCHEREXE);
			intr.Wait(1000);

			// Make sure server is up:
			if (States.IsServerState(intr, ServerState.Down)) {
				intr.Log("Server is down. Waiting until it comes up...");
				//intr.Wait(1200000);
				//return false;

				// Check every 1min for 20min and return false (relaunching patcher) if unsuccessful.
				if (!intr.WaitUntil(1200, 60, ServerState.Up, States.IsServerState, PatcherServerFailure, 0)) { return false; }
			}

			// Wait for login button to appear:
			if (!intr.WaitUntil(90, PatcherState.LogIn, States.IsPatcherState, PatcherRunFailure, 0)) { return false; }

			// Set focus on patcher:
			Screen.WindowActivate(intr, States.GAMEPATCHEREXE);

			//while (intr.WaitUntil(10, PatcherState.LogIn, Game.IsPatcherState, null, 0)) {			
			//	//Keyboard.SendEvent(intr, "{Shift down}{Tab}{Shift up}");
			//	Keyboard.SendKeyWithMod(intr, "Shift", "Tab", Keyboard.SendMode.Event);			
			//	Keyboard.SendInput(intr, gameUserName);
			//	intr.Wait(500);
			//	Keyboard.SendInput(intr, "{Tab}");
			//	Keyboard.SendInput(intr, gamePassword);
			//	intr.Wait(500);
			//	Keyboard.SendInput(intr, "{Enter}");
			//	intr.Wait(10000);
			//}

			intr.Wait(500);
			Keyboard.SendKeyWithMod(intr, "Shift", "Tab", Keyboard.SendMode.Event);			
			Keyboard.SendInput(intr, gameUserName);
			intr.Wait(500);
			Keyboard.SendInput(intr, "{Tab}");
			Keyboard.SendInput(intr, gamePassword);
			intr.Wait(500);
			Keyboard.SendInput(intr, "{Enter}");
			intr.Wait(10000);

			Screen.WindowKillTitle(intr, "Verify?");

			// // Set focus on patcher:
			//Screen.WindowActivate(intr, Game.GAMEPATCHEREXE);

			if (!intr.WaitUntil(1800, PatcherState.PlayButton, States.IsPatcherState, PatcherLogInFailure, 0)) { return false; }

			//intr.Wait(1000);
			//Keyboard.SendKeyWithMod(intr, "Shift", "Tab", Keyboard.SendMode.Event);
			//intr.Wait(300);
			//Keyboard.SendKeyWithMod(intr, "Shift", "Tab", Keyboard.SendMode.Event);
			//Keyboard.SendInput(intr, "{Enter}");

			// Click play button image (may not be as reliable as above): 
			Mouse.ClickImage(intr, "PatcherPlayButton");

			return intr.WaitUntil(60, ClientState.CharSelect, States.IsClientState, ProduceClientState, 0);
		}

		public static bool PatcherKillFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log(LogEntryType.Error, "Failed to launch Patcher, unable to close existing process. Patcher state: " + state.ToString());
			return false;
		}

		public static bool PatcherRunFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log(LogEntryType.Error, "Failed to launch Patcher, login button not found. Patcher state: " + state.ToString());
			return false;
		}

		public static bool PatcherLogInFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log(LogEntryType.Error, "Failed to log in using patcher, play button not found. Patcher state: " + state.ToString());
			return false;
		}

		public static bool PatcherServerFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log(LogEntryType.Error, "Failed to log in using patcher, server status down. Patcher state: " + state.ToString());
			return false;
		}
	}
}



//ActivateNeverwinter() {
//	global

//	LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Beginning activation process.]")

//	Sleep 100
//    if (!(WinExist("ahk_class CrypticWindowClass") == "0x0")) {
//		LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client already open; bringing window to foreground.]")
//        WinActivate
//    } else {
//	patcherOpen:= 0
//        gameClientOpen:= 0

//		LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client not open.]")

//		IfWinExist ahk_class #32770
//		{
//			WinKill
//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'ahk_class #32770' (patcher)]")
//            patcherOpen = 0
//        }

//		IfWinExist Neverwinter
//        {
//			WinKill
//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'Neverwinter')]")
//            gameClientOpen = 0
//        }

//		Sleep 1000
//        Sleep 1000
//        Sleep 1000
//        Sleep 1000
//        Sleep 1000

//		while (patcherOpen == 0) {
//			if (A_Index >= 20) {
//				break
//            }

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Launching patcher.]")
//            run % NwFolder %\Neverwinter.exe, % NwFolder %
//			  Sleep 5000

//			if (FindPatcherOpen()) {
//				LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher open, activating.]")
//                WinActivate
//				patcherOpen = 1

//				while (!FindPatcherLoginButton()) {
//					Sleep 5000
//                    if (FindPatcherOpen()) {
//						WinActivate
//                    } else {
//						patcherOpen = 0
//                        break
//                    }


//					if (A_Index >= 15) {
//						LogAppend("[ACTIVATENEVERWINTER(): Failed to find patcher login button after ".A_Index + 1. " attempts.]")
//                        patcherOpen = 0
//                        break
//                    }
//				}

//			} else {
//				while (patcherOpen == 0) {
//					LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, waiting 5 seconds then trying again.]")
//                    Sleep 5000
//                    run % NwFolder %\Neverwinter.exe, % NwFolder %
//					  Sleep 5000

//					if ((FindPatcherOpen() == 0) || (A_Index >= 10)) {
//						LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher failed to launch after ".A_Index + 1. " attempts.]")
//                        patcherOpen = 0
//                        return
//                    }
//				}
//			}


//			if (patcherOpen == 0) {
//				LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, restarting loop.]")
//                continue
//            }

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Entering UserName and Password.]")

//			Sleep 200


//			; FIXES SHIFT-STICKING ISSUE
//			Send { Shift down}
//			Sleep 200
//            Send { Shift up}
//			Sleep 200


//			; SHIFT - TAB, ENTER USER NAME, ENTER PASSWORD, HIT ENTER
//			  Send { Shift down}
//			Sleep 200
//            Send { Tab down}
//			Sleep 200
//            Send { Shift up}
//			Sleep 200
//            Send { Tab up}
//			Sleep 200
//            Send % NwUserName %
//			Sleep 200
//            Send { Tab}
//			Send % NwActPwd %
//			Sleep 1000
//            Send { enter}
//			Sleep 8000


//			// ***** NEED TO DETECT PATCHER PLAY BUTTON HERE *****

//			while (!FindPatcherPlayButton()) {
//				LogAppend("[Waiting for patcher play button to appear...]")
//                Sleep 10000

//				if (A_Index >= 60) {
//					LogAppend("[FAILURE: Patcher play button failed to appear after ". ((A_Index * 10) / 60). " minutes.]")
//                    return
//                }
//			}


//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Attempting to hit 'Play' button.]")

//			; ATTEMPT TO SHIFT TAB TO THE PLAY BUTTON
//            Sleep 2000
//            SendEvent { Shift Tab}
//			Sleep 2000
//            SendEvent { enter}
//			Sleep 3000

//			an_logged_in:= 0

//			CloseVerifyMsgbox()

//			Sleep 700

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Activating Game Client.]")

//			if (FindClientOpen()) {
//				WinActivate
//                continue
//            } else {
//				WinActivate
//            }

//			Sleep 900
//            SendEvent { Click 5, 5, 0}
//			Sleep 100

//			while ((!an_logged_in) && (A_Index < 100)) {
//				Sleep 5000
//				; WakeScreen()

//				 ; FIXES SHIFT-STICKING ISSUE
//				 ; Sleep 200
//				  ; SendEvent { Shift down}
//				; Sleep 100
//				 ; SendEvent { Shift up}
//				; Sleep 200

//				 ; FIRST SHIFT TAB
//				 ; SendEvent { Shift down}
//				; Sleep 150
//				 ; SendEvent { Tab down}
//				; Sleep 150
//				 ; SendEvent { Shift up}
//				; Sleep 150
//				 ; SendEvent { Tab up}
//				; Sleep 150

//				 ; SECOND SHIFT TAB
//				 ; SendEvent { Shift down}
//				; Sleep 150
//				 ; SendEvent { Tab down}
//				; Sleep 150
//				 ; SendEvent { Shift up}
//				; Sleep 150
//				 ; SendEvent { Tab up}
//				; Sleep 150

//				LogAppend("Launcher: Attempting to hit 'Play' button")

//				; PRESS ENTER (HOPEFULLY ON 'PLAY' BUTTON)
//				SendEvent { enter}
//				Sleep 200

//				if (FindClientOpen()) {
//					WinActivate
//                }

//				if (FindEWButton() || FindAndClick(Ppb_ImageFile, 1, 0)) {
//				an_logged_in:= 1
//                } else {
//					LogAppend("Could Not Find -- FindAndClick(Ppb_ImageFile) || FindEWButton()")
//                    if (FindClientLoginButton()) {
//						LogAppend("ATTEMPTING CLIENT LOGIN")
//                        ClientLogin()
//                    }
//				}
//			}
//			Sleep 6000
//        }
//		sleep 6000
//    }
//	sleep 6000
//}

//FindPatcherOpen() {
//	if ((WinExist("ahk_class #32770") == "0x0") || (WinExist("ahk_exe Neverwinter.exe") == "0x0")) {
//		return 0
//    } else {
//		return 1
//    }
//}