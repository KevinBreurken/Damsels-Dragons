using UnityEngine;
using System.Collections;
using Base.Game;
using Base.Game.State;
using Base.Audio;
using DG.Tweening;
using UnityEngine.UI;

namespace Base.UI.State {

    /// <summary>
    /// The UI state used in-game.
    /// </summary>
    public class GameUIState : BaseUIState {

        /// <summary>
        /// The state that is entered after this state is left.
        /// </summary>
        public BaseGameState nextGameState;

        /// <summary>
        /// The canvas group of the pop-up notification.
        /// </summary>
        [Header("Next level animation")]
        public CanvasGroup popupNotificationLayer;
        public UIObject popupReadyObject;
        public UIObject popupNextLevelGoObject;
        public UIObject popupLevelObject;
        public Text nextLevelText;

        [Header("Level Counter")]
        public UIObject levelCounter;
        public Text levelCounterText;


        public override void Enter () {
            base.Enter();
            Game.GameStateSelector.Instance.SetState("InGameState");

            AudioObject song = MusicManager.Instance.GetSongByName("GameMusic");
            song.FadeVolume(0,1, 5);
            StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

        }

        /// <summary>
        /// Plays the highscore notification.
        /// </summary>
        public IEnumerator PlayNextLevelNotification () {

            popupNextLevelGoObject.GetCanvasGroup().alpha = 0;
            popupReadyObject.GetCanvasGroup().alpha = 0;
            popupLevelObject.GetCanvasGroup().alpha = 0;

            popupNotificationLayer.interactable = true;
            popupNotificationLayer.blocksRaycasts = true;
            popupNotificationLayer.alpha = 0;
            popupNotificationLayer.DOFade(1, 1);

            popupReadyObject.Show();
            yield return new WaitForSeconds(1);

            popupReadyObject.GetCanvasGroup().DOFade(0, 0.5f);
            popupNextLevelGoObject.Show();
            yield return new WaitForSeconds(1);

            popupNextLevelGoObject.GetCanvasGroup().DOFade(0, 0.5f);
            popupNotificationLayer.DOFade(0, 0.5f);

            popupNotificationLayer.interactable = false;
            popupNotificationLayer.blocksRaycasts = false;

        }

        /// <summary>
        /// Plays the highscore notification. also shows the next level.
        /// </summary>
        public IEnumerator PlayNextLevelNotificationWithLevel (int _levelNum) {

            nextLevelText.text = "" + _levelNum;

            popupNextLevelGoObject.GetCanvasGroup().alpha = 0;
            popupReadyObject.GetCanvasGroup().alpha = 0;
            popupLevelObject.GetCanvasGroup().alpha = 0;

            popupNotificationLayer.interactable = true;
            popupNotificationLayer.blocksRaycasts = true;
            popupNotificationLayer.alpha = 0;
            popupNotificationLayer.DOFade(1, 1);

            popupLevelObject.Show();
            yield return new WaitForSeconds(1);

            popupLevelObject.GetCanvasGroup().DOFade(0, 0.5f);
            popupReadyObject.Show();
            yield return new WaitForSeconds(1);

            popupReadyObject.GetCanvasGroup().DOFade(0, 0.5f);
            popupNextLevelGoObject.Show();
            yield return new WaitForSeconds(1);

            popupNextLevelGoObject.GetCanvasGroup().DOFade(0, 0.5f);
            popupNotificationLayer.DOFade(0, 0.5f);

            popupNotificationLayer.interactable = false;
            popupNotificationLayer.blocksRaycasts = false;

        }

        /// <summary>
        /// Sets the level counter.
        /// </summary>
        /// <param name="_levelNumber">Current level number.</param>
        public void SetLevelCounterText(int _levelNumber) {

            levelCounter.Show();
            levelCounterText.text = "Level: " + _levelNumber;

        }

        /// <summary>
        /// Called when the state is left.
        /// </summary>
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