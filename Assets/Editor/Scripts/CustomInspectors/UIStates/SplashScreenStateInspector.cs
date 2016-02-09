using UnityEngine;
using UnityEditor;
using System.Collections;
using Base.UI;
using Base.UI.State;
using Base.Management;
using Base.CustomEditors;

namespace Base.CustomEditors.Inspectors {

    [CustomEditor(typeof(SplashScreenState))]
    public class SplashScreenStateInspector : Editor {

        private SplashScreenState myScript;

        public override void OnInspectorGUI () {

            myScript = (SplashScreenState)target;

            Draw.TitleField("Splash Screen State");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Next UI State: ");
            myScript.nextUIState = (BaseUIState)EditorGUILayout.ObjectField(myScript.nextUIState, typeof(BaseUIState),true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            myScript.fadeInTime = Draw.FloatField(myScript.fadeInTime, "Fade-in Time");
            myScript.fadeOutTime = Draw.FloatField(myScript.fadeOutTime, "Fade-out Time");
            myScript.timeTillFadeOutTime = Draw.FloatField(myScript.timeTillFadeOutTime, "Time until screen fades out.");
            myScript.timeTilleStateSwitch = Draw.FloatField(myScript.timeTilleStateSwitch, "Time until screen state is switched.");
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Total SplashScreen time: " + (myScript.timeTilleStateSwitch + myScript.timeTillFadeOutTime +
                myScript.fadeOutTime + myScript.fadeInTime) + " Seconds total.");
        }

      

    }



}