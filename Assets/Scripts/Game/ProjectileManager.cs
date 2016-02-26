using UnityEngine;
using System.Collections;
using Base.Management;
using System.Collections.Generic;

namespace Base.Game {

    public class ProjectileManager : MonoBehaviour {

		public GameObject projecilePrefab;
		private List<ProjectileMovement> spawnedTiles = new List<ProjectileMovement>();
        public float[] delayTimes;

		private ObjectPool objectPool;
        private int amountToSpawn;
        private float delayTime;
		private float factorSpeed;

        void Awake () {

            GameObject objectPoolObject = new GameObject();
            objectPoolObject.name = "[ObjectPool] " + projecilePrefab.name;
            objectPoolObject.transform.parent = this.transform;

            objectPool = objectPoolObject.AddComponent<ObjectPool>();
            objectPool.hidePooledObjectsInHierarchy = false;
            objectPool.Initialize(projecilePrefab, 20, true);

        }

        public void StartSpawning () {

			StartCoroutine(PreBakeProjectiles());
            StartSequence();

        }

        public void StartSequence () {

            amountToSpawn = Random.Range(2, 4);
            delayTime = delayTimes[Random.Range(0, delayTimes.Length)];
	
            StartCoroutine(SpawnLoop());

        }

		IEnumerator PreBakeProjectiles () { 

			factorSpeed = 4;
			yield return new WaitForSeconds(6);
			factorSpeed = 1;

			for (int i = 0; i < spawnedTiles.Count; i++) {
				
				spawnedTiles[i].SpeedFactor = 1;

			}

			StopAllCoroutines();

			if (amountToSpawn == 0) {

				StartCoroutine(WaitForNextSequence());

			} else {

				StartCoroutine(SpawnLoop());

			}

		}
        IEnumerator SpawnLoop () {

			yield return new WaitForSeconds(1.45f / factorSpeed);
            SpawnBall();
            amountToSpawn--;

            if (amountToSpawn == 0) {

                StartCoroutine(WaitForNextSequence());

            } else {

                StartCoroutine(SpawnLoop());

            }

        }

        IEnumerator WaitForNextSequence () {

			yield return new WaitForSeconds(delayTime / factorSpeed);
            StartSequence();

        }

        public void StopSpawning () {
			
            Debug.Log("Stop Spawning Projectiles");

        }

        public void SpawnBall () {

            GameObject newObject = objectPool.GetObjectFromPool();
            newObject.transform.position = transform.position;

			ProjectileMovement projectile = newObject.GetComponent<ProjectileMovement>();
			projectile.SpeedFactor = factorSpeed;
			spawnedTiles.Add(projectile);
			projectile.StartMove();
			projectile.manager = this;

        }

		public void RemoveFromList (ProjectileMovement _movement) {
			
			spawnedTiles.Remove(_movement);

		}
    }

}