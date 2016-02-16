using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.Management;
using Base.CustomEditors;
using Base.Game;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(GameStateSelector))]
    public class GameStateSelectorInspector : Editor {

        private GameStateSelector myScript;

        public override void OnInspectorGUI () {

            myScript = (GameStateSelector)target;

            Draw.TitleField("Game States");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Start State");
            myScript.startGameState = (BaseGameState)EditorGUILayout.ObjectField(myScript.startGameState, typeof(BaseGameState), true);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Add New")) {

                myScript.States.Add(new ListData());

            }

            for (int i = 0; i < myScript.States.Count; i++) {

                DrawItem(myScript.States[i]);

            }

        }

        private void DrawItem (ListData _data) {

            EditorGUILayout.BeginHorizontal("Box");

            //----Item Properties---------------------------------------------------------->
            EditorGUILayout.BeginVertical();
            _data.listedObject = Draw.DrawGameObjectField(_data.listedObject, "UI State Object", true);
            if (_data.listedObject != null) {

                string newstring = _data.listedObject.GetComponent<BaseGameState>().GetType().ToString().Remove(0, 14);
                EditorGUILayout.LabelField("Identifier: " + newstring);

            }
            EditorGUILayout.EndVertical();
            //----------------------------------------------------------------------------->

            //----Remove / Up / Down Buttons-------------------------------------------------->
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("X", GUILayout.Width(100))) {

                myScript.States.Remove(_data);

            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("^", GUILayout.Width(50))) {
                int index = myScript.States.IndexOf(_data);
                if (index != 0) {
                    Functions.SwapItems(myScript.States, index, index - 1);
                }
            }

            if (GUILayout.Button("V", GUILayout.Width(50))) {
                int index = myScript.States.IndexOf(_data);
                if (index != myScript.States.Count - 1) {
                    Functions.SwapItems(myScript.States, index, index + 1);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            //------------------------------------------------------------------------------>

            EditorGUILayout.EndHorizontal();

        }

    }



}