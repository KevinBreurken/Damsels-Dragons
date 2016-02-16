using UnityEngine;
using System.Collections;

namespace Base {

    /// <summary>
    /// BaseClass of State. States are used for UIStates and GameStates.
    /// </summary>
    public class BaseState : MonoBehaviour {

        /// <summary>
        /// Closes this state.
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
        /// Opens this state.
        /// </summary>
        public virtual void Enter () {

            this.gameObject.SetActive(true);

        }

        /// <summary>
        /// Disables this state.
        /// </summary>
        public void Disable () {

            this.gameObject.SetActive(false);

        }

    }

}
