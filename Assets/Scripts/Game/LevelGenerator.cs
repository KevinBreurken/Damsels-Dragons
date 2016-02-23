﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Game {

    public class LevelGenerator : MonoBehaviour {

        public GameObject spawnChunkPrefab;
        public GameObject endChunkPrefab;
        public List<GameObject> chunkPrefabs;

        private ChunkData spawnChunk;
        private List<ChunkData> endChunk = new List<ChunkData>();
        private List<ChunkData> availableChunks = new List<ChunkData>();
        private List<ChunkData> usedChunks = new List<ChunkData>();
        private GameObject chunkHolder;

        private int generatedLength;
        private int lengthToGenerate;
        private ChunkData lastSpawnedChunk;
        private int lastEndPoint = 0;

        void Awake () {
            //Create a holder for all the chunks.
            chunkHolder = new GameObject("Chunk Holder");
            chunkHolder.transform.parent = this.transform;

            //Create all chunks.
            spawnChunk = CreateChunk(spawnChunkPrefab);
            endChunk.Add(CreateChunk(endChunkPrefab));
            endChunk.Add(CreateChunk(endChunkPrefab));

            for (int i = 0; i < chunkPrefabs.Count; i++) {

                ChunkData chunk = CreateChunk(chunkPrefabs[i]);
                availableChunks.Add(chunk);

            }

        }

        void FixedUpdate () {

            if (Input.GetKeyDown(KeyCode.Space)) {

                //GenerateNewLevel();

            }

        }

        /// <summary>
        /// Creates a new Chunk.
        /// </summary>
        /// <param name="_prefab">The chunk prefab.</param>
        /// <returns>The ChunkData of the chunk.</returns>
        public ChunkData CreateChunk(GameObject _prefab) {

            GameObject instantiatedObject = Instantiate(_prefab,new Vector3(0,0,0),Quaternion.identity) as GameObject;
            instantiatedObject.transform.parent = chunkHolder.transform;
            instantiatedObject.name = _prefab.name;
            instantiatedObject.SetActive(false);
      
            return instantiatedObject.GetComponent<ChunkData>();      

        }

        /// <summary>
        /// Places the spawn chunk.
        /// </summary>
        public void SetSpawnChunk () {
            //Place the spawn Chunk.
            spawnChunk.SetChunkPosition(new Vector3(-11f, 0, 0));
            generatedLength = spawnChunk.GetChunkLength();
            lengthToGenerate = 60;
            GenerateLevel(spawnChunk);

        }

        /// <summary>
        /// Generates a new level.
        /// </summary>
        public void GenerateNewLevel () {

            for (int i = 0; i < usedChunks.Count; i++) {

                ChunkData chunk = usedChunks[i];
                chunk.DisableChunk();
                availableChunks.Add(chunk);

            }

            spawnChunk.DisableChunk();
            usedChunks.Clear();
            generatedLength = spawnChunk.GetChunkLength();
            lengthToGenerate = 60;
            GenerateLevel(lastSpawnedChunk);

        }

        private void GenerateLevel (ChunkData _previousChunk) {

            if(generatedLength >= lengthToGenerate) {
                //Generating level is finished, place end chunk.
                endChunk[lastEndPoint].SetChunkPosition(_previousChunk.endPoint.position);
                lastSpawnedChunk = endChunk[lastEndPoint];
                lastEndPoint++;

                if(lastEndPoint == 2) {

                    lastEndPoint = 0;

                }

            } else {

                ChunkData randomChunk = availableChunks[Random.Range(0, availableChunks.Count)];
                availableChunks.Remove(randomChunk);
                usedChunks.Add(randomChunk);
                randomChunk.SetChunkPosition(_previousChunk.endPoint.position);
                generatedLength += randomChunk.GetChunkLength();
                lastSpawnedChunk = randomChunk;
                GenerateLevel(randomChunk);

            }

        }

    }

}