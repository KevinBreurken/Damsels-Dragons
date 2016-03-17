using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Game;
using Base.Audio;
using DG.Tweening;

namespace Base.UI.State {

    /// <summary>
    /// The menu state of the game.
    /// </summary>
    public class MenuUIState : BaseUIState {

        public UIButton startButton;
        public UIButton highscoreButton;
		public UIButton optionsButton;
		public UIButton creditsButton;
        public UIButton quitButton;

		public CanvasGroup highscoreLayer;
		public CanvasGroup optionsLayer;
		public CanvasGroup creditsLayer;
		public HighscoreLayer highscoreLayerComponent;

        void Awake () {

            startButton.onClicked += StartButton_onClicked;
            highscoreButton.onClicked += HighscoreButton_onClicked;
            quitButton.onClicked += QuitButton_onClicked;
			creditsButton.onClicked += CreditsButton_onClicked;
			optionsButton.onClicked += OptionsButton_onClicked;
			OpenLayer(highscoreLayer);

        }

        void OptionsButton_onClicked () {

			OpenLayer(optionsLayer);

        }

        void CreditsButton_onClicked () {
			
			OpenLayer(creditsLayer);

        }

        private void QuitButton_onClicked () {

            StartCoroutine(quitButton.Hide());
            Application.Quit();

        }

        private void StartButton_onClicked () {

            StartCoroutine(startButton.Hide());
            UIStateSelector.Instance.SetState("GameUIState");

        }

        private void HighscoreButton_onClicked () {

			OpenLayer(highscoreLayer);
			highscoreLayer.GetComponent<HighscoreLayer>().Enter();

        }

		private void OpenLayer (CanvasGroup _layerToOpen) {
			
			DisableLayers();
			_layerToOpen.interactable = true;
			_layerToOpen.DOFade(1,0.5f);

		}

		/// <summary>
		/// This function disable all menu layers.
		/// </summary>
		private void DisableLayers(){

			highscoreLayer.interactable = false;
			highscoreLayer.alpha = 0;
			optionsLayer.interactable = false;
			optionsLayer.alpha = 0;
			creditsLayer.interactable = false;
			creditsLayer.alpha = 0;

		}

        public override void Enter () {

            base.Enter();

            AudioObject song = MusicManager.Instance.GetSongByName("MainMenuMusic");
            song.FadeVolume(0, 1, 5);
            StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

            startButton.Show();
            highscoreButton.Show();
            quitButton.Show();
			optionsButton.Show();
			creditsButton.Show();

			OpenLayer(highscoreLayer);

			highscoreLayerComponent.Enter();

            GameStateSelector.Instance.SetState("OffGameState");

        }

        public override IEnumerator Exit () {

            if (UIStateSelector.Instance.nextState.name == "Game UI State") {

                AudioObject song = MusicManager.Instance.GetSongByName("MainMenuMusic");
                song.FadeVolume(1, 0, 5);
                StartCoroutine(MusicManager.Instance.StopSong(song));

            }
            
            /*
            if we want the total time of the longest button animation
            float waitTime = Calculate.GetHighestFromList(new List<float>() {
                startButton.hideAnimationData.TotalLength + startButton.hideAnimationData.delay,
                optionsButton.hideAnimationData.TotalLength + optionsButton.hideAnimationData.delay,
                creditsButton.hideAnimationData.TotalLength + creditsButton.hideAnimationData.delay,
                quitButton.hideAnimationData.TotalLength + quitButton.hideAnimationData.delay
            });
            */

            yield return StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1));
            base.Exit();

        }

    }

}