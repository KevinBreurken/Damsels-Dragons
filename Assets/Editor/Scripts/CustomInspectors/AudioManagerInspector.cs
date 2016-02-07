using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.Audio;
using Base.CustomEditors;
using System.Collections.Generic;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerInspector : Editor {

        private AudioManager myScript;

        public override void OnInspectorGUI () {
            
            myScript = (AudioManager)target;

            Draw.TitleField("Audio Clip");
            EditorGUILayout.HelpBox("Audio Objects are stored in the [Files / Prefabs / Audio] folder. \n" +
                "The identifier is the name that used to create that Audio Object. \n \n The conventions for identifier names are: \n [GAME_LOCATION / GAME_ELEMENT / NAME / EXTRA] " +
                 " \n (example: Menu_Credits_Button_Hover)", MessageType.Info);
            
            myScript.hideAudioInHierarchy = Draw.ToggleField(myScript.hideAudioInHierarchy, "Objects in Hierarchy");

            if (GUILayout.Button("Add New")) {

                myScript.audioList.Add(new AudioListData());

            }

            for (int i = 0; i < myScript.audioList.Count; i++) {

                DrawItem(myScript.audioList[i]);

            }

            EditorGUILayout.Space();

            DrawVolumePanel();

        }

        private void DrawVolumePanel () {

            Draw.TitleField("Audio Volume");
            EditorGUILayout.HelpBox("Audio Volumes are handled by the Audio Mixer itself." +
                " \nLocated at : [Assets/Files/Audio]", MessageType.Info);

        }


        private void DrawItem(AudioListData _data) {

            EditorGUILayout.BeginHorizontal("Box");

            //----Item Properties---------------------------------------------------------->
            EditorGUILayout.BeginVertical();
            _data.audioObject = Draw.DrawGameObjectField(_data.audioObject, "Audio Prefab");
            if(_data.audioObject != null) {
                EditorGUILayout.LabelField("Identifier: " + _data.audioObject.name);
            }
            EditorGUILayout.EndVertical();
            //----------------------------------------------------------------------------->

            //----Remove / Up / Down Buttons-------------------------------------------------->
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("X", GUILayout.Width(100))) {

                myScript.audioList.Remove(_data);

            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("^", GUILayout.Width(50))) {
                int index = myScript.audioList.IndexOf(_data);
                if (index != 0) {
                    SwapItems(myScript.audioList, index, index - 1);
                }
            }

            if (GUILayout.Button("V", GUILayout.Width(50))) {
                int index = myScript.audioList.IndexOf(_data);
                if(index != myScript.audioList.Count - 1) {
                    SwapItems(myScript.audioList, index, index + 1);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            //------------------------------------------------------------------------------>
           
            EditorGUILayout.EndHorizontal();

        }

        /// <summary>
        /// Swaps items from the list with each other.
        /// </summary>
        public void SwapItems (List<AudioListData> list, int indexA, int indexB) {

            AudioListData tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;

        }

    }

}