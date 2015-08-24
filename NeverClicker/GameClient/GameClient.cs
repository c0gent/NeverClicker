using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.GameClient {
	class Instance {
		Interactions.Interactor Interactor;

		public Instance(Interactions.Interactor interactions) {
			//CurrentState = State.Closed;
			Interactor = interactions;
		}

		//public State CurrentState()
		//{
		//    State currentState;
		//    if (Interactor.ImageSearch()

		//}
	}

	public enum State {
		Closed,
		Patcher,
		ClientSignIn,
		CharSelect,
		InWorld
	}

	public enum PatcherState {
		Signin,
		Patching,
		Ready,
	}

	public enum InWorldState {
		InvokeReady,
		BagOpen,
		VaultOpen,
		DialogueBox,
	}

	public enum DialogueBox {
		MaxBlessings,
		ResetFeats,
	}

}

