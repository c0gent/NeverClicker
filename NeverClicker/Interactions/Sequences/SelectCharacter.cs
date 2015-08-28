using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.Interactions {
	public static partial class Sequences {

		public static bool SelectCharacter(Interactor itr, int charIdx) {
			itr.Log("Selecting character: " + charIdx.ToString() + ".");

			return true;
		}
	}
}


//SelectCharacter(n_invoke, scroll_top, skip_char) {
//	global

//	LogAppend("[Character:".CurrentCharacter. ", skip_char:".skip_char. ", TopSlotY:".TopSlotY. "]")
//	ClickX:= CharSlotX
//    must_scroll:= 0
//    scroll_count:= 0
//   ; clickable_slots:= 8
//    clickable_slots:= VisibleCharacterSelectSlots
//    BotSlotY:= TopSlotY + 45 + (70 * (clickable_slots - 1))

//	Sleep 500 + Ran(100)

//	if (n_invoke > clickable_slots) {
//	must_scroll:= 1
//    }

//	if (must_scroll) {
//	ScrollChunks:= Floor((n_invoke - (clickable_slots + 1)) / 2)
//        Position:= Mod(n_invoke, 2)
//        scroll_count:= (7 * ScrollChunks) + 5
//        ClickY:= (BotSlotY - (70 * Position))
//    } else {
//	ClickY:= TopSlotY + (70 * (n_invoke - 1))
//    }

//	if (scroll_top) {

//		Sleep 150

//		Loop 9 {
//			SendEvent { Click % CharacterSelectScrollBarTopX %, % CharacterSelectScrollBarTopY %, 1}
//			Sleep 100
//        }

//		Loop 20 {
//			SendEvent { Click WheelUp}
//			Sleep 100
//        }

//		Sleep 40

//		; Loop 140 {
//			; SendEvent { Click WheelUp}
//		 ; Sleep 20
//	   ;
//		}
//		; sleep 200
//    }

//	LogAppend("[SelectCharacter():".CurrentCharacter. ", ClickX:".ClickX. ", ClickY:".ClickY. "]")

//	SendEvent { Click % ClickX %, % ClickY %, 0}
//	Sleep 170

//	if (must_scroll) {
//		Loop % scroll_count % {
//			SendEvent { Click WheelDown}
//			Sleep 25
//          }
//	}


//	; ---LOG IN-- -
//    if (skip_char <> 1) {
//		; SendEvent { Click % EwButtonX %, % EwButtonY %, 1}

//		SendEvent { Click % ClickX %, % ClickY %, 1}
//		Sleep 80
//        SendEvent { Click % ClickX %, % ClickY %, 1}
//		Sleep 370

//		Send { Enter}
//		Sleep AfterCharSelectDelay +Ran(120)

//	} else {

//		Sleep 50
//        SendEvent { Click % ClickX %, % ClickY %, 1}
//		Sleep 170
//        return 0
//    }

//	return 1
//}