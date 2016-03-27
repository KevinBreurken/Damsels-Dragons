using UnityEngine;
using System.Collections;

namespace Base.Audio {

    /// <summary>
    /// A holder that contains the prefab and a reference
    /// to the AudioObject.
    /// </summary>
    [System.Serializable]
    public struct AudioObjectHolder {
        /// <summary>
        /// The prefab that holds the
        /// </summary>
        public GameObject objectPrefab;

        /// <summary>
        /// The audioObject of the instantiated prefab.
        /// </summary>
        [HideInInspector]
        public AudioObject audioObject;

        /// <summary>
        /// Creates the AudioObject.
        /// </summary>
        public void CreateAudioObject () {

            if (objectPrefab != null) {

                audioObject = AudioManager.Instance.CreateAudioInstance(objectPrefab);

            } else {

                Debug.LogWarning("Audio Prefab not set");

            }

        }

        public AudioObject GetAudioObject () {

            if (audioObject != null) {

                return audioObject;

            } else {

                Debug.LogError("AudioObject not created, is CreateAudiObject being used?");
                return null;

            }
                
        }

    }

}