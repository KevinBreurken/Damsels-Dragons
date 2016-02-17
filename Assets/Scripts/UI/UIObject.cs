using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Base.UI {

    [RequireComponent(typeof(CanvasGroup),typeof(RectTransform))]
    public class UIObject : MonoBehaviour {

        public UIAnimationData showAnimationData;
        public UIAnimationData hideAnimationData;

        private CanvasGroup canvasGroup;
        [SerializeField]
        private RectTransform rectTransform;

        // Use this for initialization
        void Awake () {
            //Add object position to animation positions.
            showAnimationData.AddObjectPosition(transform.localPosition);
            hideAnimationData.AddObjectPosition(transform.localPosition);

            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();

        }

        public void Hide () {

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            SetAnimation(hideAnimationData);

        }

        public void Show () {

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            SetAnimation(showAnimationData);

        }

        public void SetAnimation (UIAnimationData _data) {
            //Set starting values
            rectTransform.localPosition = _data.startPosition;
            canvasGroup.alpha = _data.startFadeValue;

            canvasGroup.DOFade(_data.endFadeValue, _data.fadeSpeed).SetEase(_data.fadeEaseType);
            rectTransform.DOLocalMove(_data.endPosition, _data.moveSpeed).SetEase(_data.moveEaseType);
        }

    }

    [System.Serializable]
    public struct UIAnimationData {
        [Header("Moving")]
        public Vector2 startPosition;
        public Vector2 endPosition;
        public float moveSpeed;
        public Ease moveEaseType;
        [Space]
        [Header("Fading")]
        public float startFadeValue;
        public float endFadeValue;
        public float fadeSpeed;
        public Ease fadeEaseType;

        public void AddObjectPosition (Vector3 _localPosition) {
            startPosition += new Vector2(_localPosition.x, _localPosition.y);
            endPosition += new Vector2(_localPosition.x, _localPosition.y);
        }

    }

}