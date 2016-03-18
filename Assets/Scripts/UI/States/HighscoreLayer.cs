using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Audio;
using UnityEngine.UI;
using Base.Score;
using DG.Tweening;
using Base.Game;

namespace Base.UI.State {


    /// <summary>
    /// The menu state of the game.
    /// </summary>
	public class HighscoreLayer : MonoBehaviour {

        public CanvasGroup inputGroup;
        public List<GameObject> scoreList;
		public UIButton submitButton;
		public InputField inputField;
		private int indexOfNewHighscore;
		private int newHighScore;
        private int latestMatchScore;

        //highscore notification
        public CanvasGroup highscoreNotificationLayer;
        public UIObject newHighscoreObject;
        public UIObject newHighscoreRankObject;
        public Text newHighscoreRankText;

        public CanvasGroup scoreCanvasGroup;
        public Text scoreText;

        void Awake () {
			
			submitButton.onClicked += SubmitButton_onClicked;

        }

        void SubmitButton_onClicked () {

			if(inputField.text.Length != 0){
				
				scoreList[indexOfNewHighscore].transform.FindChild("Name").GetComponent<Text>().text = inputField.text;
				scoreList[indexOfNewHighscore].transform.FindChild("Score").GetComponent<Text>().text = "" + newHighScore;
				newHighScore = 0;
                ScoreManager.Instance.ResetScore();
				inputField.text = "";

			}

			SubmitNewHighscore();
			HideInputPanel();

        }
			
        public void ShowNewHighscoreNotification(int _rankNumber) {

            newHighscoreRankText.text = "" + (_rankNumber + 1);
            StartCoroutine(WaitForHighscoreNotification());

        }

        private IEnumerator WaitForHighscoreNotification () {

            highscoreNotificationLayer.interactable = true;
            highscoreNotificationLayer.blocksRaycasts = true;
            highscoreNotificationLayer.alpha = 0;
            highscoreNotificationLayer.DOFade(1, 1);

            newHighscoreObject.Show();
            newHighscoreRankObject.Show();

            yield return new WaitForSeconds(3);
            StartCoroutine(newHighscoreObject.Hide());
            StartCoroutine(newHighscoreRankObject.Hide());
            highscoreNotificationLayer.DOFade(0, 0.5f);

            highscoreNotificationLayer.interactable = false;
            highscoreNotificationLayer.blocksRaycasts = false;

        }


		public void Enter () {
            
            //Check if theres a highscore.
			indexOfNewHighscore = -1;
            
            int score = ScoreManager.Instance.GetScore();
            latestMatchScore = score;
            if (HighScoreManager.Instance.IsEligibleForHighscore(score)) {

				newHighScore = score;
                inputGroup.DOFade(1, 0.5f);
                inputGroup.interactable = true;
				inputGroup.blocksRaycasts = true;
                ShowScoreWithNewScoreEmpty(score);

            } else {
				
				HideInputPanel();
                ShowScore();
            }

        }

        public void Exit () {

            newHighScore = 0;
            ScoreManager.Instance.ResetScore();
            inputField.text = "";

        }

		private void HideInputPanel () {

			inputGroup.alpha = 0;
			inputGroup.interactable = false;
			inputGroup.blocksRaycasts = false;
			UpdateScoreList();
            ShowScore();
		}

        public void ShowScore () {

            if(latestMatchScore == 0) {

                scoreCanvasGroup.alpha = 0;

            } else {

                scoreCanvasGroup.DOFade(1, 0.5f);
                scoreText.text = "" + latestMatchScore;

            }

        }

		private void SubmitNewHighscore () {
			List<HighScore> highscore = new List<HighScore>();

			for (int i = 0; i < scoreList.Count; i++) {
				
				HighScore newScore = new HighScore();
				newScore.name = scoreList[i].transform.FindChild("Name").GetComponent<Text>().text;
				newScore.score = int.Parse(scoreList[i].transform.FindChild("Score").GetComponent<Text>().text);
				highscore.Add(newScore);

			}

			HighScoreManager.Instance.SaveScoreList(highscore);

		}

        private void ShowScoreWithNewScoreEmpty (int _score) {

            int index = HighScoreManager.Instance.FindPositionInHighscore(_score);
            if(index == 99)
            index = 0;
            List<HighScore> highscore = HighScoreManager.Instance.LoadScoreList();
            for (int i = 0; i < scoreList.Count; i++) {
                if(i < index) {
                    scoreList[i].transform.FindChild("Score").GetComponent<Text>().text = "" + highscore[i].score;
                    scoreList[i].transform.FindChild("Name").GetComponent<Text>().text = "" + highscore[i].name;
                } else if (i == index) {
                    scoreList[i].transform.FindChild("Name").GetComponent<Text>().text = "YOUR NAME HERE";
                    scoreList[i].transform.FindChild("Score").GetComponent<Text>().text = "" + _score;
                } else {
                    scoreList[i].transform.FindChild("Score").GetComponent<Text>().text = "" + highscore[i - 1].score;
                    scoreList[i].transform.FindChild("Name").GetComponent<Text>().text = "" + highscore[i - 1].name;
                }
            }

			indexOfNewHighscore = index;
            ShowNewHighscoreNotification(indexOfNewHighscore);

        }

        private void UpdateScoreList () {

            List<HighScore> highscore = HighScoreManager.Instance.LoadScoreList();
            for (int i = 0; i < scoreList.Count; i++) {

                scoreList[i].transform.FindChild("Score").GetComponent<Text>().text = "" +  highscore[i].score;
                scoreList[i].transform.FindChild("Name").GetComponent<Text>().text = "" + highscore[i].name;

            }

        }

    }

}