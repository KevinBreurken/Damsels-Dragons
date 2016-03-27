using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Management;
using DG.Tweening;

namespace Base.Audio {

    /// <summary>
    /// Handles Playing / Switching / Stop music.
    /// </summary>
    public class MusicManager : MonoBehaviour {

        /// <summary>
        /// List of all AudioObjects with its identifier.
        /// </summary>
        public List<ListData> audioList;

        /// <summary>
        /// The AudioObjects that are created.
        /// </summary>
        public List<AudioObject> createdSongObjects;

        /// <summary>
        /// If the AudioObjects are visible in the hierarchy.
        /// </summary>
        public bool hideSongsInHierarchy;

        /// <summary>
        /// How fast the music fades out.
        /// </summary>
        public float musicFadeOutSpeed = 1;

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

        /// <summary>
        /// Tries to play a song, if a song is playing; stop that song.
        /// </summary>
        /// <param name="_song">The song that will be played.</param>
        public IEnumerator TryToPlaySong (AudioObject _song) {

            AudioObject objectThatIsPlaying = IsASongPlaying();

            if(objectThatIsPlaying != null) {

                yield return StartCoroutine(StopSong(objectThatIsPlaying));

            }

            _song.Play();

        }

        /// <summary>
        /// Fades the music out and stops it.
        /// </summary>
        /// <param name="_song">The song that will be stopped.</param>
        public IEnumerator StopSong (AudioObject _song) {

            _song.FadeVolume(_song.GetSource().volume,0, musicFadeOutSpeed);
            yield return new WaitForSeconds(musicFadeOutSpeed);
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

        /// <summary>
        /// Checks if a song is playing.
        /// </summary>
        /// <returns>The AudioObject that is playing.</returns>
        private AudioObject IsASongPlaying () {

            AudioObject check = null;

            for (int i = 0; i < createdSongObjects.Count; i++) {
                if (createdSongObjects[i].GetSource().isPlaying) {
                    check = createdSongObjects[i];
                }
            }

            return check;

        }

        /// <summary>
        /// Tries to find the AudioObject by its name.
        /// </summary>
        /// <param name="_name">The name of the song.</param>
        /// <returns>The found AudioObject.</returns>
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
