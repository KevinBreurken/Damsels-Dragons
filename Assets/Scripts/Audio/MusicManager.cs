using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Management;
using DG.Tweening;

namespace Base.Audio {

    public class MusicManager : MonoBehaviour {

        /// <summary>
        /// List of all AudioObjects with its identifier.
        /// </summary>
        public List<ListData> audioList;

        public List<AudioObject> createdSongObjects;

        public float musicFadeOutSpeed;

        /// <summary>
        /// If the AudioObjects are visible in the hierarchy.
        /// </summary>
        public bool hideSongsInHierarchy;

        private static MusicManager instance = null;
        /// <summary>
        /// Static reference of the AudioManager.
        /// </summary>
        public static MusicManager Instance {

            get {

                if (instance == null) {

                    instance = FindObjectOfType(typeof(MusicManager)) as MusicManager;

                }

                if (instance == null) {

                    GameObject go = new GameObject("MusicManager");
                    instance = go.AddComponent(typeof(MusicManager)) as MusicManager;

                }

                return instance;

            }

        }

        void Awake () {

            for (int i = 0; i < audioList.Count; i++) {

                AudioObject aObject = CreateAudioInstance(audioList[i].listedObject);
                aObject.name = audioList[i].listedObject.name;
                createdSongObjects.Add(aObject);

            }

        }

        public IEnumerator TryToPlaySong (AudioObject _song) {

            AudioObject objectThatIsPlaying = IsASongPlaying();

            if(objectThatIsPlaying != null) {

                yield return StartCoroutine(StopSong(objectThatIsPlaying));

            }

            _song.Play();

        }

        public IEnumerator StopSong (AudioObject _song) {

            _song.FadeVolume(1,0, 1);
            yield return new WaitForSeconds(1);
            _song.GetSource().Stop();

        }

        private AudioObject CreateAudioInstance (GameObject _object) {

            GameObject audioGameObject = Instantiate(_object);
            audioGameObject.hideFlags = hideSongsInHierarchy ? HideFlags.HideInHierarchy : HideFlags.None;
            audioGameObject.name = "[SongObject] " + _object.name;
            audioGameObject.transform.parent = this.transform;

            AudioObject audioObject = audioGameObject.GetComponent<AudioObject>();

            return audioObject;

        }

        private AudioObject IsASongPlaying () {

            AudioObject check = null;

            for (int i = 0; i < createdSongObjects.Count; i++) {
                if (createdSongObjects[i].GetSource().isPlaying) {
                    check = createdSongObjects[i];
                }
            }

            return check;

        }

        public AudioObject GetSongByName(string _name) {

            AudioObject aObject = null;

            for (int i = 0; i < createdSongObjects.Count; i++) {

                if(createdSongObjects[i].name == _name) {

                    aObject = createdSongObjects[i];

                }

            }

            if(aObject == null) {

                Debug.LogError("Song with name: " + _name + ". Not found.");

            }

            return aObject;

        }

    }

}
