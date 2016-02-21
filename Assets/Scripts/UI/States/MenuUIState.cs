using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Game;

namespace Base.UI.State {

    /// <summary>
    /// Not yet finished. acts as dummy.
    /// </summary>
    public class MenuUIState : BaseUIState {

        public UIButton startButton;
        public UIButton optionsButton;
        public UIButton creditsButton;
        public UIButton quitButton;

        void Awake () {
            startButton.onClicked += StartButton_onClicked;
        }

        private void StartButton_onClicked () {
            UIStateSelector.Instance.SetState("GameUIState");
        }

        public override void Enter () {

            base.Enter();

            startButton.Show();
            optionsButton.Show();
            creditsButton.Show();
            quitButton.Show();
            GameStateSelector.Instance.SetState("OffGameState");

        }

        public override IEnumerator Exit () {

            StartCoroutine(startButton.Hide());
            StartCoroutine(optionsButton.Hide());
            StartCoroutine(creditsButton.Hide());
            StartCoroutine(quitButton.Hide());

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