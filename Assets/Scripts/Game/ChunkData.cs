using UnityEngine;
using System.Collections;

namespace Base.Game {

    public class ChunkData : MonoBehaviour {

        /// <summary>
        /// The ending position of this chunk,
        /// This position is used for placing the next chunk.
        /// </summary>
        public Transform endPoint;

        private int chunkLenght;

        void Awake () {

            SetChunkLength();

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

            gameObject.SetActive(true);
            transform.position = _position;

        }

        /// <summary>
        /// Disables this chunk.
        /// </summary>
        public void DisableChunk () {

            gameObject.SetActive(false);

        }

    }

   
}
