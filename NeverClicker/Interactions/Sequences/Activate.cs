using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void Activate(Interactions.Interactor itr, IProgress<string> log, CancellationToken cancelToken) {


		}
	}
}


//LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Beginning activation process.]")

//	Sleep 100
//	if (!(WinExist("ahk_class CrypticWindowClass") == "0x0")) {
//        LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client already open; bringing window to foreground.]")
//        WinActivate
//	} else {
//		patcherOpen := 0
//		gameClientOpen := 0

//		LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client not open.]")

//		IfWinExist ahk_class #32770
//		{
//	WinKill
//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'ahk_class #32770' (patcher)]")
//			patcherOpen = 0
//		}

//IfWinExist Neverwinter {
//	WinKill
//	LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'Neverwinter')]")
//			gameClientOpen = 0
//		}

//Sleep 1000
//		Sleep 1000
//		Sleep 1000
//		Sleep 1000
//		Sleep 1000

//		while (patcherOpen == 0) {
//			if (A_Index >= 20) { 
//				break 
//			}

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Launching patcher.]")
//            run %NwFolder%\Neverwinter.exe, %NwFolder%
//			Sleep 5000
			
//			if (FindPatcherOpen()) {
//                LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher already open; activating.]")
//                WinActivate
//				patcherOpen = 1
//                Sleep 8000
//			} else {
//				while (patcherOpen == 0) {
//                    LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, waiting 5 seconds then trying again.]")
//                    Sleep 5000
//					run %NwFolder%\Neverwinter.exe, %NwFolder%
//					Sleep 5000
					
//					if ((FindPatcherOpen() == 0) || (A_Index >= 10)) {
//                        LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher failed to launch after " . A_Index + 1 . " attempts.]")
//                        patcherOpen = 0
//						break
//					}
//				}
//			}
			
//			if (patcherOpen == 0) {
//                LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, restarting loop.]")
//				continue
//			}

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Entering UserName and Password.]")

//			Sleep 200
			
			
//			; FIXES SHIFT-STICKING ISSUE
//            Send {Shift down}
//			Sleep 200
//			Send {Shift up}
//			Sleep 200
			
			
//			; SHIFT-TAB, ENTER USER NAME, ENTER PASSWORD, HIT ENTER
//			Send { Shift down }
//Sleep 200
//			Send {Tab down}
//			Sleep 200
//			Send {Shift up}
//			Sleep 200
//			Send {Tab up}
//			Sleep 200
//			Send %NwUserName%
//			Sleep 200
//			Send {Tab}
//			Send %NwActPwd%
//			Sleep 1000
//			Send {enter}
//			Sleep 8000

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Attempting to hit 'Play' button.]")

//			; ATTEMPT TO SHIFT TAB TO THE PLAY BUTTON
//            Sleep 2000
//            SendEvent {Shift Tab}
//            Sleep 2000
//            SendEvent {enter}
//            Sleep 3000
			
//			an_logged_in := 0

//			CloseVerifyMsgbox()

//			Sleep 700

//			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Activating Game Client.]")
			
//			if (FindClientOpen()) {
//				continue
//			} else {
//				WinActivate
//			}
			
//			Sleep 900
//			SendEvent {Click 5, 5, 0}
//			Sleep 100
			
//			while ((!an_logged_in) && (A_Index< 100)) {
//				Sleep 5000
//				;WakeScreen()

//				; FIXES SHIFT-STICKING ISSUE
//				; Sleep 200
//				;SendEvent {Shift down}
//				;Sleep 100
//				;SendEvent {Shift up}
//				;Sleep 200
				
//				; FIRST SHIFT TAB
//				;SendEvent {Shift down}
//				;Sleep 150
//				;SendEvent {Tab down}
//				;Sleep 150
//				;SendEvent {Shift up}
//				;Sleep 150
//				;SendEvent {Tab up}
//				;Sleep 150
				
//				; SECOND SHIFT TAB
//				;SendEvent {Shift down}
//				;Sleep 150
//				;SendEvent {Tab down}
//				;Sleep 150
//				;SendEvent {Shift up}
//				;Sleep 150
//				;SendEvent {Tab up}
//				;Sleep 150
				
//				; PRESS ENTER(HOPEFULLY ON 'PLAY' BUTTON)
//                SendEvent {enter}

//				LogAppend("Launcher: Attempting to hit 'Play' button")
							
//				if (FindEWButton() || FindAndClick(PpbImageFile, 1, 0)) {
//					an_logged_in := 1
//				} else {
//                    LogAppend("Could Not Find -- FindAndClick(PpbImageFile) || FindEWButton()")
//				}
//			}
//			Sleep 6000
//		}
//		sleep 6000
//	}
//	sleep 6000