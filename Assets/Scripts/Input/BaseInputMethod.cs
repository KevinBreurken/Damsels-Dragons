using UnityEngine;
using System.Collections;

namespace Base.Control.Method {

    /// <summary>
    /// BaseClass for InputMethod. InputMethod is used by the InputManager.
    /// </summary>
    public class BaseInputMethod : MonoBehaviour {

        public delegate void InputEvent ();

        /// <summary>
        /// Called when the AudioObject is finished playing.
        /// </summary>
        public event InputEvent onJumpPressed;

		public KeyCode downInputKey;

        /// <summary>
        /// Checks what the horizontal movement input is.
        /// </summary>
		public virtual float GetMovementInput () {

			return 0.0f;

        }

        /// <summary>
        /// Checks if the down key is pressed.
        /// </summary>
        /// <returns></returns>
		public virtual bool GetDownInput () {

			return Input.GetKey(downInputKey);

		}

        public virtual void Update () { }

        /// <summary>
        /// Fires the jump event.
        /// </summary>
        public void FireJumpEvent () {

            if(onJumpPressed != null) {

                onJumpPressed();

            }

        }

    }

}