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
		private int matchScore = 0;
        public Text scoreText;
        public Camera gameCamera;
        private float startXPosition;
        private float distanceScore;

        void Update () {

            distanceScore = gameCamera.transform.position.x - startXPosition;

            int displayScore = (currentMatchScore + (int)distanceScore);
            if (displayScore < 0)
                displayScore = 0;

            scoreText.text = "" + displayScore;

        }

        public void AddScore (int _value) {

            currentMatchScore += _value;

        }

        public void ResetScore () {

            currentMatchScore = 0;
            startXPosition = gameCamera.transform.position.x;

        }
        
		public void FinaliseScore(){
			
			distanceScore = gameCamera.transform.position.x - startXPosition;
			matchScore =  (currentMatchScore + (int)distanceScore);

		}

		public int GetScore () {
			
			return matchScore;

		}

    }

}
