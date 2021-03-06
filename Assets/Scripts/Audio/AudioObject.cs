﻿using UnityEngine;
using System.Collections;
using Base.Management;
using DG.Tweening;

namespace Base.Audio {

    /// <summary>
    /// Plays AudioClips.
    /// </summary>
    public class AudioObject : MonoBehaviour {

        public delegate void AudioEvent ();

        /// <summary>
        /// Called when the AudioObject is finished playing.
        /// </summary>
        public event AudioEvent onAudioFinished;

        private AudioSource source;

        /// <summary>
        /// If the AudioObject uses a random pitch.
        /// </summary>
        public bool randomPitch;

        /// <summary>
        /// The range of the random pitch.
        /// </summary>
        public Vector2 randomPitchRange;


        void Awake () {

            source = gameObject.GetComponent<AudioSource>();

        }

        /// <summary>
        /// Used for finding the ObjectPoolReturnReference. This can't be done in the awake function.
        /// </summary>
        void Start () {

            if (GetComponent<ObjectPoolReturnReference>() != null) {
               
                onAudioFinished += GetComponent<ObjectPoolReturnReference>().ReturnToPool;

            }

        }

        /// <summary>
        /// Used for disabling the AudioObject after its finished.
        /// </summary>
        private IEnumerator WaitForClip () {
            yield return new WaitForSeconds(source.clip.length);

            if (onAudioFinished != null) {

                onAudioFinished();

            }

        }

        /// <summary>
        /// Plays a AudioClip at given position.
        /// </summary>
        /// <param name="_position">The given position.</param>
        public void PlayAtPosition (Vector3 _position) {

            gameObject.transform.position = _position;
            Play();

        }

        /// <summary>
        /// Plays a AudioClip while being attached to a Transform.
        /// </summary>
        /// <param name="_parent">The Transform this AudioObject will be attached to.</param>
        public void PlayOnPosition (Transform _parent) {

            gameObject.transform.parent = _parent;
            gameObject.transform.position = new Vector3(0, 0, 0);
            Play();

        }

        /// <summary>
        /// Attaches this AudioObject to a given transform.
        /// </summary>
        /// <param name="_parent">The transform this audioObject will be parented to.</param>
        public void SetOnPosition(Transform _parent) {

            gameObject.transform.parent = _parent;
            gameObject.transform.position = new Vector3(0, 0, 0);

        }

        /// <summary>
        /// Plays a AudioClip normally.
        /// </summary>
        public void Play () {

            if(source == null) {

                source = GetComponent<AudioSource>();

            }

            if (randomPitch) {

                source.pitch = 1.0f + Random.Range(randomPitchRange.x, randomPitchRange.y);

            }

            source.Play();
            StartCoroutine(WaitForClip());

        }

        /// <summary>
        /// Plays a AudioClip with a fixed pitch.
        /// </summary>
        public void PlayWithFixedPitch (float _pitch) {

            if (source == null) {

                source = GetComponent<AudioSource>();
                
            }

            source.pitch = _pitch;

            source.Play();

        }

        /// <summary>
        /// Adjusts the volume of this AudioObject.
        /// </summary>
        /// <param name="_volume"></param>
        public void SetVolume(float _volume) {

            source.volume = _volume;

        }

        /// <summary>
        /// Fades the AudiObject's volume.
        /// </summary>
        /// <param name="_startVolume">The starting volume.</param>
        /// <param name="_endVolume">The end volume.</param>
        /// <param name="_duration">The fade duration.</param>
        public void FadeVolume(float _startVolume, float _endVolume,float _duration) {

            source.volume = _startVolume;
            source.DOFade(_endVolume, _duration);
           
        }

        /// <summary>
        /// Returns the AudioSource of this AudioObject.
        /// </summary>
        /// <returns>The AudioSource.</returns>
        public AudioSource GetSource () {

            return source;

        }

    }

}
