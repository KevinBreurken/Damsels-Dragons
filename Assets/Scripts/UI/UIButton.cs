using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Base.Audio;
using UnityEngine.EventSystems;

namespace Base.UI {

    [RequireComponent(typeof(Button),typeof(Image))]
    public class UIButton : UIObject {

        public delegate void ButtonEvent ();

        /// <summary>
        /// Called when the AudioObject is finished playing.
        /// </summary>
        public event ButtonEvent onClicked;

        [HideInInspector]
        public Button Button;
        
        //Sound
        public AudioObjectHolder clickSound;

        public override void Awake () {
            base.Awake();
            Button = GetComponent<Button>();
            Button.onClick.AddListener(() => OnButtonClicked());
        }

        private void OnButtonClicked () {
            if (onClicked != null) {
                onClicked();
            }
        }

        void Start () {
            //Create the Audio Object if it's configured in the editor.
            if(clickSound.objectPrefab != null)
            clickSound.audioObject = AudioManager.Instance.CreateAudioInstance(clickSound.objectPrefab);
         
        }

        // Used for debugging animations.
        void Update () {

            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.O)) {

                Show();

            }

            if (Input.GetKeyDown(KeyCode.P)) {

                StartCoroutine(Hide());

            }
            #endif

        }

    }

}
