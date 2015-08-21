using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverClicker.GameClient
{
    class GameClient
    {
        public GameClient()
        {

        }
    }

    public enum State
    {
        Closed,
        Patcher,
        ClientSignIn,
        CharSelect,
        InWorld
    }

    public enum PatcherState
    {
        Signin,
        Patching,
        Ready,
    }

    public enum InWorldState
    {
        InvokeReady,
        BagOpen,
        VaultOpen,
        DialogueBox,
    }

    public enum DialogueBox
    {
        MaxBlessings,
        ResetFeats,
    }

}

