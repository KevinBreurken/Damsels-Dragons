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
        private float startXPosition;
        private float distanceScore;

        void Update () {

            distanceScore = gameCamera.transform.position.x - startXPosition;
            scoreText.text = "" + (currentMatchScore + (int)distanceScore);

        }

        public void AddScore (int _value) {

            currentMatchScore += _value;

        }

        public void ResetScore () {

            currentMatchScore = 0;
            startXPosition = gameCamera.transform.position.x;

        }
        

    }

}
