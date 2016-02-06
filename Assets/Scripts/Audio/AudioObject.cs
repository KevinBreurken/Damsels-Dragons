using UnityEngine;
using System.Collections;

namespace Base.Audio {

    public class AudioObject : MonoBehaviour {

        private AudioSource source;

        public bool randomPitch;
        public Vector2 pitchRange;

        void Awake () {

            source = gameObject.GetComponent<AudioSource>();

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
            Play();

        }

        /// <summary>
        /// Plays a AudioClip normally.
        /// </summary>
        public void Play () {

            source.pitch = 1.0f + Random.Range(pitchRange.x, pitchRange.y);

            source.Play();

        }

        /// <summary>
        /// Plays a AudioClip with a fixed pitch.
        /// </summary>
        public void PlayWithFixedPitch (float _pitch) {

            source.pitch = _pitch;

            source.Play();

        }

    }

}
