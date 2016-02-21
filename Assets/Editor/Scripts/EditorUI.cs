using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Base.Audio;
using Base.Management;
using System.Collections.Generic;
using DG.Tweening;
using Base.UI;

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

        public static Vector2 DrawVector2Field(Vector2 _vector2,string _text) {

            EditorGUILayout.BeginHorizontal();

            _vector2 = EditorGUILayout.Vector2Field(_text, _vector2);

            EditorGUILayout.EndHorizontal();

            return _vector2;
        }

        public static Vector3 DrawVector3Field (Vector3 _vector3, string _text) {

            EditorGUILayout.BeginHorizontal();

            _vector3 = EditorGUILayout.Vector3Field(_text, _vector3);

            EditorGUILayout.EndHorizontal();

            return _vector3;
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

    public class DrawUI {

        public static UIAnimationData DrawAnimationDataPanel (UIAnimationData _data, string _titleName) {

            UIAnimationData data = _data;

            EditorGUILayout.BeginVertical("Box");
            Draw.TitleField(_titleName);

            Draw.TitleField("Overall");
            EditorGUILayout.BeginVertical("Box");
            data.delay = Draw.FloatField(data.delay, "Start Delay");
            EditorGUILayout.EndVertical();

            data.usesSoundEffect = Draw.TitleWithToggle(data.usesSoundEffect, "Sound Effect");
            if (data.usesSoundEffect) {
                EditorGUILayout.BeginVertical("Box");
                data.soundEffect.objectPrefab = Draw.DrawGameObjectField(data.soundEffect.objectPrefab, "Sound Effect Prefab", false);
                data.soundEffectDelay = Draw.FloatField(data.soundEffectDelay, "Start Delay");
                EditorGUILayout.EndVertical();
            }

            data.usesMoveAnimation = Draw.TitleWithToggle(data.usesMoveAnimation, "Movement");
            if (data.usesMoveAnimation) {
                EditorGUILayout.BeginVertical("Box");
                data.useStartPosition = Draw.ToggleField(data.useStartPosition, "Use Start Position");
                if(data.useStartPosition)
                    data.startPosition = Draw.DrawVector2Field(data.startPosition, "Start Position");
                data.endPosition = Draw.DrawVector2Field(data.endPosition, "End Position");
                data.moveAnimationTime = Draw.FloatField(data.moveAnimationTime, "Time");
                data.moveDelay = Draw.FloatField(data.moveDelay, "Start Delay");
                data.moveEaseType = Draw.DrawEaseField(data.moveEaseType, "Easing Type");
                EditorGUILayout.EndVertical();
            }

            data.usesFadeAnimation = Draw.TitleWithToggle(data.usesFadeAnimation, "Fade");
            if (data.usesFadeAnimation) {
                EditorGUILayout.BeginVertical("Box");
                data.useStartFadeValue = Draw.ToggleField(data.useStartFadeValue, "Use Start Fade Value");
                if (data.useStartFadeValue)
                    data.startFadeValue = Draw.FloatField(data.startFadeValue, "Start Fade Value");
                data.endFadeValue = Draw.FloatField(data.endFadeValue, "End Fade Value");
                data.fadeAnimationTime = Draw.FloatField(data.fadeAnimationTime, "Time");
                data.fadeDelay = Draw.FloatField(data.fadeDelay, "Start Delay");
                data.fadeEaseType = Draw.DrawEaseField(data.moveEaseType, "Easing Type");
                EditorGUILayout.EndVertical();
            }

            data.usesRotationAnimation = Draw.TitleWithToggle(data.usesRotationAnimation, "Rotation");
            if (data.usesRotationAnimation) {
                EditorGUILayout.BeginVertical("Box");
                data.useStartRotation = Draw.ToggleField(data.useStartRotation, "Use Start Rotation");
                if (data.useStartRotation)
                    data.startRotation = Draw.DrawVector3Field(data.startRotation, "Start Rotation Value");
                data.endRotation = Draw.DrawVector3Field(data.endRotation, "End Rotation Value");
                data.rotationAnimationTime = Draw.FloatField(data.rotationAnimationTime, "Time");
                data.rotationDelay = Draw.FloatField(data.rotationDelay, "Start Delay");
                data.rotationEaseType = Draw.DrawEaseField(data.rotationEaseType, "Easing Type");
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();

            return data;
        }

    }

}

