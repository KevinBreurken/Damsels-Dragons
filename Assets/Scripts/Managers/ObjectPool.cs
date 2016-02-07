using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Audio;

namespace Base.Management {

    /// <summary>
    /// Creates multiple instances of a given GameObject and 
    /// allows them to be accessed by the GetObjectFromPool() function.
    /// </summary>
    public class ObjectPool : MonoBehaviour {

        /// <summary>
        /// The GameObject that will be pooled.
        /// </summary>
        public GameObject objectToPool;

        /// <summary>
        /// The list of available pool objects.
        /// </summary>
        public List<GameObject> objectPool = new List<GameObject>();

        /// <summary>
        /// If this ObjectPool will create new pool objects when its empty.
        /// </summary>
        public bool createsNewObjects;

        /// <summary>
        /// How many pool objects this ObjectPool has made in total.
        /// </summary>
        public int totalAmountOfObjectsCreated;

        /// <summary>
        /// If the pooled GameObjects are visible in the hierarchy.
        /// </summary>
        private bool hidePooledObjectsInHierarchy = true;

        /// <summary>
        /// Initializes the Object pool.
        /// </summary>
        /// <param name="_objectToPool">Which GameObject will be pooled.</param>
        /// <param name="_startingAmount">The amount of objects that will be created.</param>
        /// <param name="_createsNewObjects">If this ObjectPool will create new objects when its empty.</param>
        public void Initialize (GameObject _objectToPool,int _startingAmount,bool _createsNewObjects) {

            objectToPool = _objectToPool;
            createsNewObjects = _createsNewObjects;

            for (int i = 0; i < _startingAmount; i++) {

                CreateNewPooledObject();

            }

        }

        /// <summary>
        /// Picks a GameObject from the ObjectPool.
        /// </summary>
        /// <returns>The pooled GameObject.</returns>
        public GameObject GetObjectFromPool () {

            if(objectPool.Count == 0) {

                if (createsNewObjects) {

                    CreateNewPooledObject();

                } else {

                    Debug.LogWarning("The pool reached its limit. wont create new objects",this);
                    return null;

                }

            }

            GameObject pooledObject = objectPool[0];
            objectPool.Remove(pooledObject);


            pooledObject.SetActive(true);
            return pooledObject;

        }

        /// <summary>
        /// Creates a new pool GameObject.
        /// </summary>
        /// <returns>The new GameObject.</returns>
        private GameObject CreateNewPooledObject () {

            totalAmountOfObjectsCreated++;

            GameObject pooledObject = Object.Instantiate(objectToPool, objectToPool.transform.position, objectToPool.transform.rotation) as GameObject;
            pooledObject.name = "[PooledObject] " + objectToPool.name;
            pooledObject.hideFlags = hidePooledObjectsInHierarchy ? HideFlags.HideInHierarchy : HideFlags.None;
            pooledObject.SetActive(false);
            pooledObject.transform.parent = this.transform;
            objectPool.Add(pooledObject);

            ObjectPoolReturnReference reference = pooledObject.AddComponent<ObjectPoolReturnReference>();
            reference.poolReference = this;  

            return pooledObject;

        }

        /// <summary>
        /// Adds the GameObject back to the pool.
        /// </summary>
        /// <param name="_gameObject">The GameObject that will be put back into the ObjectPool.</param>
        public void AddToPool (GameObject _gameObject) {

            _gameObject.SetActive(false);
            objectPool.Add(_gameObject);

        }

    }

    /// <summary>
    /// Used for returning the GameObject to its designated ObjectPool.
    /// </summary>
    public class ObjectPoolReturnReference : MonoBehaviour {

        /// <summary>
        /// The pool this GameObject will return to.
        /// </summary>
        public ObjectPool poolReference;

        /// <summary>
        /// Returns this GameObject back to its ObjectPool.
        /// </summary>
        public void ReturnToPool () {

            poolReference.AddToPool(this.gameObject);

        }

    }

}