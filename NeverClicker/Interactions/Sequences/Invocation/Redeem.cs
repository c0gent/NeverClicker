using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public static void Redeem(Interactor intr, int item) {
			intr.Wait(500);

			//int item = 1; // REPLACE WITH ENUM

			string cursorModeKey = intr.GameAccount.GetSettingOrEmpty("NwCursorMode", "GameHotkeys");

			//Point MaxBlessVaultButton = new Point(
			//	intr.GameClient.GetSettingOrZero("VpMaxBlessVpX", "ClickLocations"),
			//	intr.GameClient.GetSettingOrZero("VpMaxBlessVpY", "ClickLocations")
   //         );

			//Point VaultOpenButton = new Point(
			//	intr.GameClient.GetSettingOrZero("VpButtonX", "ClickLocations"),
			//	intr.GameClient.GetSettingOrZero("VpButtonY", "ClickLocations")
   //         );

			Point VaultCelestialTab = new Point(
				intr.GameClient.GetSettingOrZero("VpCsTabX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpCsTabY", "ClickLocations")
            );

			Point VaultCelestialAEPanel = new Point( // 5
				intr.GameClient.GetSettingOrZero("VpCcaePanelX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpCcaePanelY", "ClickLocations")
            );

			Point VaultCelestialEOFPanel = new Point( // 1
				intr.GameClient.GetSettingOrZero("VpEofPanelX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpEofPanelY", "ClickLocations")
            );			

			Point VaultCelestialRedeemButton = new Point(
				intr.GameClient.GetSettingOrZero("VpCsRedeemX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpCsRedeemY", "ClickLocations")
            );

			Point VaultPurchaseOkButton = new Point(
				intr.GameClient.GetSettingOrZero("VpCwaPurchaseOkX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpCwaPurchaseOkY", "ClickLocations")
            );

			Point VaultPurchaseAmtOkButton = new Point(
				intr.GameClient.GetSettingOrZero("VpEofAmtOkX", "ClickLocations"),
				intr.GameClient.GetSettingOrZero("VpEofAmtOkY", "ClickLocations")
            );


			if (Screen.ImageSearch(intr, "InvocationMaximumBlessings").Found) {
				intr.Wait(200);
				//Mouse.Click(intr, MaxBlessVaultButton);
				Mouse.ClickImage(intr, "InvocationMaximumBlessingsVaultOfPietyButton");
			} else {
				intr.Wait(1000);
				//if (Screen.ImageSearch(intr, "InvocationNotReady").Found ||) {
				//MoveAround(intr);
				Keyboard.SendKey(intr, cursorModeKey);
				intr.Wait(500);
				//Mouse.Click(intr, VaultOpenButton);
				bool clicked = false;
				clicked |= Mouse.ClickImage(intr, "InvocationNotReady");
				clicked |= Mouse.ClickImage(intr, "InvocationReady");

				if (!clicked) {
					intr.Log("Unable to click Vault of Piety button", LogEntryType.FatalWithScreenshot);
				}
			}

			intr.WaitRand(2500, 4500);

			Mouse.Click(intr, VaultCelestialTab);
			intr.Wait(2000);

			if (item == 5) {
				Mouse.Click(intr, VaultCelestialAEPanel);
				intr.Wait(200);
				Mouse.Click(intr, VaultCelestialAEPanel);
				intr.Wait(500);

				Mouse.Click(intr, VaultCelestialRedeemButton);
				intr.Wait(500);

				Mouse.Click(intr, VaultPurchaseOkButton);
				intr.Wait(1500);

				intr.Log("Vault of Piety item 5 purchased", LogEntryType.Info);
			} else if (item == 1) {
				Mouse.Click(intr, VaultCelestialEOFPanel);
				intr.Wait(200);
				Mouse.Click(intr, VaultCelestialEOFPanel);
				intr.Wait(500);

				Mouse.Click(intr, VaultCelestialRedeemButton);
				intr.Wait(500);

				Mouse.Click(intr, VaultPurchaseAmtOkButton);
				intr.Wait(1500);

				intr.Log("Vault of Piety item 1 purchased", LogEntryType.Info);
			}

			return;
		}
	}
}

//Redeem(vault_purchase) {
//	global
//	Sleep 1000
//	LogAppend("[Redeeming Item: " . vault_purchase . "]")
//	while (!(FindVaultOpen()) && (A_Index <= 5)) {
//		Sleep 1000
//		if (A_Index > 5) {
//			msgbox Redeem(): Problem finding vault window.
//			exit
//		}
//		if (FindMaxBlessings()) {
//			Sleep 200
//			SendEvent {Click %VpMaxBlessVpX%, %VpMaxBlessVpY%, 1}
//			Sleep 500
//		} else { 
//			Sleep 1000
//			MoveAround()
//			Sleep 500
//			Send {%NwCursorMode%}
//			Sleep 500
//			SendEvent {Click %VpButtonX%, %VpButtonY%, 1}
//			Sleep 2500
//		}
//	}
	
//	SendEvent {Click %VpCsTabX%, %VpCsTabY%, 1}
//	Sleep 600
		
//	if (vault_purchase == 3) {
//		; CLICK TWICE 'CAUSE
//		Sleep 400
//		SendEvent {Click %VpEofPanelX%, %VpEofPanelY%, 1}
//		Sleep 400
//		SendEvent {Click %VpEofPanelX%, %VpEofPanelY%, 1}
//		Sleep 400
		
//		SendEvent {Click %VpCsRedeemX%, %VpCsRedeemY%, 1}
//		Sleep 400
//		SendEvent {Click %VpEofAmtOkX%, %VpEofAmtOkY%, 1}
//		Sleep 400
//	} else if (vault_purchase == 5) {
//		; CLICK TWICE 'CAUSE
//		Sleep 400
//		SendEvent {Click %VpCcaePanelX%, %VpCcaePanelY%, 1}
//		Sleep 400
//		SendEvent {Click %VpCcaePanelX%, %VpCcaePanelY%, 1}
//		Sleep 400
		
//		Sleep 400
//		SendEvent {Click %VpCsRedeemX%, %VpCsRedeemY%, 1}
//		Sleep 400
//		SendEvent {Click %VpCwaPurchaseOkX%, %VpCwaPurchaseOkY%, 1}
//		Sleep 400
//	} else {
//		msgbox Unknown Redeem(vault_purchase)
//		exit
//	}
	
//	return "[Vault of Piety Item #" . vault_purchase . " Purchased]"
//}


//	FindMaxBlessings() {
//	global
//	ImageSearch, ImgX, ImgY, %MbTopLeftX%, %MbTopLeftY%, %MbBotRightX%, %MbBotRightY%, *40 %Mb_ImageFile%
	
//	if (ImgX) {
//		LogAppend("[Maximum blessings window found.]")
//	}
//	return %ImgX%
//}