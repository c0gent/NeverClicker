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

		public static bool Redeem(Interactor intr, VaultOfPietyItem item) {
			// Clamp `item` to 4.
			if ((int)item > 4 || (int)item < 0) {
				item = VaultOfPietyItem.CofferOfCelestialArtifactEquipment;
			}

			intr.Wait(500);
			string cursorModeKey = intr.AccountSettings.GetSettingValOr("ToggleMouseCursor", "GameHotkeys", 
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
					intr.Log("Unable to click Vault of Piety button", LogEntryType.FatalWithScreenshot);
				}
			}

			intr.WaitRand(2100, 2500);

			if (!Screen.ImageSearch(intr, "VaultOfPietyWindowTitle").Found) {
				return false;
			}

			Mouse.ClickImage(intr, "VaultOfPietyCelestialSynergyTabButton");
			intr.Wait(2000);

			if (item == VaultOfPietyItem.ElixirOfFate) {
				var panel = Screen.ImageSearch(intr, "VaultOfPietyElixirOfFatePanel");

				if (panel.Found) {
					Mouse.DoubleClick(intr, panel.Point);
					intr.Wait(500);					
					Mouse.ClickImage(intr, "VaultOfPietyElixirOfFateSelectAmountOkButton");
					intr.Log("Vault of Piety: 'Elixir of Fate' purchased successfully.", LogEntryType.Info);
				} else {
					intr.Log("Vault of Piety Error: Could not find 'Elixir of Fate' icon/tile.", LogEntryType.Fatal);
					return false;
				}
			} else if (item == VaultOfPietyItem.CofferOfCelestialArtifactEquipment) {
				var panel = Screen.ImageSearch(intr, "VaultOfPietyCofferOfCelestialArtifactEquipmentPanel");

				if (panel.Found) {
					Mouse.DoubleClick(intr, panel.Point);
					intr.Wait(500);					
					Mouse.ClickImage(intr, "VaultOfPietyCofferOfCelestialArtifactEquipmentPurchaseConfirmOkButton");
					intr.Log("Vault of Piety: 'Coffer of Celestial Artifact Equipment' purchased successfully.", LogEntryType.Info);
				} else {
					intr.Log("Vault of Piety Error: Could not find 'Coffer of Celestial Artifact Equipment' icon/tile.", LogEntryType.Fatal);
					return false;
				}				
			} 

			// [FIXME]: Handle the fact that the VaultOfPietyItem is:  `5`
			// [FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME]
			// [FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME][FIX THE HELL OUT OF ME]

			return true;
		}
	}
}
