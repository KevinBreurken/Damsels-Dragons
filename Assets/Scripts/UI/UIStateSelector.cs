using UnityEngine;
using System.Collections;
using Base.UI.State;
using Base.Management;
using System.Collections.Generic;

namespace Base.UI {


    /// <summary>
    /// Switches UIStates.
    /// </summary>
    public class UIStateSelector : BaseStateSelector {

        protected static UIStateSelector instance = null;

        /// <summary>
        /// Static reference of the State Selector.
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
        /// The first UIState that will be set active.
        /// </summary>
        public BaseUIState startUIState;

        public void Start () {

            StartCoroutine(SetState(startUIState));

        }

        /// <summary>
        /// Called when a new state is entered.
        /// </summary>
        public override void OnStateEntered () {
            Debug.Log(Effect.EffectManager.Instance.FadeEffect.GetFadeLayerValue());
            if(Effect.EffectManager.Instance.FadeEffect.GetFadeLayerValue() >= 0.9f) {

                StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(0));

            }

        }

    }

}
