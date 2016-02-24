using UnityEngine;
using System.Collections;
using Base.Game;

namespace Base.Effect {

    public class BackgroundController : MonoBehaviour {

        [System.Serializable]
        public class ScrollingBackgroundHolder {

            public Material background;
            public float scrollingSpeed;

        }

        public ScrollingBackgroundHolder[] backgrounds;
        private CameraController cameraController;
        public CameraController CameraController {

            get {
       
                return cameraController;

            }

            set {

                if(cameraController != null) {

                    cameraController.onCameraScrolled -= OnCameraScrolled;

                }

                cameraController = value;
                cameraController.onCameraScrolled += OnCameraScrolled;
                //Set the background to proper position.
                OnCameraScrolled();

            }

        }

        private void OnCameraScrolled () {

            for (int i = 0; i < backgrounds.Length; i++) {
                backgrounds[i].background.SetTextureOffset("_MainTex", new Vector2((cameraController.transform.position.x / 100) * backgrounds[i].scrollingSpeed, 0));
            }
            

        }

    }

}
