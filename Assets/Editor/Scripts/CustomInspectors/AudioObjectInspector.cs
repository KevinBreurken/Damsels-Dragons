using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.Audio;
using Base.CustomEditors;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(AudioObject))]
    public class AudioObjectInspector : Editor {

        public override void OnInspectorGUI () {

            AudioObject myScript = (AudioObject)target;

            DrawPitchPanel(myScript);

            if (GUILayout.Button("Preview Sound")) {

                if (Application.isPlaying) {
                
                    GameObject tempObject = Instantiate(myScript.gameObject) as GameObject;
                    Destroy(tempObject, 5);
                    tempObject.GetComponent<AudioObject>().Play();

                } else {

                    Debug.LogWarning("Previewing audio is only possible in Play Mode.");

                }
            }

        }

        private void DrawPitchPanel (AudioObject _myScript) {

            _myScript.randomPitch = Draw.TitleWithToggle(_myScript.randomPitch, "Pitch");

            if (_myScript.randomPitch == true) {

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("" + _myScript.randomPitchRange.x, GUILayout.Width(80));
                Draw.TitleField("Random Pitch");
                EditorGUILayout.LabelField("" + _myScript.randomPitchRange.y, GUILayout.Width(80));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.MinMaxSlider(ref _myScript.randomPitchRange.x, ref _myScript.randomPitchRange.y, -1.0f, 1.0f);

                if(GUILayout.Button("Set to 0")) {

                    _myScript.randomPitchRange = new Vector2(0, 0);

                }

            }

        }

    }

}