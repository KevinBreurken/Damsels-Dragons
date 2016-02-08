using UnityEngine;
using System.Collections;

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
        public BaseUIState[] UIStates;

        /// <summary>
        /// The current UIState that is active.
        /// </summary>
        public BaseUIState currentUIState;
        public BaseUIState nextUIState;
        public BaseUIState previousUIState;

        /// <summary>
        /// The first UIState that will be set active.
        /// </summary>
        public BaseUIState startUIState;

        void Awake () {
            //Disable all UIStates.
            for (int i = 0; i < UIStates.Length; i++) {

                UIStates[i].gameObject.SetActive(false);

            }

            SetUIState(startUIState);
            
        }

        private void SetUIState(BaseUIState _nextState) {
            string nextString = _nextState.GetType().ToString().Remove(0, 8);
            StartCoroutine(SetUIState(nextString));
        }

        /// <summary>
        /// Sets the UI to the given state.
        /// </summary>
        /// <param name="_nextState">The name of the next state. (as its Class name)</param>
        public IEnumerator SetUIState(string _nextState) {

            //Get the next state.
            for (int i = 0; i < UIStates.Length; i++) {

                if (UIStates[i].GetType().ToString() == "Base.UI." + _nextState) {
                  
                    nextUIState = UIStates[i];

                }

            }

            if (currentUIState != null) {
        
                yield return StartCoroutine(currentUIState.Exit());
                currentUIState.Disable();
                previousUIState = currentUIState;

            }

            currentUIState = nextUIState;
            nextUIState.Enter();
            nextUIState = null;

        }

    }

}
