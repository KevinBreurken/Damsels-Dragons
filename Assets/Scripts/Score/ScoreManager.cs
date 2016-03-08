using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Base.Score {

    public class ScoreManager : MonoBehaviour {

        protected static ScoreManager instance = null;

        /// <summary>
        /// Static reference of the State Selector.
        /// </summary>
        public static ScoreManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("ScoreManager");
                    instance = go.AddComponent(typeof(ScoreManager)) as ScoreManager;

                }

                return instance;

            }

        }

        public int currentMatchScore;
        public Text scoreText;
        public Camera gameCamera;

        void Update () {

            scoreText.text = "" + currentMatchScore;

        }

        public void AddScore (int _value) {

            currentMatchScore += _value;

        }

        

    }

}
