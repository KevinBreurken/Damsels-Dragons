using UnityEngine;
using System.Collections;

namespace Base.UI.State {

    /// <summary>
    /// BaseClass of UIState. UIStates are opened / closed by the UIStateSelector.
    /// </summary>
    public class BaseUIState : MonoBehaviour {

        /// <summary>
        /// Closes this UI state.
        /// </summary>
        public virtual IEnumerator Exit () {
            
            yield break;

        }

        /// <summary>
        /// Called on each Update.
        /// </summary>
        public virtual void Update () {

        }

        /// <summary>
        /// Opens this UI state.
        /// </summary>
        public virtual void Enter () {

            this.gameObject.SetActive(true);

        }

        /// <summary>
        /// Disables this UI state.
        /// </summary>
        public void Disable () {

            this.gameObject.SetActive(false);

        }

    }

}