using UnityEngine;
using System.Collections;
using Base.UI.State;
using Base.Management;
using System.Collections.Generic;

namespace Base.UI {

    /// <summary>
    /// Switches UIStates.
    /// </summary>
    public class UIStateSelector : MonoBehaviour {

        private static UIStateSelector instance = null;
        /// <summary>
        /// Static reference of the UIStateSelector.
        /// </summary>
        public static UIStateSelector Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(UIStateSelector)) as UIStateSelector;

                }

                if (instance == null) {

                    GameObject go = new GameObject("UIStateSelector");
                    instance = go.AddComponent(typeof(UIStateSelector)) as UIStateSelector;

                }

                return instance;

            }

        }

        /// <summary>
        /// All UIStates that are being used.
        /// </summary>
		public List<ListData> UIStates;

        /// <summary>
        /// The current UIState that is active.
        /// </summary>
        public BaseUIState currentUIState;

        /// <summary>
        /// The UIState that will be entered after the current active state is left.
        /// </summary>
        public BaseUIState nextUIState;

        /// <summary>
        /// The previous UIState that was used.
        /// </summary>
        public BaseUIState previousUIState;

        /// <summary>
        /// The first UIState that will be set active.
        /// </summary>
        public BaseUIState startUIState;

        void Awake () {
            //Disable all UIStates.
			for (int i = 0; i < UIStates.Count; i++) {

				UIStates[i].listedObject.SetActive(false);

            }

            StartCoroutine(SetUIState(startUIState));
            
        }

        public void SetUIState (string _nextState) {

            BaseUIState foundUIState = null;
            //Get the next state.
            for (int i = 0; i < UIStates.Count; i++) {

              
                if (UIStates[i].listedObject.GetComponent<BaseUIState>().GetType().ToString() == "Base.UI.State." + _nextState) {

                    foundUIState = UIStates[i].listedObject.GetComponent<BaseUIState>();

                }

            }

            if(foundUIState == null) {

                Debug.LogError("[ERROR]: UI state [" + _nextState + "] not found.");

            }

            StartCoroutine(SetUIState(foundUIState));

        }

        public IEnumerator SetUIState (BaseUIState _nextState) {

            nextUIState = _nextState;

            if (currentUIState != null) {

                yield return StartCoroutine(currentUIState.Exit());
                currentUIState.Disable();
                previousUIState = currentUIState;

            }

            currentUIState = nextUIState;
            currentUIState.Enter();
            nextUIState = null;

        }

    }

}
