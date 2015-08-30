using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool ProcessCharacter(
					Interactor itr,
					uint charZeroIdx
		) {
			itr.Log("ProcessCharacter(): Processing character: " + charZeroIdx + ".", LogEntryType.Detail);
			if (Game.ProduceClientState(itr, ClientState.CharSelect)) {

				// <<<<< FIX THIS LATER - GET RID OF CharOneIdx >>>>>
				var charOneIdx = (charZeroIdx + 1).ToString();
				itr.Log("ProcessCharacter(): Invoking character: " + charZeroIdx + ".", LogEntryType.Detail);

				var cmdString = "EnterWorldInvoke(1, 0, " + charOneIdx + ", 0, 0, 5)";
                itr.Log(cmdString, LogEntryType.Detail);
				itr.ExecuteStatement(cmdString);

				itr.Wait(1000);
				itr.Log("ProcessCharacter(): Invocation complete", LogEntryType.Detail);

				return true;

			}

			return false;
		}

	}
}

//#	While((ToggleInv) && (LastCharacterInvoked<NwCharacterCount)) {
//##		CurrentCharacter := LastCharacterInvoked + 1
//##		char_label := "Character " . CurrentCharacter
//$		LoadSetting("MostRecentInvocationTime", tmp_fakedate, char_label, as_ini)
//$        LoadSetting("VaultPurchase", 7, char_label, as_ini)

//$		LogAppend("[***** TRACE: AUTOINVOKE(): ATTEMPTING TO DETERMINE CURRENT CLIENT STATE]")
 
		
//		if (FindEwButton()) {
//            LogAppend("[***** TRACE: AUTOINVOKE(): FindEwButton()]")


//			; ##### INVOCATION ENTRY POINT
//			invocation_result := EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
			
//			if (invocation_result == 1) {
//				FirstRun := 0
//				EwRetryAttempts := 0
//				LastCharacterInvoked := CurrentCharacter
//				LogAppend("[***** TRACE: AUTOINVOKE(): CHARACTER INVOCATION COMPLETE]")
//			} else {
//                LogAppend("[### AutoInvoke(): Fatal Error, EnterWorldInvoke() has failed. Character " . CurrentCharacter. " was not invoked. ###]")
//			}
			
//			continue
			
//		} else if (FindLoginButton() || FindConnLogin()) {				; EwButton not found AND either Login Button OR Conn Login is in view

//			LogAppend("[***** TRACE: AUTOINVOKE(): FindLoginButton() || FindConnLogin()]")

//			PatcherLogin()
//		} else if (FindLoggedIn()) {									; EwButton not found AND We are logged in

//			LogAppend("[***** TRACE: AUTOINVOKE(): FindLoggedIn()]")

//			MoveAround()
//            Logout()
//		} else {														; EwButton not found, not logged in, no login screens in view
//			LogAppend("AutoInvoke(): Unable to find Enter World, Login, Connect, or Play Buttons.")
//            LogAppend("AutoInvoke(): Cannot determine current client state. Trying again...")
			
//			if (EwRetryAttempts > 150) {									; Same as above plus we've been trying for 150 seconds now

//				LogAppend("[AutoInvoke(): EwRetryAttempts > 150, Checking CrashCheckRestart()]")
				
//				if (CrashCheckRestart()) {
//                    LogAppend("[AutoInvoke(): CrashCheckRestart() has restarted Neverwinter, continuing]")
//                    EwRetryAttempts := 0
//					continue
//				} else {
//                    LogAppend("[AutoInvoke(): No Crash Detected, Aborting.]")
//					return 0
//				}

//			}
//			EwRetryAttempts := EwRetryAttempts + 1

//			LogAppend("[***** TRACE: AUTOINVOKE(): SLEEPING FOR 1 SECOND]")
//            Sleep 1000
//		}
		
//	}