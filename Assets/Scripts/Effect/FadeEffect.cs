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
        public IEnumerator Fade (float _endValue) {

            canvasGroup.DOFade(_endValue, fadeSpeed).OnComplete(FadeCompleted).SetUpdate(true);
            yield return new WaitForSeconds(fadeSpeed);

        }

        /// <summary>
        /// Fades the fade layer to the given value.
        /// </summary>
        /// <param name="_endValue">The value it will fade to.</param>
        /// <param name="_speed">How fast the screen will fade.</param>
        public IEnumerator Fade (float _endValue, float _speed) {

            canvasGroup.DOFade(_endValue, _speed).OnComplete(FadeCompleted).SetUpdate(true);
            yield return new WaitForSeconds(_speed);

        }

        /// <summary>
        /// Fades the fade layer to the given value.
        /// </summary>
        /// <param name="_endValue">The value it will fade to.</param>
        /// <param name="_speed">How fast the screen will fade.</param>
        /// <param name="_startValue">which value the canvasGroup starts in.</param>
        public IEnumerator Fade (float _endValue, float _speed, float _startValue) {

            canvasGroup.alpha = _startValue;
            canvasGroup.DOFade(_endValue, _speed).OnComplete(FadeCompleted).SetUpdate(true);
            yield return new WaitForSeconds(_speed);

        }

        public void StopFade () {

            canvasGroup.DOKill();

        }

        public void SetFadeLayerValue(float _value) {

            canvasGroup.alpha = _value;

        }

        public float GetFadeLayerValue () {

            return canvasGroup.alpha;

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
