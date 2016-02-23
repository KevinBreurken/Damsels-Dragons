﻿using UnityEngine;
using System.Collections;

namespace Base.Game.State {

    public class InGameState : BaseGameState {

        private LevelGenerator levelGenerator;
		public GameObject characterPrefab;
        public GameObject cameraPrefab;
		private PlayerController characterController;
        private CameraController cameraController;

        void Awake () {

            levelGenerator = GetComponent<LevelGenerator>();

            //Create Character
            if (characterController == null) {

                GameObject characterInstantiatedObject = (GameObject)Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                characterController = characterInstantiatedObject.GetComponent<PlayerController>();
                characterController.transform.parent = this.transform;
                characterController.SetAtStartPosition();

            }
            //Create Camera
            if (cameraController == null) {

                GameObject cameraInstantiatedObject = (GameObject)Instantiate(cameraPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                cameraController = cameraInstantiatedObject.GetComponent<CameraController>();
                cameraController.target = characterController.transform;

            }

        }

        public override void Enter () {

            base.Enter();

            Awake();

            levelGenerator.SetSpawnChunk();
            characterController.SetAtStartPosition();
            cameraController.SetAtStartPosition();
            cameraController.followTarget = true;

        }

    }

}
