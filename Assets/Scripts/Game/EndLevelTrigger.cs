using UnityEngine;
using System.Collections;
using Base.Game.State;

namespace Base.Game {

    public class EndLevelTrigger : MonoBehaviour {

        public InGameState gameState;
        private Collider2D triggerCollider;

        void Awake () {

            triggerCollider = GetComponent<Collider2D>();
            gameState = (InGameState)GameStateSelector.Instance.GetState("InGameState");

        }

        /// <summary>
        /// Called when the player hits this trigger.
        /// </summary>
        public void SendEndMessage () {

            triggerCollider.enabled = false;
            gameState.EndLevel();

        }

        public void ResetTrigger () {

            triggerCollider.enabled = true;

        }

    }

}
