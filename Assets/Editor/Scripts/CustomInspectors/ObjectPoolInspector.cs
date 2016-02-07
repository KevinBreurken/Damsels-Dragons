using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.Management;
using Base.CustomEditors;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(ObjectPool))]
    public class ObjectPoolInspector : Editor {

        private ObjectPool myScript;

        public override void OnInspectorGUI () {

            myScript = (ObjectPool)target;

            Draw.TitleField("Object Pool");

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.LabelField("[" + myScript.objectToPool.name + "]" + "    (" + myScript.objectPool.Count + "/" + myScript.totalAmountOfObjectsCreated + ")");

            if (myScript.createsNewObjects) {

                EditorGUILayout.LabelField("This pool creates NEW objects when its empty");

            } else {

                EditorGUILayout.LabelField("This pool creates NO objects when its empty");

            }

            EditorGUILayout.EndHorizontal();

        }


    }




}