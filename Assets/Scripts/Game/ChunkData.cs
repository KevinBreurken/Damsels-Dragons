using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Game {

    public class ChunkData : MonoBehaviour {

        /// <summary>
        /// The ending position of this chunk,
        /// This position is used for placing the next chunk.
        /// </summary>
        public Transform endPoint;

        private int chunkLenght;
        private CoinPickup[] coinpickups;

        public virtual void Awake () {

            SetChunkLength();

            //Get all coins within the chunk.
            Transform pickupHolder = transform.FindChild("Pickups");
            if(pickupHolder != null) {

                coinpickups = pickupHolder.GetComponentsInChildren<CoinPickup>();

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

            if (coinpickups != null) {

                for (int i = 0; i < coinpickups.Length; i++) {

                    coinpickups[i].Reset();

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
