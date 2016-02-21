using UnityEngine;
using System.Collections;

namespace Base.UI.State {

    /// <summary>
    /// Not yet finished. acts as dummy.
    /// </summary>
    public class GameUIState : BaseUIState {

        public override void Enter () {
            base.Enter();
            Game.GameStateSelector.Instance.SetState("InGameState");
        }

    }

}