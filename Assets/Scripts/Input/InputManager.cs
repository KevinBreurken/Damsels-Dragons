using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Management;
using Base.Control.Method;

namespace Base.Control {

    /// <summary>
    /// Handles everything input related.
    /// Input method is changed by calling SetInputMethod().
    /// </summary>
    public class InputManager : MonoBehaviour {

        private static InputManager instance = null;
        /// <summary>
        /// Static reference of the InputManager.
        /// </summary>
        public static InputManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(InputManager)) as InputManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("InputManager");
                    instance = go.AddComponent(typeof(InputManager)) as InputManager;

                }

                return instance;

            }

        }

		public delegate void InputChangeEvent (BaseInputMethod _changedMethod);

        /// <summary>
        /// Called when the input method is changed.
        /// </summary>
		public event InputChangeEvent onInputChanged;

        /// <summary>
        /// List of all possible input methods.
        /// </summary>
        public List<ListData> inputMethods;

        /// <summary>
        /// The inputMethod that will be used by default.
        /// </summary>
        public BaseInputMethod startInputMethod;

        private BaseInputMethod currentlyUsedInputMethod;

        void Awake () {

            SetInputMethod(startInputMethod.gameObject.name);

        }

        /// <summary>
        /// Sets the input method.
        /// </summary>
        /// <param name="_inputName">The name of the input method. (by its GameObject name)</param>
        public void SetInputMethod (string _inputName) {

            for (int i = 0; i < inputMethods.Count; i++) {

                if(inputMethods[i].listedObject.name == _inputName) {

                    currentlyUsedInputMethod = inputMethods[i].listedObject.GetComponent<BaseInputMethod>();
                    break;

                }

            }

        }

        /// <summary>
        /// Returns the currently used Input Method.
        /// </summary>
		public BaseInputMethod GetCurrentInputMethod () {
			
			return currentlyUsedInputMethod;

		}


    }

}
