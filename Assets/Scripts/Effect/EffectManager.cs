using UnityEngine;
using System.Collections;

namespace Base.Effect {

    public class EffectManager : MonoBehaviour {

        private static EffectManager instance = null;
        /// <summary>
        /// Static reference of the EffectManager.
        /// </summary>
        public static EffectManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(EffectManager)) as EffectManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("EffectManager");
                    instance = go.AddComponent(typeof(EffectManager)) as EffectManager;

                }

                return instance;

            }

        }

        public FadeEffect FadeEffect;

    }

}
