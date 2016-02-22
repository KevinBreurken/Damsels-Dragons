using UnityEngine;
using System.Collections;

namespace Base.Control.Method {

    /// <summary>
    /// BaseClass for InputMethod. InputMethod is used by the InputManager.
    /// </summary>
    public class BaseInputMethod : MonoBehaviour {

        public bool usesMouse;

        public KeyCode primaryKey;

		public virtual float GetMovementInput () {

			return 0.0f;

        }

    }

}