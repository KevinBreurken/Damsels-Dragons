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
    public class HighscoreUIState : BaseUIState {

        public UIButton retryButton;
        public UIButton menuButton;
        public CanvasGroup inputGroup;
        public List<GameObject> scoreList;
        void Awake () {

            retryButton.onClicked += RetryButton_onClicked;
            menuButton.onClicked += MenuButton_onClicked;

        }

        private void RetryButton_onClicked () {

            StartCoroutine(retryButton.Hide());
            UIStateSelector.Instance.SetState("GameUIState");

        }

        private void MenuButton_onClicked () {

            StartCoroutine(menuButton.Hide());
            UIStateSelector.Instance.SetState("MenuUIState");

        }

        public override void Enter () {

            base.Enter();

            Text retryText = retryButton.GetComponentInChildren<Text>();
            switch (UIStateSelector.Instance.previousState.name) {
                case "Menu UI State":
                retryText.text = "Play";
                break;
                case "Game UI State":
                retryText.text = "Retry";
                break;
            }

            retryButton.Show();
            menuButton.Show();
         
            //Check if theres a highscore.
            int score = ScoreManager.Instance.GetScore();
            Debug.Log(score);
            if (HighScoreManager.Instance.IsEligibleForHighscore(score)) {
                inputGroup.DOFade(1, 0.5f);
                inputGroup.interactable = true;
                ShowScoreWithNewScoreEmpty(score);
            } else {
                inputGroup.alpha = 0;
                inputGroup.interactable = false;
                UpdateScoreList();
            }

            GameStateSelector.Instance.SetState("OffGameState");

        }

        private void ShowScoreWithNewScoreEmpty (int _score) {

            int index = HighScoreManager.Instance.FindPositionInHighscore(_score);
            if(index == 99)
            index = 0;
            Debug.Log(index);
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
        }

        private void UpdateScoreList () {

            List<HighScore> highscore = HighScoreManager.Instance.LoadScoreList();
            for (int i = 0; i < scoreList.Count; i++) {

                scoreList[i].transform.FindChild("Score").GetComponent<Text>().text = "" +  highscore[i].score;
                scoreList[i].transform.FindChild("Name").GetComponent<Text>().text = "" + highscore[i].name;

            }

        }

        public override IEnumerator Exit () {

            yield return StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1));
            base.Exit();

        }

    }

}