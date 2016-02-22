using UnityEngine;
using System.Collections;
using Base.Control;

namespace Base.Control.Method {

    /// <summary>
    /// Holds all keyboard related Inputs.
    /// </summary>
    public class KeyboardInputMethod : BaseInputMethod {

		public override float GetMovementInput ()
		{
			float movementInput = 0;
			if(Input.GetKey(KeyCode.A)){
				movementInput -= 1;
			}
			if(Input.GetKey(KeyCode.D)){
				movementInput += 1;
			}

			return movementInput;
		}
    }

}
