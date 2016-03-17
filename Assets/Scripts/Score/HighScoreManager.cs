using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Score {

    public class HighScoreManager : MonoBehaviour {

        /// <summary>
        /// Default Highscore used when clearing playerprefs.
        /// </summary>
        public List<HighScore> factoryHighscores = new List<HighScore>();
        public List<HighScore> highscores = new List<HighScore>();
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

                SaveScoreList(factoryHighscores);

            }
            highscores.Clear();
            highscores = LoadScoreList();

        }

        public void SaveScoreList(List<HighScore> highscores) {

            for (int i = 0; i < highscores.Count; i++) {

                SaveScore(i, highscores[i].name, highscores[i].score);

            }

			PlayerPrefs.Save();

        }

        public List<HighScore> LoadScoreList () {

            List<HighScore> inDataHighscoreList = new List<HighScore>();

            for (int i = 0; i < 10; i++) {

                inDataHighscoreList.Add(GetScore(i));

            }

            return inDataHighscoreList;

        }

        private void SaveScore (int _index, string _name, int _score) {

            PlayerPrefs.SetString("PLAYER" + _index + "[NAME]", _name);
            PlayerPrefs.SetInt("PLAYER" + _index + "[Score]", _score);

        }

        private HighScore GetScore (int _index) {

            HighScore score = new HighScore();
            score.name = PlayerPrefs.GetString("PLAYER" + _index + "[NAME]");
            score.score = PlayerPrefs.GetInt("PLAYER" + _index + "[Score]");
            return score;

        }

        public bool IsEligibleForHighscore(int _value) {
            Debug.Log(highscores[9]);
            if(highscores[9].score < _value) {

                return true;

            }

            return false;

        }

        public int FindPositionInHighscore (int _value) {

            int currentIndex = 99;

            for (int i = 0; i < highscores.Count; i++) {

                if (highscores[i].score > _value) {

                    currentIndex = i;

                }

            }

            return currentIndex;

        }

    }

    [System.Serializable]
    public struct HighScore {

        public string name;
        public int score;

    }

}
