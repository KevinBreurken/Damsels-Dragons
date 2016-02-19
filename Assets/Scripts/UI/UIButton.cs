using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Base.Audio;
namespace Base.UI {

    [RequireComponent(typeof(Button),typeof(Image))]
    public class UIButton : UIObject {

        [HideInInspector]
        public Button Button;
        
        //Sound
        public AudioObjectHolder clickSound;

        void Awake () {
           
            Button = GetComponent<Button>();

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

                Hide();

            }
            #endif

        }

    }

}
