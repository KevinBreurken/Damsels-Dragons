using UnityEngine;
using System.Collections;
using Base.Management;

namespace Base.Game {

    public class ProjectileManager : MonoBehaviour {

        public GameObject projecilePrefab;
        private ObjectPool objectPool;

        public float[] delayTimes;

        private int amountToSpawn;
        private float delayTime;
        // Use this for initialization
        void Awake () {

            GameObject objectPoolObject = new GameObject();
            objectPoolObject.name = "[ObjectPool] " + projecilePrefab.name;
            objectPoolObject.transform.parent = this.transform;

            objectPool = objectPoolObject.AddComponent<ObjectPool>();
            objectPool.hidePooledObjectsInHierarchy = false;
            objectPool.Initialize(projecilePrefab, 20, true);

        }

        public void StartSpawning () {

            Debug.Log("Start Spawning Projectiles");
            StartSequence();

        }

        public void StartSequence () {

            amountToSpawn = Random.Range(2, 4);
            delayTime = delayTimes[Random.Range(0, delayTimes.Length)];
            StartCoroutine(SpawnLoop());

        }

        IEnumerator SpawnLoop () {

            yield return new WaitForSeconds(1);
            SpawnBall();
            amountToSpawn--;

            if (amountToSpawn == 0) {

                StartCoroutine(WaitForNextSequence());

            } else {

                StartCoroutine(SpawnLoop());

            }

        }

        IEnumerator WaitForNextSequence () {

            yield return new WaitForSeconds(delayTime);
            StartSequence();

        }

        public void StopSpawning () {
            Debug.Log("Stop Spawning Projectiles");
        }

        public void SpawnBall () {

            GameObject newObject = objectPool.GetObjectFromPool();
            newObject.transform.position = transform.position;
            newObject.GetComponent<ProjectileMovement>().StartMove();

        }

    }

}