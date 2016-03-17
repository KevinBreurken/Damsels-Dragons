using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Game;
using Base.Audio;

namespace Base.UI.State {

    /// <summary>
    /// The menu state of the game.
    /// </summary>
    public class MenuUIState : BaseUIState {

        public UIButton startButton;
        public UIButton highscoreButton;
        public UIButton quitButton;

        void Awake () {

            startButton.onClicked += StartButton_onClicked;
            highscoreButton.onClicked += HighscoreButton_onClicked;
            quitButton.onClicked += QuitButton_onClicked;

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

            StartCoroutine(highscoreButton.Hide());
            UIStateSelector.Instance.SetState("HighscoreUIState");

        }

        public override void Enter () {

            base.Enter();

            AudioObject song = MusicManager.Instance.GetSongByName("MainMenuMusic");
            song.FadeVolume(0, 1, 5);
            StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

            startButton.Show();
            highscoreButton.Show();
            quitButton.Show();
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