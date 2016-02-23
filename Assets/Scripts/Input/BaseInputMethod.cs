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

        public bool usesMouse;

		public virtual float GetMovementInput () {

			return 0.0f;

        }

        public virtual void Update () {
            
        }

        public void FireJumpEvent () {

            if(onJumpPressed != null) {

                onJumpPressed();

            }

        }

    }

}