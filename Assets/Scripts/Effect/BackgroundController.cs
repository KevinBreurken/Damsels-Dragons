using UnityEngine;
using System.Collections;
using Base.Game;

namespace Base.Effect {

    /// <summary>
    /// Handles the background.
    /// </summary>
    public class BackgroundController : MonoBehaviour {

        /// <summary>
        /// A holder for containing the backgrounds mesh and its scrolling speed.
        /// </summary>
        [System.Serializable]
        public class ScrollingBackgroundHolder {

            /// <summary>
            /// The background renderer.
            /// </summary>
			public MeshRenderer renderer;

            /// <summary>
            /// How fast the background moves.
            /// </summary>
            public float scrollingSpeed;

        }

        /// <summary>
        /// All background parts.
        /// </summary>
        public ScrollingBackgroundHolder[] backgrounds;

        private CameraController cameraController;
        /// <summary>
        /// The CameraController that will be used for scrolling.
        /// </summary>
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
				
				backgrounds[i].renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2((cameraController.transform.position.x / 100) * backgrounds[i].scrollingSpeed, 0));
               
            }

        }

    }

}
