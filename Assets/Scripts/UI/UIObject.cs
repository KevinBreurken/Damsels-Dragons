using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Base.Audio;
using UnityEngine.EventSystems;
using Math;

namespace Base.UI {

    /// <summary>
    /// UIObject is a interface based object that can be animated by using UIAnimationData.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup),typeof(RectTransform))]
    public class UIObject : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler {

        /// <summary>
        /// Animation data for the show animation.
        /// </summary>
        public UIAnimationData showAnimationData;

        /// <summary>
        /// AnimationData for the hide animation.
        /// </summary>
        public UIAnimationData hideAnimationData;

        /// <summary>
        /// AnimationData for the pointer enter animation.
        /// </summary>
        public UIAnimationData enterAnimationData;

        /// <summary>
        /// AnimationData for the pointer exit animation.
        /// </summary>
        public UIAnimationData exitAnimationData;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private bool isTweening;

        public virtual void Awake () {
            //Initialize the animation data.
            showAnimationData.Initialize(transform);
            hideAnimationData.Initialize(transform);
            enterAnimationData.Initialize(transform);
            exitAnimationData.Initialize(transform);

            //Get references
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            
        }

        /// <summary>
        /// Called when the mouse pointer enters the UIObject.
        /// </summary>
        public void OnPointerEnter (PointerEventData eventData) {
            if (!isTweening) {
                StopAllCoroutines();
                StartCoroutine(PlayAnimation(enterAnimationData));
            }
        }

        /// <summary>
        /// Called when the mouse pointer exits the UIObject.
        /// </summary>
        public void OnPointerExit (PointerEventData eventData) {
            if (!isTweening) {
                StopAllCoroutines();
                StartCoroutine(PlayAnimation(exitAnimationData));
            }
        }

        /// <summary>
        /// Hides and disables this UIObject.
        /// </summary>
        public IEnumerator Hide () {

            isTweening = true;

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            yield return StartCoroutine(PlayAnimation(hideAnimationData));

        }

        /// <summary>
        /// Shows and enables this UIObject.
        /// </summary>
        public void Show () {

            isTweening = true;

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            StopAllCoroutines();
            StartCoroutine(PlayAnimation(showAnimationData));

        }

        /// <summary>
        /// Plays a animation.
        /// </summary>
        /// <param name="_data">The data that is used for this animation.</param>
        public IEnumerator PlayAnimation (UIAnimationData _data) {
            //Stop all current DOTween animations.
            canvasGroup.DOKill();
            rectTransform.DOKill();

            //Set starting values if they're used.
            if (_data.usesMoveAnimation && _data.useStartPosition)
                rectTransform.localPosition = _data.startPosition;
            if (_data.usesFadeAnimation && _data.useStartFadeValue)
                canvasGroup.alpha = _data.startFadeValue;
            if (_data.usesRotationAnimation && _data.useStartRotation)
                rectTransform.eulerAngles = _data.startRotation;

            yield return new WaitForSeconds(_data.delay);

            //Tween it.
            if (_data.usesFadeAnimation)
                StartCoroutine(Fade(_data));
            if (_data.usesMoveAnimation) 
                StartCoroutine(Move(_data));
            if (_data.usesRotationAnimation) 
                StartCoroutine(Rotate(_data));
            if (_data.usesSoundEffect)
                StartCoroutine(Sound(_data));

            //Wait until the tween is finished.
            yield return new WaitForSeconds(_data.TotalLength);

            isTweening = false;

        }

        /// <summary>
        /// Tweens the alpha of this UIObject.
        /// </summary>
        private IEnumerator Fade (UIAnimationData _data) {

            yield return new WaitForSeconds(_data.fadeDelay);
            canvasGroup.DOFade(_data.endFadeValue, _data.fadeAnimationTime).SetEase(_data.fadeEaseType);

        }

        /// <summary>
        /// Tweens the position of this UIObjet.
        /// </summary>
        private IEnumerator Move (UIAnimationData _data) {

            yield return new WaitForSeconds(_data.moveDelay);
            rectTransform.DOLocalMove(_data.endPosition, _data.moveAnimationTime).SetEase(_data.moveEaseType);

        }

        /// <summary>
        /// Tweens the position of this UIObjet.
        /// </summary>
        private IEnumerator Rotate (UIAnimationData _data) {

            yield return new WaitForSeconds(_data.rotationDelay);
            rectTransform.DORotate(_data.endRotation, _data.rotationAnimationTime).SetEase(_data.rotationEaseType);

        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        private IEnumerator Sound (UIAnimationData _data) {

            yield return new WaitForSeconds(_data.soundEffectDelay);
            _data.soundEffect.audioObject.Play();

        }

    }

}