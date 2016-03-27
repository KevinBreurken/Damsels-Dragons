using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Game {

    /// <summary>
    /// Holds data thats contained in a chunk.
    /// </summary>
    public class ChunkData : MonoBehaviour {

        /// <summary>
        /// The ending position of this chunk,
        /// This position is used for placing the next chunk.
        /// </summary>
        public Transform endPoint;

        private int chunkLenght;
        private CoinPickup[] coinPickups;

        public virtual void Awake () {

            SetChunkLength();

            //Get all coins within the chunk.
            Transform pickupHolder = transform.FindChild("Pickups");
            if(pickupHolder != null) {

                coinPickups = pickupHolder.GetComponentsInChildren<CoinPickup>();

            }

        }

        /// <summary>
        /// Sets the length of this chunk.
        /// </summary>
        private void SetChunkLength () {

            float chunkXPosition = transform.position.x;
            float chunkXEndPosition = endPoint.position.x;
            chunkLenght = (int)Mathf.Abs((chunkXPosition - chunkXEndPosition) / 1.6f);

        }

        /// <summary>
        /// Returns the length of this chunk.
        /// </summary>
        /// <returns>The length in tile amount.</returns>
        public int GetChunkLength () {

            return chunkLenght;

        }

        /// <summary>
        /// Places this chunk at the given position.
        /// </summary>
        /// <param name="_position">The position this chunk will be placed at.</param>
        public void SetChunkPosition (Vector3 _position) {

            transform.position = _position;

        }

        /// <summary>
        /// Enables this chunk.
        /// </summary>
        public virtual void EnableChunk () {

            gameObject.SetActive(true);

            if (coinPickups != null) {

                for (int i = 0; i < coinPickups.Length; i++) {

                    coinPickups[i].Reset();

                }

            }

        }

        /// <summary>
        /// Disables this chunk.
        /// </summary>
        public virtual void DisableChunk () {

            gameObject.SetActive(false);

        }

    }

   
}
