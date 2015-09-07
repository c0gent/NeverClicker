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

			if (Game.DeterminePatcherState(intr) != PatcherState.None) {
				Screen.WindowKill(intr, Game.GAMEPATCHEREXE);
				if (!intr.WaitUntil(15, PatcherState.None, Game.IsPatcherState, PatcherKillFailure)) { return false; }
			}

			Screen.WindowRun(intr, Properties.Settings.Default.NeverwinterExePath);

			if (!intr.WaitUntil(90, GameState.Patcher, Game.IsGameState, PatcherRunFailure)) { return false; }

			intr.Wait(4000);
			Screen.WindowActivate(intr, Game.GAMEPATCHEREXE);
			intr.Wait(1000);

			if (!intr.WaitUntil(90, PatcherState.LogIn, Game.IsPatcherState, PatcherRunFailure)) { return false; }

			//intr.Wait(1000);
			//Screen.WindowActivate(intr, Game.GAMEPATCHEREXE);
			//intr.Wait(1000);

			Keyboard.SendEvent(intr, "{Shift down}{Tab}{Shift up}");
			//intr.Wait(200);
			
			Keyboard.SendInput(intr, "%NwUserName%");
			//intr.Wait(200);

			Keyboard.SendInput(intr, "{Tab}");
			//intr.Wait(200);

			Keyboard.SendInput(intr, "%NwActPwd%");
			//intr.Wait(200);

			Keyboard.SendInput(intr, "{Enter}");
			//intr.Wait(200);

			//Keyboard.SendEvent(intr, "{Shift down}{Tab}{Shift up}");
			//intr.Wait(200);

			if (!intr.WaitUntil(1800, PatcherState.PlayButton, Game.IsPatcherState, PatcherLogInFailure)) { return false; }

			Keyboard.SendEvent(intr, "{Shift down}{Tab}{Shift up}");
			//intr.Wait(200);
			Keyboard.SendEvent(intr, "{Shift down}{Tab}{Shift up}");
			//intr.Wait(200);

			Keyboard.SendInput(intr, "{Enter}");

			return intr.WaitUntil(60, ClientState.CharSelect, Game.IsClientState, ProduceClientState);
		}

		public static bool PatcherKillFailure<TState>(Interactor intr, TState state) {
			intr.Log("Failed to launch Patcher, unable to close existing process. Patcher state: " + state.ToString(), LogEntryType.Fatal);
			return false;
		}

		public static bool PatcherRunFailure<TState>(Interactor intr, TState state) {
			intr.Log("Failed to launch Patcher, login button not found. Patcher state: " + state.ToString(), LogEntryType.Fatal);
			return false;
		}

		public static bool PatcherLogInFailure<TState>(Interactor intr, TState state) {
			intr.Log("Failed to log in using patcher, play button not found. Patcher state: " + state.ToString(), LogEntryType.Fatal);
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