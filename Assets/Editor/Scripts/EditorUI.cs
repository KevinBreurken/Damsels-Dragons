using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Base.Audio;
using Base.Management;
using System.Collections.Generic;
using DG.Tweening;

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

        /// <summary>
        /// Draws a float field with text next to it.
        /// </summary>
        public static float FloatField (float _value, string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _value = EditorGUILayout.FloatField(_value);

            EditorGUILayout.EndHorizontal();
            return _value;

        }

        public static Ease DrawEaseField (Ease _ease, string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _ease = (Ease)EditorGUILayout.EnumPopup(_ease);

            EditorGUILayout.EndHorizontal();

            return _ease;
        }

        public static string DrawTextField(string _newText, string _text) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
            _newText = EditorGUILayout.TextField(_newText);

            EditorGUILayout.EndHorizontal();

            return _newText;

        }

        public static Vector3 DrawVector2Field(Vector2 _vector2,string _text) {

            EditorGUILayout.BeginHorizontal();

            _vector2 = EditorGUILayout.Vector2Field(_text, _vector2);

            EditorGUILayout.EndHorizontal();

            return _vector2;
        }

		public static GameObject DrawGameObjectField(GameObject _object, string _text,bool _allowSceneObjects) {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_text);
			_object = (GameObject)EditorGUILayout.ObjectField(_object, typeof(GameObject),_allowSceneObjects);

            EditorGUILayout.EndHorizontal();
            return _object;

        }

	}

	public class Functions{
		
		/// <summary>
		/// Swaps items from the list with each other.
		/// </summary>
		public static void SwapItems (List<ListData> list, int indexA, int indexB) {

			ListData tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;

		}

	}

    public class UIFunctions {

    }

}

