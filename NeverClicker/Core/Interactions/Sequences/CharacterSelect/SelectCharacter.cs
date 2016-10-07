using NeverClicker.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		//const int DEFAULT_CHARACTER_SELECT_SCROLL_BAR_TOP_X = 840;
		//const int DEFAULT_CHARACTER_SELECT_SCROLL_BAR_TOP_Y = 108;
		const int SCROLLS_PER_TILE = 4;
		const int TILE_SIZE = 80;
		//const int TILE_SIZE = 78;

		public static bool SelectCharacter(Interactor intr, uint charIdx, bool enterWorld) {
			if (intr.CancelSource.IsCancellationRequested) { return false; }

			intr.Log(LogEntryType.Info, "Selecting character " + charIdx.ToString() + " ...");

			int charCount = intr.AccountSettings.GetSettingValOr("CharacterCount", "General", 0);

			//int scrollBarTopX = intr.GameClient.GetSettingOrZero("CharacterSelectScrollBarTopX", "ClickLocations");
			//int scrollBarTopY = intr.GameClient.GetSettingOrZero("CharacterSelectScrollBarTopY", "ClickLocations");
			//int charSlotX = intr.GameClient.GetSettingOrZero("CharSlotX", "ClickLocations");
			//int topSlotY = intr.GameClient.GetSettingOrZero("TopSlotY", "ClickLocations");
			//int visibleSlots = intr.GameClient.GetSettingOrZero("VisibleCharacterSelectSlots", "KeyBindAndUi");
			//int scrollsAlignBot = intr.GameClient.GetSettingOrZero("ScrollsToAlignBottomSlot", "KeyBindAndUi");

			var charSel = new Global.ClientCharSelectDefaults(new Point(1920, 1080));

			int scrollBarTopX = intr.ClientSettings.GetSettingValOr("ScrollBarTopX", "CharacterSelect", charSel.ScrollBarTopX);
			int scrollBarTopY = intr.ClientSettings.GetSettingValOr("ScrollBarTopY", "CharacterSelect", charSel.ScrollBarTopY);
			int charSlotX = intr.ClientSettings.GetSettingValOr("CharSlotX", "CharacterSelect", charSel.CharSlotX);
			int topSlotY = intr.ClientSettings.GetSettingValOr("TopSlotY", "CharacterSelect", charSel.TopSlotY);
			int visibleSlots = intr.ClientSettings.GetSettingValOr("VisibleSlots", "CharacterSelect", charSel.VisibleSlots);
			int scrollsAlignBot = intr.ClientSettings.GetSettingValOr("ScrollsToAlignBottomSlot", "CharacterSelect", charSel.ScrollsToAlignBottomSlot);

			if ((charCount == 0) || (charSlotX == 0) || (topSlotY == 0) || (visibleSlots == 0)
						|| (scrollBarTopX == 0) || (scrollBarTopY == 0)) {
				intr.Log(LogEntryType.Fatal, "SelectCharacter(): Error loading ini file settings");
				return false;
			}

			int botSlotY = topSlotY + (TILE_SIZE * (visibleSlots - 1)) - (TILE_SIZE / 2);
			bool mustScroll = false;
			int scrolls = 0;
			int clickY = 0;

			if (charIdx < (visibleSlots - 1)) {
				clickY = topSlotY + (TILE_SIZE * ((int)charIdx));				
			} else {
				mustScroll = true;				
				clickY = botSlotY;
				scrolls = (SCROLLS_PER_TILE * ((int)charIdx - (visibleSlots - 1))) + scrollsAlignBot;
			}

			int scrollUpClicks = (8 / visibleSlots) * 9;

			for (int i = 0; i < scrollUpClicks; i++) {
				Mouse.Click(intr, scrollBarTopX, scrollBarTopY);
				//intr.Wait(10);
			}
			
			Mouse.WheelUp(intr, 5);

			Mouse.Move(intr, charSlotX, clickY);
			intr.Wait(200);

			if (mustScroll) {
				Mouse.WheelDown(intr, scrolls);
			}

			if (!enterWorld) { return true; }

			intr.Wait(400);

			Mouse.DoubleClick(intr, charSlotX, clickY);

			ClearSafeLogin(intr);
			ClearDialogues(intr);
			intr.Wait(3000);

			// Determine if login has been a success:
			if (!intr.WaitUntil(90, ClientState.InWorld, States.IsClientState, CharSelectFailure, 0)) {
				// [NOTE]: Look into eliminating this recursion and moving control back up and iterating rather than delving deeper.
				ProduceClientState(intr, ClientState.CharSelect, 0);
				SelectCharacter(intr, charIdx, enterWorld);
				//return false;
			}

			// [TODO]: This should happen in the 'World Verification' loop:
			ClearDialogues(intr);

			return true;
		}

		public static bool CharSelectFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log("Failure to select character. Retrying...");
			return false;
		}
	}
}
