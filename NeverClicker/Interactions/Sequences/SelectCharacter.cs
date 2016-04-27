using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {
		const int SCROLLS_PER_TILE = 4;
		const int TILE_SIZE = 80;

		public static bool SelectCharacter(Interactor intr, uint charIdx, bool enterWorld, int loginAttemptCount) {
			if (intr.CancelSource.IsCancellationRequested) { return false; }

			intr.Log("Selecting character " + charIdx.ToString() + " ...", LogEntryType.Info);

			int maxChars = intr.GameAccount.GetSettingOrZero("CharCount", "NwAct");
			int charSlotX = intr.GameClient.GetSettingOrZero("CharSlotX", "ClickLocations");
			int topSlotY = intr.GameClient.GetSettingOrZero("TopSlotY", "ClickLocations");
			int visibleSlots = intr.GameClient.GetSettingOrZero("VisibleCharacterSelectSlots", "KeyBindAndUi");
			int scrollsAlignBot = intr.GameClient.GetSettingOrZero("ScrollsToAlignBottomSlot", "KeyBindAndUi");
			int scrollBarTopX = intr.GameClient.GetSettingOrZero("CharacterSelectScrollBarTopX", "KeyBindAndUi");
			int scrollBarTopY = intr.GameClient.GetSettingOrZero("CharacterSelectScrollBarTopY", "KeyBindAndUi");

			if ((maxChars == 0) || (charSlotX == 0) || (topSlotY == 0) || (visibleSlots == 0)
						|| (scrollBarTopX == 0) || (scrollBarTopY == 0)) {
				intr.Log("SelectCharacter(): Error loading ini file settings", LogEntryType.Fatal);
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

			for (int i = 0; i < 9; i++) {
				Mouse.Click(intr, scrollBarTopX, scrollBarTopY);
				intr.Wait(10);
			}
			
			Mouse.WheelUp(intr, 5);

			Mouse.Move(intr, charSlotX, clickY);
			intr.Wait(100);

			if (mustScroll) {
				Mouse.WheelDown(intr, scrolls);
			}

			if (!enterWorld) { return true; }

			Mouse.DoubleClick(intr, charSlotX, clickY);

			ClearSafeLogin(intr);
			ClearDialogues(intr);
			intr.Wait(3000);

			if (!intr.WaitUntil(90, ClientState.InWorld, Game.IsClientState, CharSelectFailure, loginAttemptCount)) {
				ProduceClientState(intr, ClientState.CharSelect, loginAttemptCount);
				SelectCharacter(intr, charIdx, enterWorld, loginAttemptCount);
			}
			ClearDialogues(intr);
			return true;
		}

		public static bool CharSelectFailure<TState>(Interactor intr, TState state, int attemptCount) {
			intr.Log("Failure to select character. Retrying...");
			return false;
		}
	}
}
