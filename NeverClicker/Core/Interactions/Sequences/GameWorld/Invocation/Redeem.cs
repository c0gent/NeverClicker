using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		public enum VaultOfPietyItem {
			ElixirOfFate = 0,
			BlessedProfessionsElementalPack = 1,
			CofferOfCelestialEnchantments = 2,
			CofferOfCelestialArtifacts = 3,
			CofferOfCelestialArtifactEquipment = 4,
		}

		public static bool Redeem(Interactor intr, uint charIdx) {
			VaultOfPietyItem item;

			try {
				item = (VaultOfPietyItem)Enum.Parse(typeof(VaultOfPietyItem), 
					intr.AccountSettings.GetCharSetting(charIdx, "VaultOfPietyItem"), true);
			} catch (Exception) {
				item = DEFAULT_REDEMPTION_ITEM;
			}

			intr.Log(LogEntryType.Debug, "VaultOfPietyItem: " + item.ToString());

			intr.Wait(500);
			string cursorModeKey = intr.AccountSettings.GetSettingValOr("toggleMouseCursor", "gameHotkeys", 
				Global.Default.ToggleMouseCursor);

			if (Screen.ImageSearch(intr, "InvocationMaximumBlessings").Found) {
				intr.Wait(200);
				Mouse.ClickImage(intr, "InvocationMaximumBlessingsVaultOfPietyButton");
			} else {
				intr.Wait(1000);
				Keyboard.SendKey(intr, cursorModeKey);
				intr.Wait(500);
				bool clicked = false;
				clicked |= Mouse.ClickImage(intr, "InvocationNotReady");
				clicked |= Mouse.ClickImage(intr, "InvocationReady");

				if (!clicked) {
					intr.Log(LogEntryType.FatalWithScreenshot, "Unable to click Vault of Piety button");
				}
			}

			intr.WaitRand(2100, 2500);

			if (!Screen.ImageSearch(intr, "VaultOfPietyWindowTitle").Found) {
				return false;
			}

			Mouse.ClickImage(intr, "VaultOfPietyCelestialSynergyTabTitle");
			intr.Wait(2000);

			string panelImage;
			string purchaseConfirmImage;

			//if (item == VaultOfPietyItem.ElixirOfFate) {
			//	var panel = Screen.ImageSearch(intr, "VaultOfPietyCelestialSynergyElixirOfFate");

			//	if (panel.Found) {
			//		Mouse.DoubleClick(intr, panel.Point);
			//		intr.Wait(500);					
			//		Mouse.ClickImage(intr, "VaultOfPietyElixirOfFateSelectAmountOkButton");
			//		intr.Log(LogEntryType.Info, "Vault of Piety: 'Elixir of Fate' purchased successfully.");
			//	} else {
			//		intr.Log(LogEntryType.Fatal, "Vault of Piety Error: Could not find 'Elixir of Fate' icon/tile.");
			//		return false;
			//	}
			//} else if (item == VaultOfPietyItem.CofferOfCelestialArtifactEquipment) {
			//	var panel = Screen.ImageSearch(intr, "VaultOfPietyCelestialSynergyCofferOfCelestialArtifactEquipment");

			//	if (panel.Found) {
			//		Mouse.DoubleClick(intr, panel.Point);
			//		intr.Wait(500);					
			//		Mouse.ClickImage(intr, "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton");
			//		intr.Log(LogEntryType.Info, "Vault of Piety: 'Coffer of Celestial Artifact Equipment' purchased successfully.");
			//	} else {
			//		intr.Log(LogEntryType.Fatal, "Vault of Piety Error: Could not find 'Coffer of Celestial Artifact Equipment' icon/tile.");
			//		return false;
			//	}				
			//}

			if (item == VaultOfPietyItem.ElixirOfFate) {
				panelImage = "VaultOfPietyCelestialSynergyElixirOfFate";
				purchaseConfirmImage = "VaultOfPietyElixirOfFateSelectAmountOkButton";			
			} else if (item == VaultOfPietyItem.BlessedProfessionsElementalPack) {
				panelImage = "VaultOfPietyCelestialSynergyBlessedProfessionsElementalPack";
				purchaseConfirmImage = "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton";
			} else if (item == VaultOfPietyItem.CofferOfCelestialEnchantments) {
				panelImage = "VaultOfPietyCelestialSynergyCofferOfCelestialEnchantments";
				purchaseConfirmImage = "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton";
			} else if (item == VaultOfPietyItem.CofferOfCelestialArtifacts) {
				panelImage = "VaultOfPietyCelestialSynergyCofferOfCelestialArtifacts";
				purchaseConfirmImage = "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton";
			} else if (item == VaultOfPietyItem.CofferOfCelestialArtifactEquipment) {
				panelImage = "VaultOfPietyCelestialSynergyCofferOfCelestialArtifactEquipment";
				purchaseConfirmImage = "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton";
			} else {
				intr.Log(LogEntryType.Fatal, "Vault of Piety Error: Unknown item: '{0:G}'.", item);
				return false;
			}

			var panel = Screen.ImageSearch(intr, panelImage);

			if (panel.Found) {
				Mouse.DoubleClick(intr, panel.Point);
				intr.Wait(500);					
				Mouse.ClickImage(intr, purchaseConfirmImage);
				intr.Log(LogEntryType.Info, "Vault of Piety: '{0:G}' purchased successfully.", item);
			} else {
				intr.Log(LogEntryType.Fatal, "Vault of Piety Error: Could not find '{0:G}' icon/panel.", item);
				return false;
			}

			// [FIXME]: Handle the fact that the VaultOfPietyItem is:  `5`
			// [FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME]
			// [FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME]

			return true;
		}
	}
}
