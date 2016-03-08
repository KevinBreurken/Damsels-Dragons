using UnityEngine;
using System.Collections;
using Base.Effect;
using Base.UI;
using Base.Audio;

namespace Base.Game.State {

    public class InGameState : BaseGameState {

        private LevelGenerator levelGenerator;
		public GameObject characterPrefab;
        public GameObject cameraPrefab;
        public GameObject backgroundCameraPrefab;

		private PlayerController characterController;
        private CameraController cameraController;
        private BackgroundController backgroundController;

        void Awake () {
            
            levelGenerator = GetComponent<LevelGenerator>();

            //Create character.
            if (characterController == null) {

                GameObject characterInstantiatedObject = (GameObject)Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                characterController = characterInstantiatedObject.GetComponent<PlayerController>();
                characterController.transform.parent = this.transform;
                characterController.SetAtStartPosition();

            }
            
            //Create camera.
            if (cameraController == null) {

                GameObject cameraInstantiatedObject = (GameObject)Instantiate(cameraPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                cameraController = cameraInstantiatedObject.GetComponent<CameraController>();
                cameraController.target = characterController.transform;
				cameraController.transform.parent = this.transform;

            }

			//Create background.
            if (backgroundController == null) {

                GameObject backgroundCameraInstantiatedObject = (GameObject)Instantiate(backgroundCameraPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                backgroundController = backgroundCameraInstantiatedObject.GetComponent<BackgroundController>();
                backgroundController.CameraController = cameraController;
				backgroundController.transform.parent = this.transform;

            }

			characterController.SetCameraReference(cameraController);
            characterController.SetStateReference(this);
            
        }

        public override void Enter () {

            base.Enter();
            
            Awake();

            levelGenerator.SetSpawnChunk();
            characterController.SetAtStartPosition();
            cameraController.SetAtStartPosition();
            cameraController.followTarget = true;
            
        }

        public override IEnumerator Exit () {

            levelGenerator.Unload();

            return base.Exit();

        }

        public void EndLevel () {

            Debug.Log("Level is ended");
            levelGenerator.GenerateNewLevel();
            characterController.OnLevelComplete();

        }

        public void LeaveGame () {

            UIStateSelector.Instance.SetState("MenuUIState");

        }

    }

}
