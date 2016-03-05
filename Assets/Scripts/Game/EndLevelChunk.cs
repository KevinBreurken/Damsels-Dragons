using UnityEngine;
using System.Collections;

namespace Base.Game {

	public class EndLevelChunk : ChunkData {

		public bool isFinished = false;
        public ProjectileManager projectileManager;
    
        public override void EnableChunk () {

            base.EnableChunk();
            projectileManager.StartSpawning();

        }

        public override void DisableChunk () {

            
            projectileManager.StopSpawning();
            base.DisableChunk();

        }

    }

}