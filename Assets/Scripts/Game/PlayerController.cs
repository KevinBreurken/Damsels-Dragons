using UnityEngine;
using System.Collections;
using Base.Control;
using Base.Control.Method;

namespace Base.Game {
	
	public class PlayerController : MonoBehaviour {

		private BaseInputMethod inputMethod;
		public float movementSpeedFactor = 1.5f;
		void Awake () {
			
			InputManager.Instance.onInputChanged += OnInputMethodChanged;
			inputMethod = InputManager.Instance.GetCurrentInputMethod();

		}

		void Update () {
			
			transform.Translate(((Vector2.right * inputMethod.GetMovementInput()) * movementSpeedFactor) * Time.deltaTime);

		}

		/// <summary>
		/// Called when the input method is changed.
		/// </summary>
		public void OnInputMethodChanged (BaseInputMethod _newMethod) {

			inputMethod = _newMethod;

		}

	}

}