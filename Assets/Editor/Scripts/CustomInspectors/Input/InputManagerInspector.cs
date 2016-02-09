using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.Control;
using Base.Control.Method;
using Base.Management;
using Base.CustomEditors;
using System.Collections.Generic;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(InputManager))]
    public class InputManagerInspector : Editor {

        private InputManager myScript;

        public override void OnInspectorGUI () {

            myScript = (InputManager)target;

            Draw.TitleField("Input Manager");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Start Input Method");
            myScript.startInputMethod = (BaseInputMethod)EditorGUILayout.ObjectField(myScript.startInputMethod, typeof(BaseInputMethod), true);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add New")) {

                myScript.inputMethods.Add(new ListData());

            }

            for (int i = 0; i < myScript.inputMethods.Count; i++) {

                DrawItem(myScript.inputMethods[i]);

            }

        }

        private void DrawItem (ListData _data) {

            EditorGUILayout.BeginHorizontal("Box");

            //----Item Properties---------------------------------------------------------->
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Input Method");
            BaseInputMethod method = (BaseInputMethod)EditorGUILayout.ObjectField(_data.listedObject.GetComponent<BaseInputMethod>(), typeof(BaseInputMethod), true);
            _data.listedObject = method.gameObject;
            EditorGUILayout.EndHorizontal();

            if (_data.listedObject != null) {
                EditorGUILayout.LabelField("Identifier: " + _data.listedObject.name);
            }
            EditorGUILayout.EndVertical();
            //----------------------------------------------------------------------------->

            //----Remove / Up / Down Buttons-------------------------------------------------->
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("X", GUILayout.Width(100))) {

                myScript.inputMethods.Remove(_data);

            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("^", GUILayout.Width(50))) {
                int index = myScript.inputMethods.IndexOf(_data);
                if (index != 0) {
                    Functions.SwapItems(myScript.inputMethods, index, index - 1);
                }
            }

            if (GUILayout.Button("V", GUILayout.Width(50))) {
                int index = myScript.inputMethods.IndexOf(_data);
                if (index != myScript.inputMethods.Count - 1) {
                    Functions.SwapItems(myScript.inputMethods, index, index + 1);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            //------------------------------------------------------------------------------>

            EditorGUILayout.EndHorizontal();

        }



    }

}