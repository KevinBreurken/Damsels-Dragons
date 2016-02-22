using UnityEngine;
using System.Collections;

namespace Base.Game.State {

    public class InGameState : BaseGameState {

        private LevelGenerator levelGenerator;
		public GameObject characterPrefab;
		private PlayerController character;

        void Awake () {

            levelGenerator = GetComponent<LevelGenerator>();
			GameObject characterInstantiatedObject =  (GameObject)Instantiate(characterPrefab,new Vector3(0,0,0),Quaternion.identity) as GameObject;
			character = characterInstantiatedObject.GetComponent<PlayerController>();

        }

        public override void Enter () {

            base.Enter();
            levelGenerator.SetSpawnChunk();

        }

    }

}
