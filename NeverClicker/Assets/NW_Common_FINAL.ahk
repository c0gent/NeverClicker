;originalAuthor:	c0gent
;modifiedBy:		c0gent, 
;version:			1.09
;license:			http://creativecommons.org/licenses/by-nc-sa/3.0/us/

/* Release Notes


1.9.9: TRANSITION TO NEVERCLICKER: THIS SCRIPT IS BEING DEPRICATED AND MAY NO LONGER WORK ON ITS OWN

1.2.3 Add CharacterSelectScrollBarTopX And CharacterSelectScrollBarTopY Settings

1.2.2: Add VisibleCharacterSelectSlots Setting

1.2.1: Tweaks to Invocation

1.2.0: Elemental Evil
- Updated Vault of Piety redemption
	- Default item is now 5 (Artifact Equipment Coffer)

1.11:
- Added SlotPower() - Ability to automatically slot any power in any position using text identifiers
	-Example:
	
		^!0::
			powersWindowState(1)
			SlotPower("ENTF", "E3")
			SlotPower("SHLD", "E0")
			SlotPower("ROEF", "E2")
			SlotPower("ICRY", "E1")
			powersWindowState(0)
		return
		
	-Powers can be added by by including additional if else statements within the SlotPower() function
		

1.10: Well of Dragons
- Minor fixes to images

1.09: Tyranny of Dragons Patch
-Change the way characters are selected to due to changes in the character selection screen
	-Replace mouse wheel scrolling with click-dragging scroll bar when moving to top of list
	-Press enter key instead of clicking enter world button
	-Change logic to account for shorter character selection list
-Check for and dismiss "Ok" or "Okay" dialogue boxes which can pop up after character login
-Reduce number of times portable altars are searched for when invocation is not possible
-Two new image files added
-KNOWN ISSUE: Certain rare dialogue box combinations after logging in throw off timing of 
	invocation sequence however do not appear to cause terminal failure
-KNOWN ISSUE: When typing commands (such as /gotocharacterselect) additional delay is now
	required and will need to be tested on more systems

1.08.1:
-Bux Fixes related to WinExist() when used in if statements (checking for "0x0" now)
-Possible fix for the mystery "0"s (were possibly coming from SendHotkey())

1.08:
-Automatically type username as well as password to both patcher and in-client login screen
-Fixed Auto Keybinding loading code

1.07.6:
-Change Logout() to use /gotocharacterselect to alleviate the need for user to set logout hotkey
-Modify ActivateNeverwinter sequence to accommodate users with the Fast Launch option set to true on the Neverwinter Patcher

1.07.5:
-Change default hotkeys

1.07.4:
-Added more redundancy to altar detection and deployment to deal with weird sporadic timing variances

1.07.3:
-Minor Timing Adjustments to fix potential snags

1.07.2:
-Bug Fixes

1.07.1:
-Clean up logging
-Improve Full Automation

1.07:
-Full Automation. A script which simply calls AutoLaunchInvoke() from the 
auto-execute section (top of script) can be run from windows task scheduler.
	Example Script:
		A_ImagesDir = %A_ScriptDir%\Images
		A_CommonDir = %A_ScriptDir%\Common
		#include %A_ScriptDir%\Common\NW_Common.ahk

		AutoLaunchInvoke()
		
1.06:
-Automatically purchase Coffer of Wonderous Augmentation when coins are 7/7

1.05:
-Add automatic portable Altar Deployment
-Add per-character invocation information to .ini

1.04:
-Move Settings to .ini file
-Allow Keybinds to include special keys (alt, f4, etc)

1.03:
-Add Ability to automatically redeem Celestial Coins to buy Elixirs of Fate

1.02:
-Using ImageSearch() reduce failures due to timing and connection problems

1.01:
-Initial release
-Automated character invocation

*/


#MaxThreadsPerHotkey 2
#NoEnv
SendMode Input 
CoordMode, Mouse, Screen
CoordMode, Pixel, Screen
SetMouseDelay, 25
SetKeyDelay, 25, 15
SetBatchLines -1
ListLines Off

;==========================================================
;===================== VARIABLE INIT ======================
;=============== DO NOT EDIT THIS SECTION =================
;============ EDIT .INI FILE TO MAKE CHANGES ==============
;==========================================================

TestFunction1(extra := 0) {
	return 1 + extra
}

TestFunction2(extra := 0) {
	return 2 + extra
}

TestFunctionOne(extra := "") {
	return "One " . extra
}

ConfirmInvoke() {
	return 0
}

TestDetect() {
	return WinExist("ahk_exe devenv.exe")
}

LoadSetting(param, default, pgroup := "Other", ini_file := 0) {
	global
	if (!ini_file) {
		ini_file := gcs_ini
	}
	IniRead, tmp_var, %ini_file%, %pgroup%, %param%
		if ((tmp_var == "ERROR") || (CreateNewIniFiles == 1)) {
			IniWrite, %default%, %ini_file%, %pgroup%, %param%
			IniRead, %param%, %ini_file%, %pgroup%, %param%
		} else {
			%param% = %tmp_var%
		}
}

Init() {
	global
	
	CreateNewIniFiles := 0
	
	; gcs_ini := A_CommonDir . "\nw_game_client_settings.ini"
	; as_ini := A_CommonDir . "\nw_account_settings.ini"	
	; ai_log := A_CommonDir . "\nw_autoinvoke.log"

	gcs_ini := A_SettingsDir . "\NeverClicker_GameClient.ini"
	as_ini := A_SettingsDir . "\NeverClicker_GameAccount.ini"	
	ai_log := A_LogsDir . "\NeverClicker_Log_Ahk.txt"
	
	#IF 1
		if (!FileExist(gcs_ini) && !FileExist(as_ini)) {
			CreateNewIniFiles := 1
			msgbox About to create new settings files: "%gcs_ini%" and "%as_ini%". You will need to customize files for them to work (see readme). Exit script now to abort.
		}
	#IF

	;--- In-Game Hotkeys ---
	LoadSetting("NwInvokeKey", "^i", "GameHotkeys")
	; LoadSetting("NwLogoutKey", "0", "GameHotkeys")
	LoadSetting("NwMoveLeftKey", "a", "GameHotkeys")
	LoadSetting("NwMoveRightKey", "d", "GameHotkeys")
	LoadSetting("NwCursorMode", "ALT", "GameHotkeys")
	LoadSetting("NwInventoryKey", "i", "GameHotkeys")

	;--- Total Number of Characters on Account ---
	LoadSetting("NwCharacterCount", 10, "NwAct", as_ini)
	LoadSetting("NwActPwd","Enter_Password_Here_No_Quotes", "NwAct", as_ini)
	LoadSetting("NwUserName","Neverwinter_E-mail_or_Account_Name", "NwAct", as_ini)

	;--- Key Bindings File Name ---
	LoadSetting("NwBindUiFilePre", "my_nw_save", "KeyBindAndUi")
	NwBindFileName := NwBindUiFilePre . ".bind"
	NwUiFileName := NwBindUiFilePre . ".ui"

	;--- Automatic bind/ui load ---
	;--- Be sure to disable this if you use a different configuration on each character ---
	LoadSetting("AutoUiBindLoad", 0, "KeyBindAndUi")

	;--- Inventory Bag Width Slots ---
	LoadSetting("InventoryBagWidthSlots", 6, "ClickLocations")

	;--- Visible Character Select Slots
	LoadSetting("VisibleCharacterSelectSlots", 8, "KeyBindAndUi")

	LoadSetting("CharacterSelectScrollBarTopX", 840, "KeyBindAndUi")
	LoadSetting("CharacterSelectScrollBarTopY", 86, "KeyBindAndUi")

	;--- Center of Enter World Button Coordinates ---
	LoadSetting("EwButtonX", 1745, "ClickLocations")
	LoadSetting("EwButtonY", 967, "ClickLocations")

	;--- Center of Logout Confirmation OK Button Coordinates ---
	LoadSetting("LoButtonX", 1026, "ClickLocations")
	LoadSetting("LoButtonY", 593, "ClickLocations")

	;--- Center of Character Slot Button Coordinates (MUST CHECK)---
	LoadSetting("CharSlotX", 680, "ClickLocations")	; X-coord for center of all char slots
	LoadSetting("TopSlotY", 105, "ClickLocations")	; Y-coord for center of top char slot

	;--- Vault of Piety --- 
	LoadSetting("VpButtonX", 1722, "ClickLocations")
	LoadSetting("VpButtonY", 1028, "ClickLocations")

	LoadSetting("VpCsTabX", 650, "ClickLocations")
	LoadSetting("VpCsTabY", 300, "ClickLocations")

	LoadSetting("VpCsRedeemX", 440, "ClickLocations")
	LoadSetting("VpCsRedeemY", 480, "ClickLocations")

	LoadSetting("VpEofAmtOkX", 1030, "ClickLocations")
	LoadSetting("VpEofAmtOkY", 630, "ClickLocations")

	LoadSetting("VpCelSynLeftArrowX", 369, "ClickLocations")
	LoadSetting("VpCelSynLeftArrowY", 631, "ClickLocations")

	LoadSetting("VpCelSynRightArrowX", 805, "ClickLocations")
	LoadSetting("VpCelSynRightArrowY", 626, "ClickLocations")

	LoadSetting("VpCctPanelX", 592, "ClickLocations")
	LoadSetting("VpCctPanelY", 618, "ClickLocations")

	LoadSetting("VpEofPanelX", 600, "ClickLocations")
	LoadSetting("VpEofPanelY", 620, "ClickLocations")

	LoadSetting("VpCcaePanelX", 380, "ClickLocations")
	LoadSetting("VpCcaePanelY", 689, "ClickLocations")

	LoadSetting("VpCwaPanelX", 716, "ClickLocations")
	LoadSetting("VpCwaPanelY", 626, "ClickLocations")

	LoadSetting("VpCwaPurchaseOkX", 1042, "ClickLocations")
	LoadSetting("VpCwaPurchaseOkY", 582, "ClickLocations")

	LoadSetting("VpMaxBlessVpX", 1090, "ClickLocations")
	LoadSetting("VpMaxBlessVpY", 592, "ClickLocations")

	;--- Delays:(these delays have an additional +/-120ms random delay added)	---
	;--- Don't worry about getting these perfect, the script automatically		---
	;--- detects when it can go to each next step.								---
	LoadSetting("AfterCharSelectDelay", 5500, "Delays")
	LoadSetting("AfterLoginDelay", 8100, "Delays")
	LoadSetting("BeforeInvokeDelay", 3750, "Delays")
	LoadSetting("AfterInvokeDelay", 3750, "Delays")
	LoadSetting("LoggingOutClickExitNowExtraDelay", 500, "Delays")
	LoadSetting("AfterLogoutDelay", 5100, "Delays")
	LoadSetting("NwMouseDragClickDelay", 750, "Delays")


	;--- Rectangle to search for character logged in confirmation (serpent) ---

	LoadSetting("LiTopLeftX", 550, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LiTopLeftY", 950, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LiBotRightX", 1250, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LiBotRightY", 1080, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Li_ImageFile", "nw_character_panel_serpent.bmp", "SearchRectanglesAnd_ImageFiles")
	Li_ImageFile := A_ImagesDir . "\" . Li_ImageFile

	;--- Rectangle to search for character select screen confirmation (enter world button) ---
	LoadSetting("EwTopLeftX", 1420, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("EwTopLeftY", 700, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("EwBotRightX", 1830, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("EwBotRightY", 1030, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Ew_ImageFile", "nw_ew_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Ew_ImageFile := A_ImagesDir . "\" . Ew_ImageFile

	;--- Rectangle to search for character log in screen confirmation (login button) ---
	LoadSetting("LbTopLeftX", 280, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LbTopLeftY", 440, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LbBotRightX", 920, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("LbBotRightY", 1260, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Lb_ImageFile", "nw_login_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Lb_ImageFile := A_ImagesDir . "\" . Lb_ImageFile

	;--- Connecting to server ---
	LoadSetting("ClTopLeftX", 230, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("ClTopLeftY", 280, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("ClBotRightX", 1400, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("ClBotRightY", 800, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Cl_ImageFile", "nw_connecting_to_login.bmp", "SearchRectanglesAnd_ImageFiles")
	Cl_ImageFile := A_ImagesDir . "\" . Cl_ImageFile

	;--- Rectangle to search for vault of piety title ---
	LoadSetting("VpTopLeftX", 300, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("VpTopLeftY", 140, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("VpBotRightX", 960, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("VpBotRightY", 480, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Vp_ImageFile1", "nw_vault_piety_title.bmp", "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Vp_ImageFile2", "nw_vault_piety_title_yellow.bmp", "SearchRectanglesAnd_ImageFiles")
	Vp_ImageFile1 := A_ImagesDir . "\" . Vp_ImageFile1
	Vp_ImageFile2 := A_ImagesDir . "\" . Vp_ImageFile2

	;--- Invocation status ---
	LoadSetting("InvokeReadyTopLeftX", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("InvokeReadyTopLeftY", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("InvokeReadyBotRightX", 1920, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("InvokeReadyBotRightY", 1080, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("InvokeNotReady_ImageFile", "nw_invoke_notready.bmp", "SearchRectanglesAnd_ImageFiles")
	LoadSetting("InvokeReady_ImageFile", "nw_invoke_ready.bmp", "SearchRectanglesAnd_ImageFiles")
	InvokeNotReady_ImageFile := A_ImagesDir . "\" . InvokeNotReady_ImageFile
	InvokeReady_ImageFile := A_ImagesDir . "\" . InvokeReady_ImageFile

	;--- Altar Icon ---
	LoadSetting("AiTopLeftX", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("AiTopLeftY", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("AiBotRightX", 1920, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("AiBotRightY", 1080, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Ai_ImageFile", "nw_altar_icon.bmp", "SearchRectanglesAnd_ImageFiles")
	Ai_ImageFile := A_ImagesDir . "\" . Ai_ImageFile

	;--- Maximum Blessings ---
	LoadSetting("MbTopLeftX", 500, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("MbTopLeftY", 300, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("MbBotRightX", 1400, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("MbBotRightY", 700, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Mb_ImageFile", "nw_maximum_blessings_new.bmp", "SearchRectanglesAnd_ImageFiles")
	Mb_ImageFile := A_ImagesDir . "\" . Mb_ImageFile

	;--- Patcher Play Button---
	LoadSetting("PpbTopLeftX", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("PpbTopLeftY", 1, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("PpbBotRightX", 1920, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("PpbBotRightY", 1680, "SearchRectanglesAnd_ImageFiles")
	LoadSetting("Ppb_ImageFile", "nw_patcher_play_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Ppb_ImageFile := A_ImagesDir . "\" . Ppb_ImageFile

	;--- Patcher Login Button ---
	LoadSetting("Plb_ImageFile", "nw_patcher_login_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Plb_ImageFile := A_ImagesDir . "\" . Plb_ImageFile

	;--- Client Login Button --- NEW ---
	LoadSetting("ClientLoginButton_ImageFile", "ClientLoginButton.png", "SearchRectanglesAnd_ImageFiles")
	ClientLoginButton_ImageFile := A_ImagesDir . "\" . ClientLoginButton_ImageFile

	;--- Client Charselect Logout Button ---
	LoadSetting("Cslo_ImageFile", "nw_charselect_logout_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Cslo_ImageFile := A_ImagesDir . "\" . Cslo_ImageFile

	;--- Client Login Exit Button ---
	LoadSetting("Lseb_ImageFile", "nw_loginscreen_exit_button.bmp", "SearchRectanglesAnd_ImageFiles")
	Lseb_ImageFile := A_ImagesDir . "\" . Lseb_ImageFile

	;--- Ok Button Upon Character Select ---
	LoadSetting("OkUcs_ImageFile", "nw_ok_button_upon_charselect.bmp", "SearchRectanglesAnd_ImageFiles")
	OkUcs_ImageFile := A_ImagesDir . "\" . OkUcs_ImageFile

	;--- Okay Button Upon Character Select ---
	LoadSetting("OkayUcs_ImageFile", "nw_okay_button_upon_charselect.bmp", "SearchRectanglesAnd_ImageFiles")
	OkayUcs_ImageFile := A_ImagesDir . "\" . OkayUcs_ImageFile

	;--- Powers Reset Button Upon Character Select ---
	LoadSetting("PowersReset_ImageFile", "powers_reset.bmp", "SearchRectanglesAnd_ImageFiles")
	PowersReset_ImageFile := A_ImagesDir . "\" . PowersReset_ImageFile

	
	;--- Character Select Safe Login Button --- NEW ---
	LoadSetting("CharSelectSafeLoginButton_ImageFile", "CharSelectSafeLoginButton.png", "SearchRectanglesAnd_ImageFiles")
	CharSelectSafeLoginButton_ImageFile := A_ImagesDir . "\" . CharSelectSafeLoginButton_ImageFile


	;--- Rectangle to search for number of 7/7 coins (unused)

	;--- Rectangle to search for number of other coins (unused)

	;--- Rectangle to search for AD Amount (unused)

	ConfirmLoadUiBind := 0
	CurrentCharacter := 0
	ToggleInv := 0
	OffsetSmall=7 ;Randomized offset for clicking
	mouseDragStartX := 0
	mouseDragStartY := 0
	mouseDragEndX := 0
	mouseDragEndY := 0
	mouseDragClickX := 0
	mouseDragClickY := 0
	mouseDragExtraX := 0
	mouseDragExtraY := 0
	bagSlotCurrent := 0

	tmp_fakedate := A_Now
	EnvAdd, tmp_fakedate, -30, days  ; -------------------------=-=-=-=-=-=-=- Change to 5 years ago

	_powersWindowState := 0	; Closed: 0; Open: 1;
	_powersWindowPosition := 0 ; Number of scrolls down in powers window
	
	LogNewLine()
	LogAppend("[*****TRACE: INIT(): NW_COMMON INITIALIZED]")	
	LogNewLine()
	; LogAppend("[Image folder: " . A_ImagesDir . "]")
	
	ToggleInv := 1
	
	FirstRun := 1
	EwRetryAttempts := 0
	UpdateInvokeLog := 1
}

;==========================================================
;======================== FUNCTIONS =======================
;==========================================================

AutoLaunchInvoke() {
	global
	
	LogNewLine()
	LogAppend("[Beginning AutoLaunchInvoke()]")
	
	WakeScreen()
	
	Sleep 1000
	
	SendEvent {Click 1200, 500, 0}
		
	;MsgBox 48, Abort Auto-Launch?, Neverwinter will launch for auto-invoke in 10 seconds. Press OK to abort., 10
	;IfMsgBox Timeout
	;{
	;Wake up from sleep and screensavers
	;PostMessage, 0x0112, 0x0F060, 0,, A			; 0x0112 is WM_SYSCOMMAND, 0x0F060 is SC_CLOSE -- turns off screensaver
	;WakeScreen()  	; Doing this later already now		

	Sleep 1000
	
	ActivateNeverwinter()
	
	Sleep 4000

	AutoInvoke()
			
	LogAppend("[Auto-Activated Invocation Complete]")
	
	VigilantlyCloseClientAndExit()
}

VigilantlyCloseClientAndExit() {
	global
	if (FindLoggedIn()) {
		Logout()
		Sleep 5000
	}
	
	if (FindEwButton()) {
		FindAndClick(Cslo_ImageFile)
		Sleep 3000
	}

	if (FindClientLoginButton()) {
		FindAndClick(Lseb_ImageFile)
	}
	
	
	; ##### SHUT THINGS DOWN
	
	IfWinExist ahk_class CrypticWindowClass
	{
		WinKill
	}
	
	IfWinExist ahk_class #32770
	{
		WinKill
	}		
	
	IfWinExist ahk_class #327707
	{
		WinKill
	}
	
	IfWinExist ahk_exe Neverwinter.exe
	{
		WinKill
	}
	
	IfWinExist ahk_exe GameClient.exe
	{
		WinKill
	}
	
				
	; PostMessage, 0x0112, 0xF170, 2,, A 			; Turn off Display (-1 on, 1 low-pow, 2 off)
	
	; exitapp 0
}



; ============================== AutoInvoke ===================================


/* 
invoke_modes:
1.) Invoke and Logoff (default)
2.) Invoke, Buy Elixir of Fate, Logoff 
3.) Hover over character slot but don't log on
*/

AutoInvoke(invoke_mode := 1) {
	global
	
	if (ToggleInv) {
		ToggleInv := 0
		pause on, 1
		suspend
		msgbox Script paused and suspended. Right click tray icon to unsuspend/unpause.
		;exit
	} else {
		pause off, 1
		ToggleInv := 1
	}
	
	
	if (AutoUiBindLoad) {
		MsgBox, 292, Load Bindings and UI?, You have set AutoUiBindLoad=1 in %gcs_ini%. Do you want to copy key bindings and ui settings from the first character to all other characters? , 120
		IfMsgBox Yes 
		{
			Sleep 1000
			ActivateNeverwinter()
		}
		IfMsgbox No
		{
			Sleep 500
			msgbox Exiting Script. Please edit %gcs_ini% and adjust AutoUiBindLoad then restart.
			pause on, 1
			exit
			suspend on
		}
	}
		
	LoadSetting("LastCharacterInvoked", 0, "Invocation", as_ini)
	
	; #####
	; ##### PRIMARY INVOCATION LOOP
	; ##### 	- THIS LOOP NEEDS SERIOUS REFACTORING
	; #####
	
	While ((ToggleInv) && (LastCharacterInvoked < NwCharacterCount)) {
		CurrentCharacter := LastCharacterInvoked + 1
		char_label := "Character " . CurrentCharacter
		LoadSetting("MostRecentInvocationTime", tmp_fakedate, char_label, as_ini) 
		LoadSetting("VaultPurchase", 7, char_label, as_ini)
		
		LogAppend("[***** TRACE: AUTOINVOKE(): ATTEMPTING TO DETERMINE CURRENT CLIENT STATE]")
 
		
		if (FindEwButton()) {
			LogAppend("[***** TRACE: AUTOINVOKE(): FindEwButton()]")		

			; ##### INVOCATION ENTRY POINT
			invocation_result := EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad, FirstRun, VaultPurchase)
			
			if (invocation_result == 1) {
				FirstRun := 0
				EwRetryAttempts := 0
				LastCharacterInvoked := CurrentCharacter
				LogAppend("[***** TRACE: AUTOINVOKE(): CHARACTER INVOCATION COMPLETE]")
			} else {
				LogAppend("[### AutoInvoke(): Fatal Error, EnterWorldInvoke() has failed. Character " . CurrentCharacter . " was not invoked. ###]")
			}
			
			continue
			
		} else if (FindClientLoginButton() || FindConnLogin()) {				; EwButton not found AND either Login Button OR Conn Login is in view
		
			LogAppend("[***** TRACE: AUTOINVOKE(): FindClientLoginButton() || FindConnLogin()]")
			
			ClientLogin()
		} else if (FindLoggedIn()) {									; EwButton not found AND We are logged in
		
			LogAppend("[***** TRACE: AUTOINVOKE(): FindLoggedIn()]")
			
			MoveAround()
			Logout()
		} else {														; EwButton not found, not logged in, no login screens in view
			LogAppend("AutoInvoke(): Unable to find Enter World, Login, Connect, or Play Buttons.")
			LogAppend("AutoInvoke(): Cannot determine current client state. Trying again...")
			
			if (EwRetryAttempts > 150) {									; Same as above plus we've been trying for 150 seconds now

				LogAppend("[AutoInvoke(): EwRetryAttempts > 150, Checking CrashCheckRestart()]")
				
				if (CrashCheckRestart()) {
					LogAppend("[AutoInvoke(): CrashCheckRestart() has restarted Neverwinter, continuing]")
					EwRetryAttempts := 0
					continue
				} else {
					LogAppend("[AutoInvoke(): No Crash Detected, Aborting.]")
					return 0
				}

			}
			EwRetryAttempts := EwRetryAttempts + 1
			
			LogAppend("[***** TRACE: AUTOINVOKE(): SLEEPING FOR 1 SECOND]")
			Sleep 1000
		}
		
	}
		
	if (LastCharacterInvoked >= NwCharacterCount)  {
		IniWrite, 0, %as_ini%, Invocation, LastCharacterInvoked
		IniWrite, %A_Now%, %as_ini%, Invocation, LastInvocationCompleteTime
		ToggleInv = 0
	}
}

ClientLogin() {
	global
	
	While ((ToggleInv && !FindClientLoginButton()) && (A_Index < 10))  {
		LogAppend("[Attempting to find ClientLoginButton.]")
		sleep 1500
	}
	
	if (ToggleInv && !FindClientLoginButton()) {
			LogAppend("[Not sure if we found ClientLoginButton but continuing...]")
			; msgbox ClientLogin() Waited too long for Login Screen to appear. If you are at login screen please check or remake %Lb_ImageFile%.
	}
	
	if (ToggleInv) {
		Sleep 2000 + Ran(500)
		Send {Shift down}
		Sleep 20
		Send {Home down}
		Sleep 20
		Send {Shift up}
		Sleep 20
		Send {Home up}
		Sleep 20
		Send %NwUserName%
		Sleep 50
		Send {Tab}
		Sleep 200 + Ran(100)
		Send %NwActPwd%
		Sleep 200 + Ran(100)
		Send {Enter}
		Sleep AfterLoginDelay + Ran(120)
	}
}

; #####
; ##### SPLIT INTO ENTERWORLD + INVOKE
; #####
EnterWorldInvoke(invoke_mode, MostRecentInvocationTime, CurrentCharacter, AutoUiBindLoad := 0, FirstRun := 0, VaultPurchase := 5) {
	global
	
	; invo_gap := A_Now
	; invo_gap -= MostRecentInvocationTime, Minutes
	invo_gap := 60
	LogAppend("[EnterWorldInvoke(): invoke_mode:" . invoke_mode . ", MostRecentInvocationTime:" . MostRecentInvocationTime . ", CurrentCharacter:" . CurrentCharacter . ", AutoUiBindLoad:" . AutoUiBindLoad . ", FirstRun:" . FirstRun . ", VaultPurchase:" . VaultPurchase . ", invo_gap:" . invo_gap . "]")
	
	if ((invo_gap > 15) || (invoke_mode == 2)) {
		SelectCharacter(CurrentCharacter, 1, 0)
	
		While (!(FindLoggedIn())) {
		
	
			; ##### IF IT'S OUR SECOND TRY, CLOSE POP-UP DIALOGUES
			if (A_Index > 2) {
				LogAppend("[EnterWorldInvoke(): (FindLoggedIn() = '" . FindLoggedIn() . "')  (Attempt: " . A_Index . ")]")
				ClearOkPopupBullshit()
				ClearSafeLogin()
			}

			; ##### IF WE'VE BEEN AT IT FOR 10 TRIES, CHECK TO SEE IF WE'VE CRASHED
			if ((A_Index >= 15)) {
				LogAppend("[EnterWorldInvoke(): Checking CrashCheckRestart()]")
				if (CrashCheckRestart()) {
					Sleep 5000
					SelectCharacter(CurrentCharacter, 1, 0)
					continue
				}
			}
			
			; ##### GIVE UP AND ABORT
			if (A_Index == 30) {
				err_str := "[*** AutoInvoke(): Error Logging In. Aborting. ***]"
				LogAppend(err_str)
				
				Sleep 5000
				return 0
			}
			
			; ##### SLEEP BEFORE NEXT 'ARE WE LOGGED IN' CHECK
			Sleep 1200
		}
		; #####
		; #####	END ENTER WORLD
		; #####
		
		
		; ##### LOAD BINDINGS AND UI IF NEEDED
		if (AutoUiBindLoad) {
			if (FirstRun) {
				; ***** NOTE: WHAT IS FIRSTRUN ALL ABOUT? DO WE NEED IT?
				LoadBindAndUi(1)
			} else {
				LoadBindAndUi(0)
			}
		}
		
		; #####
		; #####	BEGIN INVOKE - INVOCATION ENTRY POINT
		; #####
		
		While (FindLoggedIn()) {
		
			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): PRE-INVOKE]")
			
			Invoke(FirstRun, VaultPurchase)
			
			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-INVOKE]")


			
			if (invoke_mode == 2) {
				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): REEDEEMING]")
				Redeem(3)
			}
			
			; #####	CLOSE ALL POP-UPS AND LOOP UNTIL THAT IS DONE
			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): CLEARING POPUPS]")

			if (ClearOkPopupBullshit()) {
				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): AT LEAST ONE POPUP WAS CLEARED, ATTEMPTING TO INVOKE AGAIN]")
				continue
			} else {
				LogAppend("[*****TRACE: ENTERWORLDINVOKE(): NO POPUPS WERE FOUND, COMPLETED]")
				break
			}
			
			LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-INVOKE POST-CLEAR-POPUPS]")
		}
		
		LogAppend("[*****TRACE: ENTERWORLDINVOKE(): PRE-LOGOUT]")
		
		Logout()
		
		LogAppend("[*****TRACE: ENTERWORLDINVOKE(): POST-LOGOUT]")
		
	} else {
	
		if ((invo_gap <= 15) || (invoke_mode == 3)) {
			if (invo_gap <= 15) {
				LogAppend("[Character " . CurrentCharacter . " has been invoked within the last 15 minutes. Skipping.]")
			}
			
			if (SelectCharacter(CurrentCharacter, 1, 1) == 1) {
				LogAppend("[*** Character " . CurrentCharacter . " was not correctly skipped. ***]")
			}
			
			UpdateInvokeLog = 0
		}		
	}
	
	
	char_label := "Character " . CurrentCharacter
	;LoadSetting("MostRecentInvocationTime", tmp_fakedate, char_label, as_ini) 
	
	if (UpdateInvokeLog) {
		;IniWrite, %A_Now%, %as_ini%, %char_label%, MostRecentInvocationTime
		;IniWrite, %invo_gap%, %as_ini%, %char_label%, PrevInvoTimeGap
		LogAppend("[Logging Out]")
	}

	;IniWrite, %CurrentCharacter%, %as_ini%, Invocation, LastCharacterInvoked
	
	LogAppend("[EnterWorldInvoke(): Returning 1]")
	
	return 1
}

SelectCharacter(n_invoke, scroll_top, skip_char) {
	global
	
	LogAppend("[Character:" . CurrentCharacter . ", skip_char:" . skip_char . ", TopSlotY:" . TopSlotY . "]")
	ClickX := CharSlotX
	must_scroll := 0
	scroll_count := 0
	;clickable_slots := 8
	clickable_slots := VisibleCharacterSelectSlots
	BotSlotY := TopSlotY + 45 + (70 * (clickable_slots - 1))

	Sleep 500 + Ran(100)
	
	if (n_invoke > clickable_slots) {
		must_scroll := 1
	}
	
	if (must_scroll) {
		ScrollChunks := Floor((n_invoke - (clickable_slots + 1))/2)
		Position := Mod(n_invoke,2)
		scroll_count := (7 * ScrollChunks) + 5
		ClickY := (BotSlotY - (70 * Position))
	} else {
		ClickY := TopSlotY + (70 * (n_invoke - 1))
	}
		
	if (scroll_top) {

		Sleep 150
		
		Loop 7 {
			SendEvent {Click %CharacterSelectScrollBarTopX%, %CharacterSelectScrollBarTopY%, 1}
			Sleep 100
		}
		
		Loop 5 {
			SendEvent {Click WheelUp}
			Sleep 50
		}
		
		Sleep 40
		
		;Loop 140 {
		;	SendEvent {Click WheelUp}
		;	Sleep 20
		;}
		;sleep 200
	}
	
	LogAppend("[SelectCharacter():" . CurrentCharacter . ", ClickX:" . ClickX . ", ClickY:" . ClickY . "]")
	
	SendEvent {Click %ClickX%, %ClickY%, 0}
	Sleep 170
		
	if (must_scroll) {
		Loop %scroll_count% {
			SendEvent {Click WheelDown}
			Sleep 25
		}
	}
	
		
	; --- LOG IN ---
	if (skip_char <> 1) {
		;SendEvent {Click %EwButtonX%, %EwButtonY%, 1}
		
		SendEvent {Click %ClickX%, %ClickY%, 1}
		Sleep 80
		SendEvent {Click %ClickX%, %ClickY%, 1}
		Sleep 370
		
		Send {Enter}
		Sleep AfterCharSelectDelay + Ran(120)
		
	} else {
		
		Sleep 50
		SendEvent {Click %ClickX%, %ClickY%, 1}
		Sleep 170
		return 0
	}
	
	return 1
}


Invoke(first_run, vault_purchase) {
	global
	invo_poss := 0
	invo_started := 0
	invo_altar_deployed := 0
	if (first_run) {
		Sleep 500 + Ran(200)
		MoveAround()
		Sleep 500 + Ran(200)
	}
				
	while (invo_started == 0) {
		MoveAround()
		Sleep 500
		invo_poss += FindInvokePossible()

		;Send {%NwInvokeKey%}
		;Sleep 200
		;Send {%NwInvokeKey%}
		;Sleep 200

		Sleep %BeforeInvokeDelay%
		
		Send {%NwInvokeKey% down}
		sleep 180
		Send {%NwInvokeKey% up}
		sleep %AfterInvokeDelay%
		
		
		if (invo_poss < 1) {
			sleep 500
			invo_poss += FindInvokePossible()
		}
		
		Sleep 100
		
		if (FindMaxBlessings()) { 			; == Max Blessing Windows Found == --- Maybe Move this to ConfirmInvoke() ---
			Redeem(vault_purchase)
			LogAppend("[CWA Purchased]")
			invo_started := 0
			continue
		} else if (0) {						; == Unreachable Code ==
			continue
		} else {							; == Normal Situation ==
			if (invo_poss < 1) {
				sleep 500
				invo_poss += FindInvokePossible()
			}
			
			if (invo_poss > 0) {			; == FindInvokePossible() was true ==
				invo_started := 1
				LogAppend("[Invoke Started]")
			} else {						; == FindInvokePossible() was false, let's find out why ==
				LogAppend("[Invoke(): Checking CrashCheckRestart()")
				if (CrashCheckRestart()) {
					invo_started := 0
					continue
				}

				if (invo_altar_deployed) {
					LogAppend("[Altar Deployed but Cannot Invoke]")
					return 0
				}
				
				MoveAround()
				
				if (DeployAltar()) {
					LogAppend("[Altar Deployed]")
					invo_altar_deployed := 1
				} else {
					LogAppend("[Altar Not Found - Invoke Failed]")
					return 0
				}
				
				invo_started := 0
				continue
			}
			Sleep 150 + Ran(20)
			invo_started := 1
		}
	}
	
	Sleep 200
	Send {%NwInvokeKey%}

	return 1
}
 
MoveAround() {
	global
	Sleep 135 + Ran(70)
	
	Send {%NwMoveLeftKey% down}
	Sleep 180
	Send {%NwMoveLeftKey% up}
	Sleep 180
	
	Send {%NwMoveRightKey% down}
	Sleep 180
	Send {%NwMoveRightKey% up}
	Sleep 180
}

/*
Vault Purchase Item:
3: Elixir of Fate
6: Campaign Cache
7: Coffer of Wondrous Augmentation
*/

Redeem(vault_purchase) {
	global
	Sleep 1000
	LogAppend("[Redeeming Item: " . vault_purchase . "]")
	while (!(FindVaultOpen()) && (A_Index <= 5)) {
		Sleep 1000
		if (A_Index > 5) {
			msgbox Redeem(): Problem finding vault window.
			exit
		}
		if (FindMaxBlessings()) {
			Sleep 200
			SendEvent {Click %VpMaxBlessVpX%, %VpMaxBlessVpY%, 1}
			Sleep 500
		} else { 
			Sleep 1000
			MoveAround()
			Sleep 500
			Send {%NwCursorMode%}
			Sleep 500
			SendEvent {Click %VpButtonX%, %VpButtonY%, 1}
			Sleep 2500
		}
	}
	
	SendEvent {Click %VpCsTabX%, %VpCsTabY%, 1}
	Sleep 600
		
	if (vault_purchase == 3) {
		; CLICK TWICE 'CAUSE
		Sleep 400
		SendEvent {Click %VpEofPanelX%, %VpEofPanelY%, 1}
		Sleep 400
		SendEvent {Click %VpEofPanelX%, %VpEofPanelY%, 1}
		Sleep 400
		
		SendEvent {Click %VpCsRedeemX%, %VpCsRedeemY%, 1}
		Sleep 400
		SendEvent {Click %VpEofAmtOkX%, %VpEofAmtOkY%, 1}
		Sleep 400
	} else if (vault_purchase == 5) {
		; CLICK TWICE 'CAUSE
		Sleep 400
		SendEvent {Click %VpCcaePanelX%, %VpCcaePanelY%, 1}
		Sleep 400
		SendEvent {Click %VpCcaePanelX%, %VpCcaePanelY%, 1}
		Sleep 400
		
		Sleep 400
		SendEvent {Click %VpCsRedeemX%, %VpCsRedeemY%, 1}
		Sleep 400
		SendEvent {Click %VpCwaPurchaseOkX%, %VpCwaPurchaseOkY%, 1}
		Sleep 400
	} else {
		msgbox Unknown Redeem(vault_purchase)
		exit
	}
	
	return "[Vault of Piety Item #" . vault_purchase . " Purchased]"
}

DeployAltar() {
	global
	
	da_altar_deployed := 0
	altar_deploy_attempts := 2
	
	LogAppend("[Attempting to Deploy Altar]")
	while ((!da_altar_deployed) && (A_Index <= altar_deploy_attempts)) {
		LogAppend("[Attempting to Open Inventory]")
		MoveAround()
		Sleep 500 + Ran(50)
		SendHotkey(NwInventoryKey)
		Sleep 1500
		if (FindAltarIcon(0)) {
			da_altar_deployed := 1
		}
	}
	if (FindAltarIcon(1)) {
		LogAppend("[Altar Icon Found and Clicked]")
		Sleep 1200
		da_altar_deployed := 1
	}
	return %da_altar_deployed%
}

LoadBindAndUi(save := 0) {
	global
	
	Sleep 4000 + Ran(1500)
	
	MoveAround()
	
	Sleep 250
	Send {Enter}
	Sleep 250
	if (save) {
		Send /ui_save_file
		Sleep 130
		Send {space}
		Sleep 100 + Ran(50) 
		Send %NwUiFileName%
	} else {
		Send /ui_load_file 
		Sleep 130
		Send {space}
		Sleep 100 + Ran(50) 
		Send %NwUiFileName%
	}
	Sleep 150
	Send {Enter}
	Sleep 310
	
	MoveAround()

	Sleep 150
	Send {Enter}
	Sleep 250
	if (save) {
		Send /bind_save_file
		Sleep 130
		Send {space}
		Sleep 100 + Ran(50) 
		Send %NwBindFileName%
	} else {
		Send /bind_load_file
		Sleep 130
		Send {space}
		Sleep 100 + Ran(50)
		Send %NwBindFileName%
	}
	Sleep 150
	Send {Enter}
	Sleep 260
	
	MoveAround()
	
	Sleep 500 + Ran(400)
}


; Returns 0 if Neverwinter is running and negative numbers if restarted or for problems
CrashCheckRestart() {
	if (WinExist("ahk_class CrypticWindowClass")) {
		Sleep 50
		ClearOkPopupBullshit()
		Sleep 50
		ClearSafeLogin()
		Sleep 50
		return 0
	} else {
		LogAppend("[CrashCheckRestart(): Neverwinter probably crashed and is not running, restarting]")
		ActivateNeverwinter()
		return 1
	}

	;LogAppend("CrashCheckRestart(): WinExist('ahk_class CrypticWindowClass') == " . WinExist("ahk_class CrypticWindowClass"))
	return -99
}


Logout(fast_mode := 0) {
	global
	if (!fast_mode) {
		MoveAround()
	}
	
	LogAppend("[*****TRACE: LOGGING OUT]")
	
	Sleep 100
	
	Send {Enter}
	Sleep 60
	Send /gotocharacterselect
	Sleep 90
	Send {Enter}
	Sleep 100
}




/*
Context:
1.) Drag-and-drop start
2.) Drag-and-drop end

*/
CaptureMouseDragStart() {
	global
	MouseGetPos, mouseDragStartX, mouseDragStartY
	bagSlotCurrent := 0
}

CaptureMouseDragEnd() {
	global
	MouseGetPos, mouseDragEndX, mouseDragEndY
}

CaptureMouseDragClick() {
	global
	MouseGetPos, mouseDragClickX, mouseDragClickY
}

CaptureMouseDragExtra() {
	global
	MouseGetPos, mouseDragExtraX, mouseDragExtraY
}

/*
* MouseDragClick()
* destSlotType:
*	0: Single Slot
*	1: Mail Slots
*/
MouseDragClick(stackSize := 20, destSlotType := 0, extraBagsFull := 0) {
	global
	if ((mouseDragStartX == 0) || (mouseDragEndX == 0) || (mouseDragClickX == 0)) {
		msgbox Mouse drag points not set!
		Sleep 100
		ActivateNeverwinter()
		return
	}
	if ((destSlotType == 1) && (mouseDragExtraX == 0)) {
		msgbox Extra click point not set!
		Sleep 100
		ActivateNeverwinter()
		return
	}
	if (ToggleMouseDragClick == 1) {
		ToggleMouseDragClick := 0
		return
	} else {
		ToggleMouseDragClick := 1
	}
	
	bagSlotCurrentX := 0
	bagSlotCurrentY := 0
	RefineStackCurrentRemaining := stackSize
	MailSlotCurrent := 0
	
	mdeX := mouseDragEndX
	
	While (ToggleMouseDragClick) {
		if (bagSlotCurrent > 29) {
			bagSlotCurrent := 0
			if (extraBagsFull > 0) {
				mouseDragStartY := mouseDragStartY + 47
				extraBagsFull--
			} else {
				ToggleMouseDragClick := 0
				break
			}
		}
		
		if (destSlotType == 1) {
			stackSize := 0
			if (MailSlotCurrent < 5) {
				mdeX := mouseDragEndX + (MailSlotCurrent * 56)
			} else {
				MailSlotCurrent := 0
				mdeX := mouseDragEndX
			}
			
		}
		
		if (destSlotType == 1) {
			if (MailSlotCurrent == 0) {
				Sleep 90
				SendEvent {Click %mouseDragExtraX%, %mouseDragExtraY%, 1}
				Sleep 90
				Send @cogent992
				Sleep 70
				Send {enter}
			}
		} else {
			if (A_Index == 1) {
				Sleep 800
				SendEvent {Click %mouseDragStartX%, %mouseDragStartY%, 0}
			}
		}
		Sleep 100 + Ran(20)
		
		if (destSlotType == 0) {
			SendEvent {Click %mouseDragStartX%, %mouseDragStartY%, 0}
			Sleep %NwMouseDragClickDelay%
		} else {
			Sleep 50
		}
		SendEvent {Click %mouseDragStartX%, %mouseDragStartY%, down}
		Sleep 75 + Ran(50)
		SendEvent {click %mdeX%, %mouseDragEndY%, up}
		
		if ((destSlotType == 0) || ((destSlotType == 1) && (MailSlotCurrent == 4))) {
			Sleep 110 + Ran(20)
			SendEvent {Click %mouseDragClickX%, %mouseDragClickY%, 1}
		}
		Sleep 70
		
		RefineStackCurrentRemaining--
		
		if (RefineStackCurrentRemaining <= 0) {
			if (destSlotType == 0) {
				Sleep 400
			}
			
			if (bagSlotCurrentX >= (InventoryBagWidthSlots - 1)) {
				bagSlotCurrentX := 0
				bagSlotCurrentY++
				mouseDragStartX := mouseDragStartX - (47 * (InventoryBagWidthSlots - 1))
				mouseDragStartY := mouseDragStartY + 47
			} else {
				mouseDragStartX := mouseDragStartX + 47
				bagSlotCurrentX++
			}
			
			if (destSlotType == 0) {
				SendEvent {Click %mouseDragStartX%, %mouseDragStartY%, 0}
			}
			RefineStackCurrentRemaining := stackSize
			bagSlotCurrent++
			mailSlotCurrent++
			;ToggleSlotSpellClick := 0
		}
	}
}


/*
* SlotPower()
* Takes a power abbreviation and generates a DragSpell() for said power
*
* Powers:
*	Assign any text identifier.
*
* Destination Slots:
*	D1 - Left Daily Slot
*	D2 - Right Daily Slot
*
*	F1 - Top Feature Slot
*	F2 - Bottom Feature Slot
*
*	E0 - Special Encounter Slot
*
*	E1 - Left Encounter Slot
*	E2 - Center Encounter Slot
*	E3 - Right Encounter Slot
*
*	A1 - Left At-Will Slot
*	A2 - Right At-Will Slot
*

*/
SlotPower(powerAbbr, destSlot) {
	global
	endCoordX := 0 
	endCoordY := 0
	
	
	if (destSlot == "D1") {
		endCoordX = 870
		endCoordY = 975
	} else if (destSlot == "D2") {
		endCoordX = 1000
		endCoordY = 975
	} else if (destSlot == "F1") {
		endCoordX = 853
		endCoordY = 1012
	} else if (destSlot == "F2") {
		endCoordX = 854
		endCoordY = 1039
	} else if (destSlot == "E0") {
		endCoordX = 809
		endCoordY = 1027
	} else if (destSlot == "E1") {
		endCoordX = 894
		endCoordY = 1026
	} else if (destSlot == "E2") {
		endCoordX = 936
		endCoordY = 1029
	} else if (destSlot == "E3") {
		endCoordX = 978
		endCoordY = 1024
	} else if (destSlot == "A1") {
		endCoordX = 1024
		endCoordY = 1026
	} else if (destSlot == "A2") {
		endCoordX = 1068
		endCoordY = 1026
	} else {
		msgbox SlotPower: destSlot not valid
	}
	
	
	; 0 24 32 - covers all positions
	if (powerAbbr == "CHST") {						; Chill Strike
		DragSpell(0, 993, 370, endCoordX, endCoordY)
	} else if (powerAbbr == "CNOI") {					; Conduit of Ice
		DragSpell(0, 693,  532, endCoordX, endCoordY)
	} else if (powerAbbr == "ENTF") {					; Entangling Force
		DragSpell(0, 893, 530, endCoordX, endCoordY)
	} else if (powerAbbr == "RPEL") {					; Repel
		DragSpell(14, 596, 446, endCoordX, endCoordY)
	} else if (powerAbbr == "SHLD") {					; Shield
		DragSpell(14, 887, 542, endCoordX, endCoordY)
	} else if (powerAbbr == "ICTR") {					; Icy Terrain
		DragSpell(14, 594, 646, endCoordX, endCoordY)
	} else if (powerAbbr == "SDST") {					; Sudden Storm
		DragSpell(14, 793, 648, endCoordX, endCoordY)
	} else if (powerAbbr == "ROEF") {					; Ray of Enfeeblement
		DragSpell(28, 895, 459, endCoordX, endCoordY)
	} else if (powerAbbr == "ICRY") {					; Icy Rays
		DragSpell(28, 692, 644, endCoordX, endCoordY)
	} else if (powerAbbr == "SLTM") {					; Steal Time
		DragSpell(28, 893, 644, endCoordX, endCoordY)
	} else {
		msgbox SlotPower: powerAbbr not valid
	}

	/*
	else if powerAbbr == "" {
		DragSpell(  ,   ,   , endCoordX, endCoordY)
	}
	*/
}

DragSpell(scrolls, startCoordX, startCoordY, endCoordX, endCoordY) {
	SendEvent {Click %startCoordX%, %startCoordY%, 0}
	Sleep 10
	powersWindowPosition(scrolls)
	Sleep 55
	SendEvent {Click %startCoordX%, %startCoordY%, down}
	Sleep 35
	SendEvent {click %endCoordX%, %endCoordY%, up}
	;	Sleep 75 + Ran(20)
}

powersWindowState(newState) {
	global
	
	if (_powersWindowState <> newState) {
		Send {z}
		Sleep 120
		_powersWindowState := !_powersWindowState
		
		if (_powersWindowState == 0) {
			_powersWindowPosition := -1
		} else {
			_powersWindowPosition := 0
		}
	
	}
	
	return _powersWindowState
}

powersWindowPosition(newPos) {
	global

	if (_powersWindowPosition <> newPos) {
		if (newPos == -1) {
			powersWindowState(0)
		} else {
			powersWindowState(1)
			Sleep 100
			;SoundBeep, 750, 150
		}
		
		wheelUps := 0
		wheelDowns := 0
		
		if (newPos > _powersWindowPosition) {
			wheelDowns := newPos - _powersWindowPosition
		} else {
			wheelUps := _powersWindowPosition - newPos
		}
		
		Sleep 10
		Loop %wheelUps% {
			SendEvent {Click WheelUp}
			Sleep 10
		}
		Loop %wheelDowns% {
			SendEvent {Click WheelDown}
			Sleep 10
		}
		
		;SoundBeep, 750, 150
		
		_powersWindowPosition := newPos
	}
	
	Sleep 10
	
	return _powersWindowPosition
}

ActivateNeverwinter() {
	global
	
	LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Beginning activation process.]")
	
	Sleep 100
	if (!(WinExist("ahk_class CrypticWindowClass") == "0x0")) {
		LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client already open; bringing window to foreground.]")
		WinActivate
	} else {
		patcherOpen := 0
		gameClientOpen := 0
		
		LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Game client not open.]")
		
		IfWinExist ahk_class #32770
		{
			WinKill
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'ahk_class #32770' (patcher)]")
			patcherOpen = 0
		}
		
		IfWinExist Neverwinter
		{
			WinKill
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Killing: 'Neverwinter')]")
			gameClientOpen = 0
		}
		
		Sleep 1000
		Sleep 1000
		Sleep 1000
		Sleep 1000
		Sleep 1000

		while (patcherOpen == 0) {
			if (A_Index >= 20) { 
				break 
			}
		
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Launching patcher.]")
			run %NwFolder%\Neverwinter.exe, %NwFolder%
			Sleep 5000
			
			if (FindPatcherOpen()) {
				LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher open, activating.]")
				WinActivate
				patcherOpen = 1
				
				while (!FindPatcherLoginButton()) {
					Sleep 5000
					if (FindPatcherOpen()) {
						WinActivate
					} else {
						patcherOpen = 0
						break
					}
					
					
					if (A_Index >= 15) {
						LogAppend("[ACTIVATENEVERWINTER(): Failed to find patcher login button after " . A_Index + 1 . " attempts.]")
						patcherOpen = 0
						break
					}
				}
				
			} else {
				while (patcherOpen == 0) {
					LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, waiting 5 seconds then trying again.]")
					Sleep 5000
					run %NwFolder%\Neverwinter.exe, %NwFolder%
					Sleep 5000
					
					if ((FindPatcherOpen() == 0) || (A_Index >= 10)) {
						LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher failed to launch after " . A_Index + 1 . " attempts.]")
						patcherOpen = 0
						return
					}
				}
			}

			
			if (patcherOpen == 0) {
				LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Patcher not open, restarting loop.]")
				continue
			}
						
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Entering UserName and Password.]")
			
			Sleep 200
			
			
			; FIXES SHIFT-STICKING ISSUE
			Send {Shift down}
			Sleep 200
			Send {Shift up}
			Sleep 200
			
			
			; SHIFT-TAB, ENTER USER NAME, ENTER PASSWORD, HIT ENTER
			Send {Shift down}
			Sleep 200
			Send {Tab down}
			Sleep 200
			Send {Shift up}
			Sleep 200
			Send {Tab up}
			Sleep 200
			Send %NwUserName%
			Sleep 200
			Send {Tab}
			Send %NwActPwd%
			Sleep 1000
			Send {enter}
			Sleep 8000
			
			
			// ***** NEED TO DETECT PATCHER PLAY BUTTON HERE *****
			
			while (!FindPatcherPlayButton()) {
				LogAppend("[Waiting for patcher play button to appear...]")
				Sleep 10000
				
				if (A_Index >= 60) {
					LogAppend("[FAILURE: Patcher play button failed to appear after " . ((A_Index * 10) / 60) . " minutes.]")
					return
				}
			}
			
			
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Attempting to hit 'Play' button.]")
			
			; ATTEMPT TO SHIFT TAB TO THE PLAY BUTTON
			Sleep 2000
            SendEvent {Shift Tab}
            Sleep 2000
            SendEvent {enter}
            Sleep 3000
			
			an_logged_in := 0
			
			CloseVerifyMsgbox()
			
			Sleep 700
			
			LogAppend("[***** TRACE: ACTIVATENEVERWINTER(): Activating Game Client.]")
			
			if (FindClientOpen()) {
				WinActivate
				continue
			} else {
				WinActivate
			}
			
			Sleep 900
			SendEvent {Click 5, 5, 0}
			Sleep 100
			
			while ((!an_logged_in) && (A_Index < 100)) {
				Sleep 5000
				;WakeScreen()
				
				; FIXES SHIFT-STICKING ISSUE
				;Sleep 200
				;SendEvent {Shift down}
				;Sleep 100
				;SendEvent {Shift up}
				;Sleep 200
				
				; FIRST SHIFT TAB
				;SendEvent {Shift down}
				;Sleep 150
				;SendEvent {Tab down}
				;Sleep 150
				;SendEvent {Shift up}
				;Sleep 150
				;SendEvent {Tab up}
				;Sleep 150
				
				; SECOND SHIFT TAB
				;SendEvent {Shift down}
				;Sleep 150
				;SendEvent {Tab down}
				;Sleep 150
				;SendEvent {Shift up}
				;Sleep 150
				;SendEvent {Tab up}
				;Sleep 150
				
				LogAppend("Launcher: Attempting to hit 'Play' button")
				
				; PRESS ENTER (HOPEFULLY ON 'PLAY' BUTTON)
				SendEvent {enter}
				Sleep 200
				
				if (FindClientOpen()) {
					WinActivate
				}
							
				if (FindEWButton() || FindAndClick(Ppb_ImageFile, 1, 0)) {
					an_logged_in := 1
				} else {
					LogAppend("Could Not Find -- FindAndClick(Ppb_ImageFile) || FindEWButton()")
					if (FindClientLoginButton()) {
						LogAppend("ATTEMPTING CLIENT LOGIN")
						ClientLogin()
					}
				}
			}
			Sleep 6000
		}
		sleep 6000
	}
	sleep 6000
}

FindPatcherOpen() {
	if ((WinExist("ahk_class #32770") == "0x0") || (WinExist("ahk_exe Neverwinter.exe") == "0x0")) {
		return 0
	} else {
		return 1
	}
}

FindPatcherLoginButton() {
	global
	return FindAndClick(A_ImagesDir . "\" . "PatcherLoginButtonPart.png", 0, 1)
}

FindPatcherPlayButton() {
	global
	return FindAndClick(A_ImagesDir . "\" . "PatcherPlayButton.png", 0, 1)
}

FindClientOpen() {
	if ((WinExist("ahk_class CrypticWindowClass") == "0x0") || (WinExist("ahk_exe GameClient.exe") == "0x0")) {
		return 0
	} else {
		return 1
	}
}


FindVaultOpen() {
	global
	ImageSearch, ImgX, ImgY, %VpTopLeftX%, %VpTopLeftY%, %VpBotRightX%, %VpBotRightY%, *40 %Vp_ImageFile2%
	; FindAndClick(Vp_ImageFile2, 0, 1)
	if (ImgX) {
		return %ImgX%
	} else {
		ImageSearch, ImgX, ImgY, %VpTopLeftX%, %VpTopLeftY%, %VpBotRightX%, %VpBotRightY%, *40 %Vp_ImageFile1%
		return %ImgX%
		; return FindAndClick(Vp_ImageFile1, 0, 1)		
	}
}

FindLoggedIn() {
	global
	
	; ImageSearch, ImgX, ImgY, %LiTopLeftX%, %LiTopLeftY%, %LiBotRightX%, %LiBotRightY%, *40 %Li_ImageFile%
	; return %ImgX%
	
	return FindAndClick(Li_ImageFile, 0, 1)
}

FindEwButton() {
	global
	
	; ImageSearch, ImgX, ImgY, %EwTopLeftX%, %EwTopLeftY%, %EwBotRightX%, %EwBotRightY%, *40 %Ew_ImageFile%
	; return %ImgX%
	
	return FindAndClick(Ew_ImageFile, 0, 1)
}

FindClientLoginButton() {
	global
	
	; ImageSearch, ImgX, ImgY, %LbTopLeftX%, %LbTopLeftY%, %LbBotRightX%, %LbBotRightY%, *40 %Lb_ImageFile%
	; return %ImgX%	
	; return FindAndClick(Lb_ImageFile, 0, 1)
	
	return FindAndClick(ClientLoginButton_ImageFile, 0, 1)
}

FindConnLogin() {
	global
	
	; ImageSearch, ImgX, ImgY, %ClTopLeftX%, %ClTopLeftY%, %ClBotRightX%, %ClBotRightY%, *40 %Cl_ImageFile%
	; return %ImgX%
	
	return FindAndClick(Cl_ImageFile, 0, 1)
}

FindAltarIcon(deploy := 0) {
	global
	
	altar_icon_found := 0
	fai_tries := 3
	
	while ((!altar_icon_found) && (A_Index <= fai_tries)) {
		ImageSearch, ImgX, ImgY, %AiTopLeftX%, %AiTopLeftY%, %AiBotRightX%, %AiBotRightY%, *30 *TransBlack %Ai_ImageFile%
		if (!ErrorLevel) {
			LogAppend("[Altar Icon Found]")
			if (deploy) {
				ImgX += 5
				ImgY += 5
				Sleep 100
				SendEvent {Click %ImgX%, %ImgY%, 0}
				Sleep 100
				SendEvent {Click}
				Sleep 50
				SendEvent {Click}
				Sleep 100
				altar_icon_found := 1
				break
			} else {
				altar_icon_found := 1
				break
			}
		} else {
			if (A_Index == fai_tries) {
				LogAppend("[Altar Icon Not Found - ErrorLevel:" . ErrorLevel . "]")
			}
			altar_icon_found := 0
		}
	}
	return altar_icon_found
}

FindMaxBlessings() {
	global
	ImageSearch, ImgX, ImgY, %MbTopLeftX%, %MbTopLeftY%, %MbBotRightX%, %MbBotRightY%, *40 %Mb_ImageFile%
	
	if (ImgX) {
		LogAppend("[Maximum blessings window found.]")
	}
	return %ImgX%
}

ClearOkPopupBullshit() {
	global
	if (FindAndClick(OkayUcs_ImageFile, 1, 1) || FindAndClick(OkUcs_ImageFile, 1, 1)) {
		return 1
	}
	return 0
}

ClearSafeLogin() {
	global
	if (FindAndClick(CharSelectSafeLoginButton_ImageFile, 1, 1)) {
		return 1
	}
	return 0
}

FindAndClick(image_file, clk := 1, log := 1) 
{
	global
	
	; LogAppend("[***** TRACE: FINDANDCLICK(): " . image_file . " (log:" . log . ")]")
	
	ImageSearch, ImgX, ImgY, 1, 1, 1920, 1080, *40 %image_file%
	;Sleep 100
	ImgX += 5
	ImgY += 5
	if (!ErrorLevel) {
		if (clk == 1) {
			SendEvent {Click %ImgX%, %ImgY%, 1}
		}
		
		return 1
	} else {
		if (log) {
			el_string := ""
			if (ErrorLevel == 1) {
				el_string = "1 - Image not found on screen"
			} else if (ErrorLevel == 2) {
				el_string = "2 - Image file or ImageSearch option error"
			}
			LogAppend("[FindAndClick - _ImageFile: " . image_file . " - ErrorLevel: " . el_string . "]")
		}
		return 0
	}
}

FindInvokePossible() { ; ->  1: Ready, 0: Not Ready
	global
	i_poss := 0
	error_level := 0
	While (A_Index < 20) {
		ImageSearch, ImgX, ImgY, %InvokeReadyTopLeftX%, %InvokeReadyTopLeftY%, %InvokeReadyBotRightX%, %InvokeReadyBotRightY%, *40 %InvokeNotReady_ImageFile%
		if (!ErrorLevel) {
			i_poss := 0
			error_level := 0
			break
		} else {
			error_level := ErrorLevel
		}
		ImageSearch, ImgX, ImgY, %InvokeReadyTopLeftX%, %InvokeReadyTopLeftY%, %InvokeReadyBotRightX%, %InvokeReadyBotRightY%, *40 %InvokeReady_ImageFile%
		if (!ErrorLevel) {
			i_poss := 1
			error_level := 0
			break
		} else {
			error_level := ErrorLevel
		}
		Sleep 400
	}
	
	if (error_level) {
		LogAppend("[Error Detecting Invocation Readiness]")
	}
	return i_poss
}

CloseVerifyMsgbox() {
	Sleep 200
	;LogAppend("CloseVerifyMsgbox(): WinExist('Verify?') = " .  WinExist("Verify?"))
	if (WinExist("Verify?") <> "0x0") {
		WinActivate
		Sleep 200
		ControlClick, Button2, Verify?, , Left
		Send {enter}
		LogAppend("[Attempting to close 'Verify?' dialogue box]")
	} else {
		return 0
	}
	
	if (WinExist("Verify?") <> "0x0") {
		WinActivate
		LogAppend("['Verify?' is still around I guess... who the hell knows... Attempting to close again...]")
		ControlClick, Button2, Verify?, , Left
		Send {enter}
		return 1
	} else {
		LogAppend("[The 'Verify?' dialogue was apparently closed successfully. Returning 0 and golf clapping]")
		return 0
	}
	
	LogAppend("CloseVerifyMsgbox(): Unreachable section: WinExist('Verify?') = " .  WinExist("Verify?"))
	return -1
}

LogAppend(entry) {
	global
	curr_char_string := ""
	if (!CurrentCharacter) {
		curr_char_string := "Unknown Character"
	} else {
		curr_char_string := "Character " . CurrentCharacter
	}
	entry := GetNowStr() . " ::  " . curr_char_string . " " . entry
	FileAppend, %entry% `n, %ai_log%
	return 1
}

LogNewLine() {
	global
	FileAppend, `n, %ai_log%
	return 1
}

SendHotkey(hotkey) {
	global
	KeyMod := 0
	Key := 0
	Loop, Parse, hotkey
	{
		if (A_LoopField == "^") {
			KeyMod := "ctrl"
		} else if (A_LoopField == "!") {
			KeyMod := "alt"
		} else if (A_LoopField == "+") {
			KeyMod := "shift"
		} else if (A_LoopField == A_Space) {
			KeyMod := ""
		} else {
			Key := A_LoopField
		}
	}
	if (KeyMod <> "") {
		Send {%KeyMod% down}
		Sleep 10
	}
	Send {%Key% down}
	Sleep 10
	Send {%Key% up}
	if (KeyMod <> "") {
		Sleep 10
		Send {%KeyMod% up}
	}
}

GetNowStr() {
	global
	ts := ""
	FormatTime, ts, , yyyy-MM-dd HH:mm:ss
	return ts
}

Ran(Num)
{
   Random, r, -1*Num, Num
   Return r
}

InArray(val,array) {
	global
	
	For i, aval in array
	{
		if (val == aval) {
			return true
		}
	}
	return false
}

ArrayToString(array) {
	global
	out_str := ""
	first_run := 1
	
	For i, aval in array
	{
		if (!first_run) {
			out_str := out_str . ", "
		}
		out_str := out_str . aval
		first_run := 0
	}
	return out_str
}

StringToArray(string) {
	out_array := []
	delim := ","
	Loop, Parse, string , %delim%, %A_Space%%A_Tab%
	{
		out_array.Insert(A_LoopField)
	}
	return out_array
}

WakeScreen() {
	PostMessage, 0x0112, 0xF170, -1,, A
}
