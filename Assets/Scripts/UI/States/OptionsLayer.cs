using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Base.UI.States {

    public class OptionsLayer : MonoBehaviour {

        public RenderImage contrast;
        public Slider contrastSlider;

        public Slider MasterSlider;
        public Slider MusicSlider;
        public Slider SFXSlider;
        public Slider InterfaceSlider;

        public AudioMixer mixer;

        // Use this for initialization
        void Awake () {

            contrastSlider.onValueChanged.AddListener(delegate { ContrastChanged(); });
            MasterSlider.onValueChanged.AddListener(delegate { OnMasterChanged(); });
            MusicSlider.onValueChanged.AddListener(delegate { OnMusicChanged(); });
            SFXSlider.onValueChanged.AddListener(delegate { OnSFXChanged(); });
            InterfaceSlider.onValueChanged.AddListener(delegate { OnUIChanged(); });

            SetDefaultMusicValues();
            SetSliders();

        }

        void OnMasterChanged () {
            PlayerPrefs.SetFloat("Volume_Master", MasterSlider.value);
            mixer.SetFloat("Volume_Master", MasterSlider.value);
        }
        void OnMusicChanged () {
            PlayerPrefs.SetFloat("Volume_Music", MusicSlider.value);
            mixer.SetFloat("Volume_Music", MusicSlider.value);
        }
        void OnSFXChanged () {
            PlayerPrefs.SetFloat("Volume_SFX", SFXSlider.value);
            mixer.SetFloat("Volume_SFX", SFXSlider.value);
        }
        void OnUIChanged () {
            PlayerPrefs.SetFloat("Volume_UI", InterfaceSlider.value);
            mixer.SetFloat("Volume_UI", InterfaceSlider.value);
        }

        void SetSliders () {

            contrastSlider.value = contrast.brightnessAmount;
            contrast.brightnessAmount = PlayerPrefs.GetFloat("Contrast");
            contrastSlider.value = contrast.brightnessAmount;

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

        void SetDefaultMusicValues () {

            float val;
            mixer.GetFloat("Volume_Master", out val);
            PlayerPrefs.SetFloat("Volume_Master", val);

            mixer.GetFloat("Volume_SFX", out val);
            PlayerPrefs.SetFloat("Volume_SFX", val);

            mixer.GetFloat("Volume_Music", out val);
            PlayerPrefs.SetFloat("Volume_Music", val);

            mixer.GetFloat("Volume_UI", out val);
            PlayerPrefs.SetFloat("Volume_UI", val);

        }

        void ContrastChanged () {

            contrast.brightnessAmount = contrastSlider.value;
            PlayerPrefs.SetFloat("Contrast", contrast.brightnessAmount);

        }


    }

}
