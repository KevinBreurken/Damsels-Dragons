using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Audio {

    [AddComponentMenu("Manager/AudioManager")]
    public class AudioManager : MonoBehaviour {
        
        public List<AudioListData> audioClips;
        public bool hideAudioInHierarchy;

        private static AudioManager instance = null;
        public static AudioManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent(typeof(AudioManager)) as AudioManager;

                }

                return instance;

            }

        }

        /// <summary>
        /// Creates a new AudioObject.
        /// </summary>
        /// <param name="_identifier">The identifier name.</param>
        public AudioObject CreateAudioInstance (string _identifier) {

            GameObject audioGameObject = new GameObject();
            audioGameObject.hideFlags = hideAudioInHierarchy ? HideFlags.HideInHierarchy : HideFlags.None;
            audioGameObject.name = "[AudioObject] " + _identifier;
            audioGameObject.transform.parent = this.transform;

            AudioObject audioObject = audioGameObject.AddComponent<AudioObject>();

            return audioObject;

        }

        private AudioClip GetClipByName(string _name) {

            for (int i = 0; i < audioClips.Count; i++) {

                if(_name == audioClips[i].identifier) {

                    return null;

                }

            }

            Debug.LogError("AUDIOCLIP: " + _name + " NOT FOUND.");
            return null;

        }

        void OnApplicationQuit () {

            instance = null;

        }

    }

    [System.Serializable]
    public class AudioListData {

        public string identifier;
        public GameObject audioObject;

    }

}