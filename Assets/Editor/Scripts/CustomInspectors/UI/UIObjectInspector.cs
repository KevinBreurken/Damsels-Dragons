using UnityEngine;
using System.Collections;
using UnityEditor;
using Base.UI;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(UIObject))]
    public class UIObjectInspector : Editor {

        private UIObject myScript;

        public override void OnInspectorGUI () {

            myScript = (UIObject)target;

            Draw.TitleField("UI Object");
            myScript.showAnimationData = DrawAnimationPanel(myScript.showAnimationData, "Show Animation");
            EditorGUILayout.Space();
            myScript.hideAnimationData = DrawAnimationPanel(myScript.hideAnimationData, "Hide Animation");
        }

        private UIAnimationData DrawAnimationPanel (UIAnimationData _data,string _titleName) {

            UIAnimationData data = _data;

            EditorGUILayout.BeginVertical("Box");
            Draw.TitleField(_titleName);

            data.usesMoveAnimation = Draw.TitleWithToggle(data.usesMoveAnimation, "Movement");
            if (data.usesMoveAnimation) {
                EditorGUILayout.BeginVertical("Box");
                data.startPosition = Draw.DrawVector2Field(data.startPosition, "Start Position");
                data.endPosition = Draw.DrawVector2Field(data.endPosition, "End Position");
                data.moveAnimationTime = Draw.FloatField(data.moveAnimationTime, "Time");
                data.moveDelay = Draw.FloatField(data.moveAnimationTime, "Start Delay");
                data.moveEaseType = Draw.DrawEaseField(data.moveEaseType, "Easing Type");
                EditorGUILayout.EndVertical();
            }

            data.usesFadeAnimation = Draw.TitleWithToggle(data.usesFadeAnimation, "Fade");
            if (data.usesFadeAnimation) {
                EditorGUILayout.BeginVertical("Box");
                data.startFadeValue = Draw.FloatField(data.fadeAnimationTime, "Start Fade Value");
                data.endFadeValue = Draw.FloatField(data.endFadeValue, "End Fade Value");
                data.fadeAnimationTime = Draw.FloatField(data.fadeAnimationTime, "Time");
                data.fadeDelay = Draw.FloatField(data.moveAnimationTime, "Start Delay");
                data.fadeEaseType = Draw.DrawEaseField(data.moveEaseType, "Easing Type");
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();

            return data;
        }

    }

}