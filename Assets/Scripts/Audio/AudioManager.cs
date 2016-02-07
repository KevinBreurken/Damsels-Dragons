using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Management;

namespace Base.Audio {

    /// <summary>
    /// Handles creation of new AudioObjects and ObjectPools for AudioObjects.
    /// </summary>
    [AddComponentMenu("Manager/AudioManager")]
    public class AudioManager : MonoBehaviour {
        
        /// <summary>
        /// List of all AudioObjects with its identifier.
        /// </summary>
        public List<AudioListData> audioList;

        /// <summary>
        /// If the AudioObjects are visible in the hierarchy.
        /// </summary>
        public bool hideAudioInHierarchy;

        private static AudioManager instance = null;
        /// <summary>
        /// Static reference of the AudioManager.
        /// </summary>
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
        /// <param name="_identifier">The AudioObjects identifier as in the audioList.</param>
        /// <returns>The created AudioObject.</returns>
        public AudioObject CreateAudioInstance (string _identifier) {

            GameObject audioGameObject = Instantiate(GetClipByName(_identifier));
            audioGameObject.hideFlags = hideAudioInHierarchy ? HideFlags.HideInHierarchy : HideFlags.None;
            audioGameObject.name = "[AudioObject] " + _identifier;
            audioGameObject.transform.parent = this.transform;

            AudioObject audioObject = audioGameObject.GetComponent<AudioObject>();

            return audioObject;

        }

        /// <summary>
        /// Creates a new ObjectPool with AudioObjects.
        /// </summary>
        /// <param name="_identifier">The AudioObjects identifier as in the audioList.</param>
        /// <param name="_startingAmount">The amount of objects that will be created.</param>
        /// <param name="_createsNewObjects">If this ObjectPool will create new objects when its full.</param>
        /// <returns>The created ObjectPool.</returns>
        public ObjectPool CreateAudioPoolInstance(string _identifier,int _startAmount,bool _createsNewObjects) {

            GameObject objectPoolObject = new GameObject();
            objectPoolObject.name = "[ObjectPool] " + _identifier;

            ObjectPool pool = objectPoolObject.AddComponent<ObjectPool>();
            pool.Initialize(GetClipByName(_identifier), _startAmount, _createsNewObjects);

            return pool;

        }

        private GameObject GetClipByName(string _name) {

            for (int i = 0; i < audioList.Count; i++) {
 
                if(_name == audioList[i].audioObject.name) {

                    return audioList[i].audioObject;

                }

            }

            Debug.LogError("AUDIOCLIP: " + _name + " NOT FOUND.");
            return null;

        }

        void OnApplicationQuit () {

            instance = null;

        }

    }

    /// <summary>
    /// A structure for holding AudioObjects.
    /// </summary>
    [System.Serializable]
    public struct AudioListData {

        /// <summary>
        /// The held AudioObject (as GameObject).
        /// </summary>
        public GameObject audioObject;

    }

}