using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Score {

    /// <summary>
    /// Handles loading and saving highscore data.
    /// </summary>
    public class HighScoreManager : MonoBehaviour {

        /// <summary>
        /// Default Highscore used when clearing playerprefs.
        /// </summary>
        public List<HighScore> factoryHighscores = new List<HighScore>();

        /// <summary>
        /// Current Highscore list.
        /// </summary>
        public List<HighScore> highscores = new List<HighScore>();

        /// <summary>
        /// If the highscore will be cleared to the factoryHighscores.
        /// </summary>
        public bool clearHighscores;

        protected static HighScoreManager instance = null;

        /// <summary>
        /// Static reference of the State Selector.
        /// </summary>
        public static HighScoreManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(HighScoreManager)) as HighScoreManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("HighScoreManager");
                    instance = go.AddComponent(typeof(HighScoreManager)) as HighScoreManager;

                }

                return instance;

            }

        }

        void Awake () {

            if (clearHighscores) {

                SaveHighScoreList(factoryHighscores);

            }

            highscores.Clear();
            highscores = LoadHighScoreList();

        }

        /// <summary>
        /// Saves the current Highscore in PlayerPrefs.
        /// </summary>
        /// <param name="highscores">The highscore list.</param>
        public void SaveHighScoreList(List<HighScore> highscores) {

            for (int i = 0; i < highscores.Count; i++) {

                SaveScore(i, highscores[i].name, highscores[i].score);

            }

			PlayerPrefs.Save();

        }

        /// <summary>
        /// Loads the Highscore that is stored in PlayerPrefs.
        /// </summary>
        /// <returns>The highscore list.</returns>
        public List<HighScore> LoadHighScoreList () {

            List<HighScore> inDataHighscoreList = new List<HighScore>();

            for (int i = 0; i < 10; i++) {

                inDataHighscoreList.Add(GetScore(i));

            }

            return inDataHighscoreList;

        }

        /// <summary>
        /// Saves a score.
        /// </summary>
        /// <param name="_rank">The rank of the player his score.</param>
        /// <param name="_name">The name of the player.</param>
        /// <param name="_score">The score of the player.</param>
        private void SaveScore (int _rank, string _name, int _score) {

            PlayerPrefs.SetString("PLAYER" + _rank + "[NAME]", _name);
            PlayerPrefs.SetInt("PLAYER" + _rank + "[Score]", _score);

        }

        private HighScore GetScore (int _index) {

            HighScore score = new HighScore();
            score.name = PlayerPrefs.GetString("PLAYER" + _index + "[NAME]");
            score.score = PlayerPrefs.GetInt("PLAYER" + _index + "[Score]");
            return score;

        }

        /// <summary>
        /// Checks if the score is eligible to placed in the highscore.
        /// </summary>
        /// <param name="_value">The score that will be checked.</param>
        public bool IsEligibleForHighscore(int _value) {

            if(highscores[9].score < _value) {

                return true;

            }

            return false;

        }

        /// <summary>
        /// Finds the position of the given score.
        /// </summary>
        /// <param name="_scoreValue">The score value.</param>
        /// <returns></returns>
        public int FindPositionInHighscore (int _scoreValue) {

            int currentIndex = 99;

            for (int i = 0; i < highscores.Count; i++) {

                if (highscores[i].score < _scoreValue) {
                   
                    return i;

                }

            }

            return currentIndex;

        }

    }

    /// <summary>
    /// Contains highscoreData.
    /// </summary>
    [System.Serializable]
    public struct HighScore {

        /// <summary>
        /// Name of the player.
        /// </summary>
        public string name;
        
        /// <summary>
        /// The score made in-game.
        /// </summary>
        public int score;

    }

}
