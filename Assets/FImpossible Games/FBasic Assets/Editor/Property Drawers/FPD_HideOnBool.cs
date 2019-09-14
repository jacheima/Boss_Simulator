#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

namespace FIMSpace.FEditor
{
    [CustomPropertyDrawer(typeof(FPD_HideOnBoolAttribute))]
    public class FPropDrawers_HideOnBool : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent content)
        {
            FPD_HideOnBoolAttribute condHAtt = (FPD_HideOnBoolAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;

            if (!condHAtt.HideInInspector || enabled)
            {
                EditorGUI.PropertyField(rect, property, content, true);
            }

            GUI.enabled = wasEnabled;
        }

        private bool GetConditionalHideAttributeResult(FPD_HideOnBoolAttribute condHAtt, SerializedProperty property)
        {
            bool enabled = true;
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, condHAtt.BoolVarName);
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            if (sourcePropertyValue != null)
            {
                enabled = sourcePropertyValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.BoolVarName);
            }

            return enabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            FPD_HideOnBoolAttribute condHAtt = (FPD_HideOnBoolAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            if (!condHAtt.HideInInspector || enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }
    }
}


#endif
