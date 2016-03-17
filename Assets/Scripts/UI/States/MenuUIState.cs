using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Game;
using Base.Audio;
using DG.Tweening;
using UnityEngine.UI;

namespace Base.UI.State {

    /// <summary>
    /// The menu state of the game.
    /// </summary>
    public class MenuUIState : BaseUIState {

        [System.Serializable]
        public struct ButtonImage {

            public Sprite normalButtonSprite;
            public Sprite pressedButtonSprite;

        }

        public ButtonImage leftButtonImage;
        public ButtonImage rightButtonImage;

        public UIButton startButton;
        public UIButton highscoreButton;
		public UIButton optionsButton;
		public UIButton creditsButton;
        public UIButton quitButton;

		public CanvasGroup highscoreLayer;
		public CanvasGroup optionsLayer;
		public CanvasGroup creditsLayer;
		public HighscoreLayer highscoreLayerComponent;

        private CanvasGroup previousOpenedLayer;
        private UIButton previousButton;
        private Sprite previousButtonSprite;

        void Awake () {

            startButton.onClicked += StartButton_onClicked;
            highscoreButton.onClicked += HighscoreButton_onClicked;
            quitButton.onClicked += QuitButton_onClicked;
			creditsButton.onClicked += CreditsButton_onClicked;
			optionsButton.onClicked += OptionsButton_onClicked;
			OpenLayer(highscoreLayer,highscoreButton, leftButtonImage);

        }

        void OptionsButton_onClicked () {

			OpenLayer(optionsLayer,optionsButton, leftButtonImage);

        }

        void CreditsButton_onClicked () {
			
			OpenLayer(creditsLayer,creditsButton, rightButtonImage);

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

			OpenLayer(highscoreLayer,highscoreButton, leftButtonImage);
			highscoreLayer.GetComponent<HighscoreLayer>().Enter();

        }

		private void OpenLayer (CanvasGroup _layerToOpen,UIButton _button, ButtonImage _buttonImage) {

            if (previousOpenedLayer == _layerToOpen)
                return;

            if(previousOpenedLayer == highscoreLayer) {

                highscoreLayerComponent.Exit();

            }

			DisableLayers();
			_layerToOpen.interactable = true;
			_layerToOpen.DOFade(1,0.5f);

            if (previousButton != null)
                previousButton.GetComponent<Image>().sprite = previousButtonSprite;

            _button.GetComponent<Image>().sprite = _buttonImage.pressedButtonSprite;
            previousOpenedLayer = _layerToOpen;

            previousButton = _button;
            previousButtonSprite = _buttonImage.normalButtonSprite;

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

			OpenLayer(highscoreLayer,highscoreButton, leftButtonImage);

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