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
            myScript.showAnimationData = DrawUI.DrawAnimationDataPanel(myScript.showAnimationData, "Show Animation");
            EditorGUILayout.Space();
            myScript.hideAnimationData = DrawUI.DrawAnimationDataPanel(myScript.hideAnimationData, "Hide Animation");

        }

       

    }

}