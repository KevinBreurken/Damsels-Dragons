using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Base.CustomEditors {

    /// <summary>
    /// Fancier Editor UI functions.
    /// </summary>
    public class Draw {

        /// <summary>
        /// Draws a Title 
        /// </summary>
		public static void TitleField (string text) {

            GUIStyle style = EditorStyles.toolbarButton;
            style.fontStyle = FontStyle.Normal;
            EditorGUILayout.LabelField(text,style);

		}

        /// <summary>
        /// Draws a title with a toggle next to it. Used for groups.
        /// </summary>
        public static bool TitleWithToggle (bool _state, string _text) {

            EditorGUILayout.BeginHorizontal();
            TitleField(_text);
            _state = EditorGUILayout.Toggle(_state, new GUILayoutOption[] { GUILayout.Width(20) });
            EditorGUILayout.EndHorizontal();

            return _state;

        }

        /// <summary>
        /// Draws a toggle button with text next to it.
        /// </summary>
        public static bool ToggleField (bool _state,string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _state = EditorGUILayout.Toggle(_state,GUILayout.Width(25));

            EditorGUILayout.EndHorizontal();
            return _state;

        }

        public static string DrawTextField(string _newText, string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _newText = EditorGUILayout.TextField(_newText);

            EditorGUILayout.EndHorizontal();

            return _newText;

        }

        public static GameObject DrawGameObjectField(GameObject _object, string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _object = (GameObject)EditorGUILayout.ObjectField(_object, typeof(GameObject),false);

            EditorGUILayout.EndHorizontal();
            return _object;

        }

	}

}

