﻿using UnityEngine;
using System.Collections;
using Base.Management;
using System.Collections.Generic;

namespace Base.Game {

    public class ProjectileManager : MonoBehaviour {

		public GameObject fireProjectilePrefab;
        public GameObject stoneProjectilePrefab;
        public ProjectileSequence[] projectileSequences;
        public float[] delayTimes;

        private List<ProjectileMovement> spawnedProjectiles = new List<ProjectileMovement>();

		private ObjectPool fireProjectilePool;
        private ObjectPool stoneProjectilePool;

        private bool canSpawn;
        private int amountToSpawn;
        private int currentSpawnedProjectileIndex;
        private float delayTime;
		private float factorSpeed;
        private ProjectileSequence currentSequence;

        void Awake () {

            //For the fire projectiles
            GameObject firePoolObject = new GameObject();
            firePoolObject.name = "[ObjectPool] " + fireProjectilePrefab.name;
            firePoolObject.transform.parent = this.transform;

            fireProjectilePool = firePoolObject.AddComponent<ObjectPool>();
            fireProjectilePool.hidePooledObjectsInHierarchy = false;
            fireProjectilePool.Initialize(fireProjectilePrefab, 40, true);

            //For the fire projectiles
            GameObject stonePoolObject = new GameObject();
            stonePoolObject.name = "[ObjectPool] " + fireProjectilePrefab.name;
            stonePoolObject.transform.parent = this.transform;

            stoneProjectilePool = stonePoolObject.AddComponent<ObjectPool>();
            stoneProjectilePool.hidePooledObjectsInHierarchy = false;
            stoneProjectilePool.Initialize(stoneProjectilePrefab, 40, true);

        }

        /// <summary>
        /// Unloads all current spawned projectiles.
        /// </summary>
        public void Unload () {
            
            StopAllCoroutines();
            for (int i = 0; i < spawnedProjectiles.Count; i++) {

                spawnedProjectiles[i].Unload();

            }

        }

        /// <summary>
        /// Starts the projectile sequence and pre-bakes it.
        /// </summary>
        public void StartSpawning () {

            canSpawn = true;
			StartCoroutine(PreBakeProjectiles());
            StartSequence();

        }

        /// <summary>
        /// Begins a new projectile sequence.
        /// </summary>
        public void StartSequence () {

            currentSequence = projectileSequences[Random.Range(0,projectileSequences.Length)];
            amountToSpawn = currentSequence.projectileSequence.Length;
            delayTime = delayTimes[Random.Range(0, delayTimes.Length)];
            currentSpawnedProjectileIndex = 0;
            StartCoroutine(SpawnLoop());

        }

		IEnumerator PreBakeProjectiles () { 

			factorSpeed = 12;
			yield return new WaitForSeconds(2);
			factorSpeed = 1;

			for (int i = 0; i < spawnedProjectiles.Count; i++) {
				
				spawnedProjectiles[i].SpeedFactor = 1;

			}

			StopAllCoroutines();

			if (currentSpawnedProjectileIndex == amountToSpawn) {

				StartCoroutine(WaitForNextSequence());

			} else {

				StartCoroutine(SpawnLoop());

			}

		}

        IEnumerator SpawnLoop () {

			yield return new WaitForSeconds(1.45f / factorSpeed);

            if (canSpawn) {

                SpawnBall(currentSequence.projectileSequence[currentSpawnedProjectileIndex]);
                currentSpawnedProjectileIndex++;

                if (currentSpawnedProjectileIndex == amountToSpawn) {

                    StartCoroutine(WaitForNextSequence());

                } else {

                    StartCoroutine(SpawnLoop());

                }

            }

        }

        IEnumerator WaitForNextSequence () {

			yield return new WaitForSeconds(delayTime / factorSpeed);
            StartSequence();

        }

        public void StopSpawning () {

            Debug.Log("Spawner Stops with spawning projectiles.");
            canSpawn = false;
            StopAllCoroutines();

        }

        public void SpawnBall (ProjectileSequence.ProjectileType _projectileType) {

            GameObject newObject = null;

            switch (_projectileType) {

                case ProjectileSequence.ProjectileType.Fire:

                    newObject = fireProjectilePool.GetObjectFromPool();

                break;

                case ProjectileSequence.ProjectileType.Stone:

                    newObject = stoneProjectilePool.GetObjectFromPool();

                break;

            }

            newObject.transform.position = transform.position;

			ProjectileMovement projectile = newObject.GetComponent<ProjectileMovement>();
            spawnedProjectiles.Add(projectile);
            projectile.SpeedFactor = factorSpeed;
			projectile.StartMove();
			projectile.manager = this;

        }

		public void RemoveFromList (ProjectileMovement _movement) {
			
			spawnedProjectiles.Remove(_movement);

		}

        [System.Serializable]
        public class ProjectileSequence {

            public enum ProjectileType {

                Stone,
                Fire

            }

            public ProjectileType[] projectileSequence;

        }

    }

}