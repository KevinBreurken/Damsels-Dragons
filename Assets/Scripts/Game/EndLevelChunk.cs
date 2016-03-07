using UnityEngine;
using System.Collections;

namespace Base.Game {

	public class EndLevelChunk : ChunkData {

		public bool isFinished = false;
        public ProjectileManager projectileManager;
        public EndLevelTrigger endTrigger;

        public override void EnableChunk () {

            base.EnableChunk();
            projectileManager.StartSpawning();
            endTrigger.ResetTrigger();

        }

        public override void DisableChunk () {

            projectileManager.StopSpawning();
            base.DisableChunk();

        }

    }

}