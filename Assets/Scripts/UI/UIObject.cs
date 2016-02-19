using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Base.UI {

    [RequireComponent(typeof(CanvasGroup),typeof(RectTransform))]
    public class UIObject : MonoBehaviour {

        /// <summary>
        /// Animation data for the show animation.
        /// </summary>
        public UIAnimationData showAnimationData;

        /// <summary>
        /// AnimationData for the hide animation.
        /// </summary>
        public UIAnimationData hideAnimationData;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        void Awake () {
            //Add object position to animation positions.
            showAnimationData.AddObjectPosition(transform.localPosition);
            hideAnimationData.AddObjectPosition(transform.localPosition);

            //Get references
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();

        }

        /// <summary>
        /// Hides and disables this UIObject.
        /// </summary>
        public void Hide () {

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            SetAnimation(hideAnimationData);

        }

        /// <summary>
        /// Shows and enables this UIObject.
        /// </summary>
        public void Show () {

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            SetAnimation(showAnimationData);

        }

        public void SetAnimation (UIAnimationData _data) {
            //Kill if its still active
            canvasGroup.DOKill();
            rectTransform.DOKill();
            StopAllCoroutines();

            //Set starting values
            rectTransform.localPosition = _data.startPosition;
            canvasGroup.alpha = _data.startFadeValue;

            //Tween it.
            if(_data.usesFadeAnimation)
                StartCoroutine(Fade(_data));
            if (_data.usesMoveAnimation)
                StartCoroutine(Move(_data));
           
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

    }

    [System.Serializable]
    public struct UIAnimationData {

        public GameObject soundEffectPrefab;

        //Move Variables
        public bool usesMoveAnimation;
        public Vector2 startPosition;
        public Vector2 endPosition;
        public float moveAnimationTime;
        public float moveDelay;
        public Ease moveEaseType;
        
        //Fade Variables
        public bool usesFadeAnimation;
        public float startFadeValue;
        public float endFadeValue;
        public float fadeAnimationTime;
        public float fadeDelay;
        public Ease fadeEaseType;

        public void AddObjectPosition (Vector3 _localPosition) {
            startPosition += new Vector2(_localPosition.x, _localPosition.y);
            endPosition += new Vector2(_localPosition.x, _localPosition.y);
        }

    }

}