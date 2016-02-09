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
        public float timeTilleStateSwitch;

        /// <summary>
        /// The UI state that will be entered after this UI state is finished.
        /// </summary>
        public BaseUIState nextUIState;

        private CanvasGroup canvasGroup;

        void Awake () {
			
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;

		}

		public override void Enter () {

			base.Enter ();
            canvasGroup.DOFade(1, fadeInTime).OnComplete(OnFadedIn);

		}

        private void OnFadedIn () {

            StartCoroutine(WaitToFadeOut());

        }

        private void OnFadedOut () {

            StartCoroutine(WaitToLeaveState());

        }

		IEnumerator WaitToFadeOut() {
			
			yield return new WaitForSeconds(timeTillFadeOutTime);
            canvasGroup.DOFade(0, 2).OnComplete(OnFadedOut);

		}

        IEnumerator WaitToLeaveState () {

            yield return new WaitForSeconds(timeTilleStateSwitch);
            UIStateSelector.Instance.SetUIState(nextUIState);

        }
			
	}

}