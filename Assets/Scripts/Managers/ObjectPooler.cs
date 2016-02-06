using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base.Managment {

    public class ObjectPooler : MonoBehaviour {

        public GameObject objectToPool;
        private List<GameObject> objectPool;
        [SerializeField]
        private int startingAmount;

        
        void Start () {

            for (int i = 0; i < startingAmount; i++) {

                CreateNewPooledObject();
                
            }

        }

        public GameObject GetObjectFromPool () {

            GameObject pooledObject = objectPool[0];
            if (pooledObject == null) {

                return CreateNewPooledObject();

            }

            Debug.LogError("THIS SHOULD NEVER HAPPEN.");
            return null;
        }

        private GameObject CreateNewPooledObject () {

            GameObject pooledObject = Instantiate(objectToPool, objectToPool.transform.position, objectToPool.transform.rotation) as GameObject;
            pooledObject.SetActive(false);
            pooledObject.transform.parent = this.transform;

            return pooledObject;
        }

        private void AddToPool (GameObject _gameObject) {

            objectPool.Add(_gameObject);

        }

    }

}