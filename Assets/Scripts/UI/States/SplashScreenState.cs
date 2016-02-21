using UnityEngine;
using System.Collections;
using Base.UI;
using Base.UI.State;
using DG.Tweening;

namespace Base.UI.State {

    /// <summary>
    /// The UIState that shows the team logo.
    /// </summary>
	public class SplashScreenState : BaseUIState {

        /// <summary>
        /// How long it takes to fade in.
        /// </summary>
		public float fadeInTime;

        /// <summary>
        /// How long it takes to fade out.
        /// </summary>
        public float fadeOutTime;

        /// <summary>
        /// How long it takes before the screen will fade out. (after fading in)
        /// </summary>
        public float timeTillFadeOutTime;

        /// <summary>
        /// How long it takes before the screen will enter the next state. (after fading out)
        /// </summary>
        public float timeTillStateSwitch;

        /// <summary>
        /// The UI state that will be entered after this UI state is finished.
        /// </summary>
        public BaseUIState nextUIState;

        private bool forceNextScreen = false;

        public override void Update () {

            if (Input.anyKey) {

                if (!forceNextScreen) {
                    
                    ForceToNextScreen();
                    
                }

            }

        }

		public override void Enter () {

			base.Enter ();

            Effect.EffectManager.Instance.FadeEffect.SetFadeLayerValue(1);
            StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(0, fadeInTime));
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished += OnFadedIn;
            
        }

        private void OnFadedIn () {

            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedIn;
            StartCoroutine(WaitToFadeOut());

        }

        IEnumerator WaitToFadeOut () {

            yield return new WaitForSeconds(timeTillFadeOutTime);
            StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1, fadeOutTime));
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished += OnFadedOut;

        }

        private void OnFadedOut () {

            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedOut;
            StartCoroutine(WaitToLeaveState());

        }

        IEnumerator WaitToLeaveState () {

            //safety check to remove the event listeners.
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedOut;
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedIn;

            yield return new WaitForSeconds(timeTillStateSwitch);
            UIStateSelector.Instance.SetState("MenuUIState");

        }
			
        private void ForceToNextScreen () {

            StopAllCoroutines();
            forceNextScreen = true;
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedOut;
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= OnFadedIn;

            Effect.EffectManager.Instance.FadeEffect.StopFade();
            StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1, 1.35f));
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished += OnFadedOut;

        }

	}

}