using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		//public static bool ActivateClient(Interactor intr) {
		//	//intr.ExecuteStatement("ActivateNeverwinter()");

		//	Screen.WindowActivate(intr, Game.GAMECLIENTEXE);

		//	intr.Wait(1000);

		//	return true;
		//}

		//public static bool LogOut(Interactor intr) {
		//	//intr.ExecuteStatement("Logout()");
		//	intr.Log("Logging out...", LogEntryType.Info);

		//	MoveAround(intr);

		//	Keyboard.SendKey(intr, "Enter");
		//	intr.Wait(50);

		//	Keyboard.Send(intr, "/gotocharacterselect");
		//	intr.Wait(100);

		//	Keyboard.SendKey(intr, "Enter");
		//	intr.Wait(100);

		//	intr.Wait(3000);

		//	return true;
		//}


		public static bool ClientSignIn(Interactor intr) {
			intr.Log("Signing in Client...", LogEntryType.Info);

			//intr.ExecuteStatement("ClientLogin()");

			intr.Wait(5000);

			ClearDialogues(intr);

			//if (!intr.WaitUntil(15, ClientState.LogIn, Game.IsClientState, null, 0)) { return false; }

			string gameUserName = intr.GameAccount.GetSettingOrEmpty("NwUserName", "NwAct");
			string gamePassword = intr.GameAccount.GetSettingOrEmpty("NwActPwd", "NwAct");

			intr.Wait(100);

			//var shiftHome = @"Send {Shift down}
			//	Sleep 20
			//	Send { Home down}
			//	Sleep 20
			//	Send { Shift up}
			//	Sleep 20
			//	Send { Home up}
			//	Sleep 20
			//";

			//intr.ExecuteStatement(shiftHome);

			Keyboard.SendKeyWithMod(intr, "Shift", "Home", Keyboard.SendMode.Input);

			Keyboard.Send(intr, gameUserName);

			Keyboard.SendKey(intr, "Tab"); 
			intr.Wait(200);

			Keyboard.Send(intr, gamePassword);
			intr.Wait(200);

			Keyboard.SendKey(intr, "Enter");

			intr.Wait(3000);

			ClearDialogues(intr);

			return true;

			//ClientLogin() {
			//	global
	
			//	While ((ToggleInv && !FindClientLoginButton()) && (A_Index < 10))  {
			//		LogAppend("[Attempting to find ClientLoginButton.]")
			//		sleep 1500
			//	}
	
			//	if (ToggleInv && !FindClientLoginButton()) {
			//			LogAppend("[Not sure if we found ClientLoginButton but continuing...]")
			//			; msgbox ClientLogin() Waited too long for Login Screen to appear. If you are at login screen please check or remake %Lb_ImageFile%.
			//	}
	
			//	if (ToggleInv) {
			//		Sleep 2000 + Ran(500)
			//		Send {Shift down}
			//		Sleep 20
			//		Send {Home down}
			//		Sleep 20
			//		Send {Shift up}
			//		Sleep 20
			//		Send {Home up}
			//		Sleep 20
			//		Send %NwUserName%
			//		Sleep 50
			//		Send {Tab}
			//		Sleep 200 + Ran(100)
			//		Send %NwActPwd%
			//		Sleep 200 + Ran(100)
			//		Send {Enter}
			//		Sleep AfterLoginDelay + Ran(120)
			//	}
			//}
		}


		public static void KillAll(Interactor intr) {
			intr.Log("Closing game client and/or patcher...", LogEntryType.Info);
			//intr.ExecuteStatement("VigilantlyCloseClientAndExit()");

			Screen.WindowKill(intr, "Neverwinter.exe");
			Screen.WindowKill(intr, "GameClient.exe");
			Screen.WindowKillClass(intr, "CrypticWindowClass");
			Screen.WindowKillClass(intr, "Neverwinter");

			//VigilantlyCloseClientAndExit() {
			//	global
			//	if (FindLoggedIn()) {
			//		Logout()
			//		Sleep 5000
			//	}
	
			//	if (FindEwButton()) {
			//		FindAndClick(Cslo_ImageFile)
			//		Sleep 3000
			//	}

			//	if (FindClientLoginButton()) {
			//		FindAndClick(Lseb_ImageFile)
			//	}
	
	
			//	; ##### SHUT THINGS DOWN
	
			//	IfWinExist ahk_class CrypticWindowClass
			//	{
			//		WinKill
			//	}
	
			//	IfWinExist ahk_class #32770
			//	{
			//		WinKill
			//	}		
	
			//	IfWinExist ahk_class #327707
			//	{
			//		WinKill
			//	}
	
			//	IfWinExist ahk_exe Neverwinter.exe
			//	{
			//		WinKill
			//	}
	
			//	IfWinExist ahk_exe GameClient.exe
			//	{
			//		WinKill
			//	}
	
				
			//	; PostMessage, 0x0112, 0xF170, 2,, A 			; Turn off Display (-1 on, 1 low-pow, 2 off)
	
			//	; exitapp 0
			//}
		}
	}
}
