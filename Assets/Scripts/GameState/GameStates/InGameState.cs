using UnityEngine;
using System.Collections;

namespace Base.Game.State {

    public class InGameState : BaseGameState {

        private LevelGenerator levelGenerator;

        void Awake () {

            levelGenerator = GetComponent<LevelGenerator>();

        }

        public override void Enter () {

            base.Enter();
            levelGenerator.SetSpawnChunk();

        }

    }

}
