using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Math;
using Base.Audio;
using DG.Tweening;

namespace Base.UI {

    /// <summary>
    /// The data that is used for animating a UI Element.
    /// </summary>
    [System.Serializable]
    public struct UIAnimationData {

        //Overall Variables
        /// <summary>
        /// How long it takes before the animation starts.
        /// </summary>
        public float delay;

        /// <summary>
        /// The total length of this animation.
        /// </summary>
        public float TotalLength;

        //Audio Variables
        /// <summary>
        /// If this animation uses a sound effect.
        /// </summary>
        public bool usesSoundEffect;

        /// <summary>
        /// The prefab of the sound effect.
        /// </summary>
        public AudioObjectHolder soundEffect;

        /// <summary>
        /// How long it takes before the sound effect starts.
        /// </summary>
        public float soundEffectDelay;

        //Move Variables
        /// <summary>
        /// If this animation uses a move animation.
        /// </summary>
        public bool usesMoveAnimation;

        /// <summary>
        /// If this animation starts at a start position.
        /// </summary>
        public bool useStartPosition;

        /// <summary>
        /// Where this move animation starts.
        /// </summary>
        public Vector2 startPosition;

        /// <summary>
        /// Where this move animation ends.
        /// </summary>
        public Vector2 endPosition;

        /// <summary>
        /// How long the move animation takes.
        /// </summary>
        public float moveAnimationTime;

        /// <summary>
        /// How long it takes before the move animation starts.
        /// </summary>
        public float moveDelay;

        /// <summary>
        /// The easing type this move animation uses.
        /// </summary>
        public Ease moveEaseType;

        //Fade Variables
        /// <summary>
        /// If this animation uses a fade animation.
        /// </summary>
        public bool usesFadeAnimation;

        /// <summary>
        /// If this animation starts at a fade value.
        /// </summary>
        public bool useStartFadeValue;

        /// <summary>
        /// The fade value this animation starts with.
        /// </summary>
        public float startFadeValue;

        /// <summary>
        /// The fade value this animation ends with.
        /// </summary>
        public float endFadeValue;

        /// <summary>
        /// How long this fade animation takes.
        /// </summary>
        public float fadeAnimationTime;

        /// <summary>
        /// How long it takes before this fade animation starts.
        /// </summary>
        public float fadeDelay;

        /// <summary>
        /// The easing type this fade animation uses.
        /// </summary>
        public Ease fadeEaseType;

        //Rotation Variables
        /// <summary>
        /// If this animation uses a rotation animation.
        /// </summary>
        public bool usesRotationAnimation;

        /// <summary>
        /// If this animation starts at a rotation value.
        /// </summary>
        public bool useStartRotation;

        /// <summary>
        /// The rotation value this animation starts with.
        /// </summary>
        public Vector3 startRotation;

        /// <summary>
        /// The rotation value this animation ends with.
        /// </summary>
        public Vector3 endRotation;

        /// <summary>
        /// How long this rotation animation takes.
        /// </summary>
        public float rotationAnimationTime;

        /// <summary>
        /// How long it takes before this rotation animation starts.
        /// </summary>
        public float rotationDelay;

        /// <summary>
        /// The easing type this rotation animation uses.
        /// </summary>
        public Ease rotationEaseType;

        /// <summary>
        /// Initializes this animation. it adds the current position of the object to the start and end position,
        /// creates the audio objects and determines the total length of this animation.
        /// </summary>
        /// <param name="_parent"></param>
        public void Initialize (Transform _parent) {

            AddObjectPosition(_parent.localPosition);
            CreateSoundObject(_parent);
            SetTotalAnimationLength();

        }

        private void AddObjectPosition (Vector3 _localPosition) {

            startPosition += new Vector2(_localPosition.x, _localPosition.y);
            endPosition += new Vector2(_localPosition.x, _localPosition.y);

        }

        /// <summary>
        /// Plays the Sound effect if it has one.
        /// </summary>
        public void PlaySound () {

            if (soundEffect.audioObject != null) {

                soundEffect.audioObject.Play();

            }

        }

        private void CreateSoundObject (Transform _parent) {

            if (soundEffect.objectPrefab != null) {

                soundEffect.audioObject = AudioManager.Instance.CreateAudioInstance(soundEffect.objectPrefab);
                soundEffect.audioObject.SetOnPosition(_parent);

            }

        }

        private void SetTotalAnimationLength () {

            List<float> timeTotal = new List<float>();

            //Get the length of the animations being used.
            if (usesFadeAnimation)
                timeTotal.Add(fadeAnimationTime + fadeDelay);
            if (usesMoveAnimation)
                timeTotal.Add(moveAnimationTime + moveDelay);
            if (usesRotationAnimation)
                timeTotal.Add(rotationAnimationTime + rotationDelay);

            TotalLength = Calculate.GetHighestFromList(timeTotal);

        }

    }

}
