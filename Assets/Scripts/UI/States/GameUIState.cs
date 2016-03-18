using UnityEngine;
using System.Collections;
using Base.Game;
using Base.Game.State;
using Base.Audio;
using DG.Tweening;
using UnityEngine.UI;

namespace Base.UI.State {

    /// <summary>
    /// Not yet finished. acts as dummy.
    /// </summary>
    public class GameUIState : BaseUIState {

        public BaseGameState nextGameState;

        //next level notification
        public CanvasGroup nextLevelNotificationLayer;
        public UIObject nextLevelReadyObject;
        public UIObject nextLevelGoObject;
        public UIObject nextLevelLevelObject;
        public Text nextLevelText;

        public UIObject levelCounter;
        public Text levelCounterText;


        public override void Enter () {
            base.Enter();
            Game.GameStateSelector.Instance.SetState("InGameState");

            AudioObject song = MusicManager.Instance.GetSongByName("GameMusic");
            song.FadeVolume(0,1, 5);
            StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

        }

        public IEnumerator WaitForHighscoreNotification () {

            nextLevelGoObject.GetCanvasGroup().alpha = 0;
            nextLevelReadyObject.GetCanvasGroup().alpha = 0;
            nextLevelLevelObject.GetCanvasGroup().alpha = 0;

            nextLevelNotificationLayer.interactable = true;
            nextLevelNotificationLayer.blocksRaycasts = true;
            nextLevelNotificationLayer.alpha = 0;
            nextLevelNotificationLayer.DOFade(1, 1);

            nextLevelReadyObject.Show();
            yield return new WaitForSeconds(1);
            nextLevelReadyObject.GetCanvasGroup().DOFade(0, 0.5f);
            nextLevelGoObject.Show();
            yield return new WaitForSeconds(1);
            nextLevelGoObject.GetCanvasGroup().DOFade(0, 0.5f);

            nextLevelNotificationLayer.DOFade(0, 0.5f);

            nextLevelNotificationLayer.interactable = false;
            nextLevelNotificationLayer.blocksRaycasts = false;

        }

        public IEnumerator WaitForHighscoreNotificationWithLevel (int _levelNum) {

            nextLevelText.text = "" + _levelNum;

            nextLevelGoObject.GetCanvasGroup().alpha = 0;
            nextLevelReadyObject.GetCanvasGroup().alpha = 0;
            nextLevelLevelObject.GetCanvasGroup().alpha = 0;

            nextLevelNotificationLayer.interactable = true;
            nextLevelNotificationLayer.blocksRaycasts = true;
            nextLevelNotificationLayer.alpha = 0;
            nextLevelNotificationLayer.DOFade(1, 1);

            nextLevelLevelObject.Show();
            yield return new WaitForSeconds(1);
            nextLevelLevelObject.GetCanvasGroup().DOFade(0, 0.5f);
            nextLevelReadyObject.Show();
            yield return new WaitForSeconds(1);
            nextLevelReadyObject.GetCanvasGroup().DOFade(0, 0.5f);
            nextLevelGoObject.Show();
            yield return new WaitForSeconds(1);
            nextLevelGoObject.GetCanvasGroup().DOFade(0, 0.5f);

            nextLevelNotificationLayer.DOFade(0, 0.5f);

            nextLevelNotificationLayer.interactable = false;
            nextLevelNotificationLayer.blocksRaycasts = false;

        }

        public void SetLevelCounterText(int _levelNum) {
            levelCounter.Show();
            levelCounterText.text = "Level: " + _levelNum;
        }
        public override IEnumerator Exit () {

            Effect.EffectManager.Instance.FadeEffect.onFadeFinished += FadeEffect_onFadeFinished;
			Score.ScoreManager.Instance.FinaliseScore();
            AudioObject song = MusicManager.Instance.GetSongByName("GameMusic");
            song.FadeVolume(1,0, 5);
            StartCoroutine(MusicManager.Instance.StopSong(song));

            yield return StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1));
            GameStateSelector.Instance.SetState(nextGameState);

        }

        private void FadeEffect_onFadeFinished () {
			
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= FadeEffect_onFadeFinished;
            Time.timeScale = 1;

        }

    }

}