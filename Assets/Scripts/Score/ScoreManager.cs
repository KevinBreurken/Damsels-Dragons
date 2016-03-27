using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Base.Score {

    /// <summary>
    /// Handles the score that is achieved in the game.
    /// </summary>
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

        /// <summary>
        /// How high the score currently is.
        /// </summary>
        public int currentMatchScore;

        /// <summary>
        /// The UI display of the score.
        /// </summary>
        public Text scoreText;

        /// <summary>
        /// Reference to the game view camera.
        /// </summary>
        public Camera gameViewCamera;

        private int matchScore = 0;
        private float startXPosition;
        private float distanceScore;

        void Update () {

            distanceScore = gameViewCamera.transform.position.x - startXPosition;

            int displayScore = (currentMatchScore + (int)distanceScore);
            if (displayScore < 0)
                displayScore = 0;

            scoreText.text = "" + displayScore;

        }

        /// <summary>
        /// Adds a given value to the score.
        /// </summary>
        /// <param name="_value">The value to be added.</param>
        public void AddScore (int _value) {

            currentMatchScore += _value;

        }

        /// <summary>
        /// Resets the score back to 0.
        /// </summary>
        public void ResetScore () {

            currentMatchScore = 0;
            matchScore = 0;
            startXPosition = gameViewCamera.transform.position.x;

        }
        
        /// <summary>
        /// Combines the distance the player has ran with the rest of the score.
        /// </summary>
		public void FinaliseScore(){
			
			distanceScore = gameViewCamera.transform.position.x - startXPosition;
			matchScore =  (currentMatchScore + (int)distanceScore);

		}

        /// <summary>
        /// Returns the score currently made.
        /// </summary>
        /// <returns></returns>
		public int GetScore () {
			
			return matchScore;

		}

    }

}
