using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Base.Effect {

    public class FadeEffect : MonoBehaviour {

        public delegate void FadeEvent ();

        /// <summary>
        /// Called when the AudioObject is finished playing.
        /// </summary>
        public event FadeEvent onFadeFinished;

        /// <summary>
        /// If the screen fade is faded in (example: the screen is black).
        /// </summary>
        public bool isFadedIn;

        /// <summary>
        /// How fast the screen will fade.
        /// </summary>
        public float fadeSpeed;

        private CanvasGroup canvasGroup;

        void Awake () {

            canvasGroup = GetComponent<CanvasGroup>();

        }

        /// <summary>
        /// Fades the fade layer to the given value.
        /// </summary>
        /// <param name="_endValue">The value it will fade to.</param>
        public void Fade (float _endValue) {

            canvasGroup.DOFade(_endValue, fadeSpeed).OnComplete(FadeCompleted);

        }

        /// <summary>
        /// Fades the fade layer to the given value.
        /// </summary>
        /// <param name="_endValue">The value it will fade to.</param>
        /// <param name="_speed">How fast the screen will fade.</param>
        public void Fade (float _endValue, float _speed) {
            Debug.Log(_endValue);
            canvasGroup.DOFade(_endValue, _speed).OnComplete(FadeCompleted);

        }

        /// <summary>
        /// Fades the fade layer to the given value.
        /// </summary>
        /// <param name="_endValue">The value it will fade to.</param>
        /// <param name="_speed">How fast the screen will fade.</param>
        /// <param name="_startValue">which value the canvasGroup starts in.</param>
        public void Fade (float _endValue, float _speed, float _startValue) {

            canvasGroup.alpha = _startValue;
            canvasGroup.DOFade(_endValue, _speed).OnComplete(FadeCompleted);

        }

        public void StopFade () {

            canvasGroup.DOKill();

        }
        /// <summary>
        /// Called when the fade tween is finished.
        /// </summary>
        private void FadeCompleted () {

            if(canvasGroup.alpha == 1) {

                isFadedIn = true;

            } else {

                isFadedIn = false;

            }

            if(onFadeFinished != null) {

                onFadeFinished();

            }

        }

    }

}
