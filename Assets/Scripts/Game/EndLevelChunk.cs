using UnityEngine;
using System.Collections;

namespace Base.Game {

    /// <summary>
    /// The chunk that is located at the end of the level.
    /// </summary>
	public class EndLevelChunk : ChunkData {

        /// <summary>
        /// If this chunk is finished.
        /// </summary>
		public bool isFinished = false;

        /// <summary>
        /// The ProjectileManager that spawns the projectiles.
        /// </summary>
        public ProjectileManager projectileManager;

        /// <summary>
        /// The trigger that causes the level to end (the head of the dragon)
        /// </summary>
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