using UnityEngine;
using System.Collections;
using UnityEditor;
using Base.UI;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(UIButton),false)]
    public class UIButtonInspector : Editor {

        private UIButton myScript;
        
        public override void OnInspectorGUI () {

            myScript = (UIButton)target;

            Draw.TitleField("UI Button");
            myScript.showAnimationData = DrawUI.DrawAnimationDataPanel(myScript.showAnimationData, "Show Animation");
            EditorGUILayout.Space();
            myScript.hideAnimationData = DrawUI.DrawAnimationDataPanel(myScript.hideAnimationData, "Hide Animation");
            EditorGUILayout.Space();
            myScript.enterAnimationData = DrawUI.DrawAnimationDataPanel(myScript.enterAnimationData, "Enter Animation");
            EditorGUILayout.Space();
            myScript.exitAnimationData = DrawUI.DrawAnimationDataPanel(myScript.exitAnimationData, "Exit Animation");
        }

        
    }

}