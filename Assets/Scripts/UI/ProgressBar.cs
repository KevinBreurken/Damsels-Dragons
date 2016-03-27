using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
namespace Base.UI {

    /// <summary>
    /// Keeps track of the players position on a level.
    /// </summary>
    public class ProgressBar : MonoBehaviour {

        /// <summary>
        /// If the progress bar is keeping track of its target.
        /// </summary>
        public bool followsTarget;

        private Transform target;
        private Slider slider;
        private float startPosition;
        private float endPosition;
        private float currentPosition;
        private float sliderPosition;

        void Awake () {

            slider = GetComponentInChildren<Slider>();

        }

        void Update () {

            if (followsTarget) {

                currentPosition = target.position.x;
                float a1 = endPosition - startPosition;
                float a2 = currentPosition - startPosition;
                sliderPosition = a2 / a1;
                slider.value = sliderPosition;
        
            }

        }

        /// <summary>
        /// Sets the starting and end position for the slider.
        /// </summary>
        /// <param name="_startPosition">The starting position. (left side of the bar)</param>
        /// <param name="_endPosition">The ending position. (right side of the bar)</param>
        public void SetValues (float _startPosition, float _endPosition) {

            startPosition = _startPosition;
            endPosition = _endPosition;
            followsTarget = false;
            slider.DOValue(0, 0.5f).OnComplete(OnTweenComplete);

        }

        public void SetTarget(Transform _newTarget) {

            target = _newTarget;

        }

        private void OnTweenComplete () {

            followsTarget = true;

        }

    }

}
