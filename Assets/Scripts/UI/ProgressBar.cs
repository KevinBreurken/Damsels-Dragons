using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
namespace Base.UI {

    public class ProgressBar : MonoBehaviour {

        public float startPosition;
        public float endPosition;
        public float currentPosition;
        private float sliderPosition;

        public Slider slider;
        public GameObject target;
        public bool followsTarget;

        void Update () {

            if (followsTarget) {

                currentPosition = target.transform.position.x;
                float a1 = endPosition - startPosition;
                float a2 = currentPosition - startPosition;
                sliderPosition = a2 / a1;
                slider.value = sliderPosition;
        
            }

        }

        public void SetValues (float _startPosition, float _endPosition) {

            startPosition = _startPosition;
            endPosition = _endPosition;
            followsTarget = false;
            slider.DOValue(0, 0.5f).OnComplete(OnTweenComplete);

        }

        private void OnTweenComplete () {

            followsTarget = true;

        }

    }

}
