using UnityEngine;
using System.Collections;

namespace Base.Control.Method {

    /// <summary>
    /// BaseClass for InputMethod. InputMethod is used by the InputManager.
    /// </summary>
    public class BaseInputMethod : MonoBehaviour {

        public bool usesMouse;

        public KeyCode primaryKey;

        public virtual Vector2 GetMovementInput () {

            return new Vector2(0, 0);

        }

    }

}