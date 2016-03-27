using UnityEngine;
using System.Collections;
using Base.Management;
using System.Collections.Generic;

namespace Base {

    public class BaseStateSelector : MonoBehaviour {

        /// <summary>
        /// All UIStates that are being used.
        /// </summary>
        public List<ListData> States;

        /// <summary>
        /// The current UIState that is active.
        /// </summary>
        public BaseState currentState;

        /// <summary>
        /// The UIState that will be entered after the current active state is left.
        /// </summary>
        public BaseState nextState;

        /// <summary>
        /// The previous UIState that was used.
        /// </summary>
        public BaseState previousState;

        public virtual void Awake () {
            //Disable all UIStates.
            for (int i = 0; i < States.Count; i++) {

                States[i].listedObject.SetActive(false);

            }      

        }
        
        /// <summary>
        /// Gets the state by its name.
        /// </summary>
        /// <param name="_stateName">The name of the state.</param>
        /// <returns>The found state.</returns>
        public BaseState GetState(string _stateName) {

            BaseState foundState = null;
            //Get the next state.
            for (int i = 0; i < States.Count; i++) {

                if (States[i].listedObject.GetComponent<BaseState>().GetType().ToString().Contains(_stateName)) {

                    foundState = States[i].listedObject.GetComponent<BaseState>();

                }

            }

            return foundState;

        }

        /// <summary>
        /// Changes the State by its name.
        /// </summary>
        /// <param name="_nextState">the name of the state.</param>
        public void SetState (string _nextState) {

            BaseState foundState = null;
            //Get the next state.
            for (int i = 0; i < States.Count; i++) {

                if (States[i].listedObject.GetComponent<BaseState>().GetType().ToString().Contains(_nextState)) {

                    foundState = States[i].listedObject.GetComponent<BaseState>();

                }

            }

            if (foundState == null) {

                Debug.LogError("[ERROR]: state [" + _nextState + "] not found.");

            }

            StartCoroutine(SetState(foundState));
            
        }

        /// <summary>
        /// Changes the State.
        /// </summary>
        /// <param name="_nextState">The new state.</param>
        public IEnumerator SetState (BaseState _nextState) {

            if(_nextState == currentState) {

                Debug.Log("State " + _nextState.name + " is already open.");

            }

            nextState = _nextState;

            if (currentState != null) {

                yield return StartCoroutine(currentState.Exit());
                currentState.Disable();
                previousState = currentState;

            }

            currentState = nextState;
            currentState.Enter();
            nextState = null;
            OnStateEntered();

        }

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public virtual void OnStateEntered () {

        }

    }

}