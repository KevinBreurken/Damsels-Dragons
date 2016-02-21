using UnityEngine;
using System.Collections;

namespace Base.Audio {

    /// <summary>
    /// A holder that contains the prefab and a reference
    /// to the AudioObject.
    /// </summary>
    [System.Serializable]
    public struct AudioObjectHolder {
        /// <summary>
        /// The prefab that holds the
        /// </summary>
        public GameObject objectPrefab;

        /// <summary>
        /// The audioObject of the instantiated prefab.
        /// </summary>
        [HideInInspector]
        public AudioObject audioObject;

    }

}