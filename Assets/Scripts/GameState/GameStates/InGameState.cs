using UnityEngine;
using System.Collections;
using Base.Effect;
using Base.UI;
using Base.Audio;
using Base.UI.State;

namespace Base.Game.State {

    /// <summary>
    /// The game state that contains the actual game.
    /// </summary>
    public class InGameState : BaseGameState {

        /// <summary>
        /// Reference to the game UI state.
        /// </summary>
        public GameUIState uiState;

        /// <summary>
        /// Prefab of the character.
        /// </summary>
		public GameObject characterPrefab;

        /// <summary>
        /// Prefab of the Game Camera.
        /// </summary>
        public GameObject gameCameraPrefab;

        /// <summary>
        /// Prefab of the Background Camera.
        /// </summary>
        public GameObject backgroundCameraPrefab;

        /// <summary>
        /// Reference to the progress bar.
        /// </summary>
        public ProgressBar progressBar;

        private LevelGenerator levelGenerator;
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

                GameObject cameraInstantiatedObject = (GameObject)Instantiate(gameCameraPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                cameraController = cameraInstantiatedObject.GetComponent<CameraController>();
                cameraController.target = characterController.transform;
				cameraController.transform.parent = this.transform;
                Score.ScoreManager.Instance.gameViewCamera = cameraController.gameViewCamera;

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
            progressBar.SetTarget(characterController.transform);

        }


        /// <summary>
        /// Called when this state is entered.
        /// </summary>
        public override void Enter () {

            base.Enter();
            
            Awake();

            levelGenerator.SetSpawnChunk();
            characterController.SetAtStartPosition();
            cameraController.SetAtStartPosition();
            cameraController.followTarget = true;
            Score.ScoreManager.Instance.ResetScore();
            progressBar.SetValues(characterController.transform.position.x, levelGenerator.GetLastChunk().transform.position.x);
            StartCoroutine(WaitForCharacterControl());

        }

        /// <summary>
        /// Called when this state is left.
        /// </summary>
        public override IEnumerator Exit () {

            levelGenerator.Unload();

            return base.Exit();

        }

        /// <summary>
        /// Called when a level is ended.
        /// </summary>
        public void EndLevel () {

            Debug.Log("Level is ended");
            levelGenerator.GenerateNewLevel();
            characterController.OnLevelComplete();
            progressBar.SetValues(characterController.transform.position.x, levelGenerator.GetLastChunk().transform.position.x);
            StartCoroutine(WaitForCharacterControlWithLevel());

        }

        private IEnumerator WaitForCharacterControlWithLevel () {

            characterController.isControlledByPlayer = false;
            StartCoroutine(uiState.PlayNextLevelNotificationWithLevel(levelGenerator.currentLevel));
            yield return new WaitForSeconds(3);
            uiState.SetLevelCounterText(levelGenerator.currentLevel);
            characterController.isControlledByPlayer = true;

        }

        /// <summary>
        /// Waits to give control back to the player.
        /// </summary>
        private IEnumerator WaitForCharacterControl () {

            characterController.isControlledByPlayer = false;
            StartCoroutine(uiState.PlayNextLevelNotification());

            yield return new WaitForSeconds(2);

            characterController.isControlledByPlayer = true;
            uiState.SetLevelCounterText(levelGenerator.currentLevel);

        }

        /// <summary>
        /// Called to leave this game state.
        /// </summary>
        public void LeaveGame () {

            UIStateSelector.Instance.SetState("MenuUIState");

        }

    }

}
