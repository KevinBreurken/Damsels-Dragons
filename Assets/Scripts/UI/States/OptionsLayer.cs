using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Base.UI.States {

    /// <summary>
    /// Layer that contains all options related GUI content.
    /// </summary>
    public class OptionsLayer : MonoBehaviour {

        /// <summary>
        /// The script that handles contrast.
        /// </summary>
        public RenderImage contrastScript;

        public Slider contrastSlider;
        public Slider MasterSlider;
        public Slider MusicSlider;
        public Slider SFXSlider;
        public Slider InterfaceSlider;

        /// <summary>
        /// Reference to the AudioMixer.
        /// </summary>
        public AudioMixer mixer;

        // Use this for initialization
        void Awake () {

            contrastSlider.onValueChanged.AddListener(delegate { ContrastChanged(); });
            MasterSlider.onValueChanged.AddListener(delegate { OnMasterChanged(); });
            MusicSlider.onValueChanged.AddListener(delegate { OnMusicChanged(); });
            SFXSlider.onValueChanged.AddListener(delegate { OnSFXChanged(); });
            InterfaceSlider.onValueChanged.AddListener(delegate { OnUIChanged(); });

            //SetDefaultMusicValues();
            SetSliders();

        }

        private void OnMasterChanged () {

            PlayerPrefs.SetFloat("Volume_Master", MasterSlider.value);
            mixer.SetFloat("Volume_Master", MasterSlider.value);

        }

        private void OnMusicChanged () {

            PlayerPrefs.SetFloat("Volume_Music", MusicSlider.value);
            mixer.SetFloat("Volume_Music", MusicSlider.value);

        }

        private void OnSFXChanged () {

            PlayerPrefs.SetFloat("Volume_SFX", SFXSlider.value);
            mixer.SetFloat("Volume_SFX", SFXSlider.value);

        }

        private void OnUIChanged () {

            PlayerPrefs.SetFloat("Volume_UI", InterfaceSlider.value);
            mixer.SetFloat("Volume_UI", InterfaceSlider.value);

        }

        private void SetSliders () {

            contrastSlider.value = contrastScript.brightnessAmount;
            contrastScript.brightnessAmount = PlayerPrefs.GetFloat("Contrast");
            contrastSlider.value = contrastScript.brightnessAmount;

            float masterValue = PlayerPrefs.GetFloat("Volume_Master");
            MasterSlider.value = masterValue;
            mixer.SetFloat("Volume_Master", masterValue);

            float musicValue = PlayerPrefs.GetFloat("Volume_Music");
            MusicSlider.value = musicValue;
            mixer.SetFloat("Volume_Music", musicValue);

            float sfxValue = PlayerPrefs.GetFloat("Volume_SFX");
            SFXSlider.value = sfxValue;
            mixer.SetFloat("Volume_SFX", sfxValue);

            float interfaceValue = PlayerPrefs.GetFloat("Volume_UI");
            InterfaceSlider.value = interfaceValue;
            mixer.SetFloat("Volume_UI", interfaceValue);

        }

        private void SetDefaultMusicValues () {

            float outputValue;

            mixer.GetFloat("Volume_Master", out outputValue);
            PlayerPrefs.SetFloat("Volume_Master", outputValue);

            mixer.GetFloat("Volume_SFX", out outputValue);
            PlayerPrefs.SetFloat("Volume_SFX", outputValue);

            mixer.GetFloat("Volume_Music", out outputValue);
            PlayerPrefs.SetFloat("Volume_Music", outputValue);

            mixer.GetFloat("Volume_UI", out outputValue);
            PlayerPrefs.SetFloat("Volume_UI", outputValue);

        }

        private void ContrastChanged () {

            contrastScript.brightnessAmount = contrastSlider.value;
            PlayerPrefs.SetFloat("Contrast", contrastScript.brightnessAmount);

        }


    }

}
