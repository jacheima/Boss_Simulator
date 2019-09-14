using UnityEngine;
using UnityEditor;

namespace FIMSpace.FEditor
{
    [CustomPropertyDrawer(typeof(FPD_VerticalLimitCircleAttribute))]
    public class FPropDrawers_VerticalLimitCircle : PropertyDrawer
    {
        int adjustSymmetrical = 45;
        int preAdjust = 45;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent content)
        {
            FPD_VerticalLimitCircleAttribute limit = attribute as FPD_VerticalLimitCircleAttribute;

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                float minValue = property.vector2Value.x;
                float maxValue = property.vector2Value.y;

                float minRange = limit.MinValue;
                float maxRange = limit.MaxValue;

                if (limit.drawHR) FEditor_Styles.DrawUILine(new Color(0.55f, 0.55f, 0.55f, 0.7f));

                GUILayout.BeginHorizontal();
                GUILayout.Label(content, GUILayout.MaxWidth(170f));
                GUILayout.FlexibleSpace();
                GUILayout.Label(Mathf.Round(minValue) + "°", FEditor_Styles.GrayBackground, GUILayout.MaxWidth(40f));
                FEditor_CustomInspectorHelpers.DrawMinMaxVertSphere(-maxValue, -minValue, 14);
                GUILayout.Label(Mathf.Round(maxValue) + "°", FEditor_Styles.GrayBackground, GUILayout.MaxWidth(40f));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minRange, maxRange);

                if (limit.symSlider)
                {
                    adjustSymmetrical = (int)EditorGUILayout.Slider("Adjust symmetrical", adjustSymmetrical, 0f, maxRange);

                    if (preAdjust != adjustSymmetrical)
                    {
                        minValue = -adjustSymmetrical;
                        maxValue = adjustSymmetrical;
                        preAdjust = adjustSymmetrical;
                    }
                }

                if (limit.drawHR) FEditor_Styles.DrawUILine(new Color(0.55f, 0.55f, 0.55f, 0.7f));
                GUILayout.Space(5f);

                property.vector2Value = new Vector2(minValue, maxValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float size = 1f;
            return size;
        }
    }



    [CustomPropertyDrawer(typeof(FPD_HorizontalLimitCircleAttribute))]
    public class FPropDrawers_HorizontalLimitCircle : PropertyDrawer
    {
        int adjustSymmetrical = 45;
        int preAdjust = 45;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent content)
        {
            FPD_HorizontalLimitCircleAttribute limit = attribute as FPD_HorizontalLimitCircleAttribute;

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                float minValue = property.vector2Value.x;
                float maxValue = property.vector2Value.y;

                float minRange = limit.MinValue;
                float maxRange = limit.MaxValue;

                FEditor_Styles.DrawUILine(new Color(0.55f, 0.55f, 0.55f, 0.7f));

                GUILayout.BeginHorizontal();
                GUILayout.Label(content, GUILayout.MaxWidth(170f));
                GUILayout.FlexibleSpace();
                GUILayout.Label(Mathf.Round(minValue) + "°", FEditor_Styles.GrayBackground, GUILayout.MaxWidth(40f));
                FEditor_CustomInspectorHelpers.DrawMinMaxSphere(-maxValue, -minValue, 14);
                GUILayout.Label(Mathf.Round(maxValue) + "°", FEditor_Styles.GrayBackground, GUILayout.MaxWidth(40f));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minRange, maxRange);

                if (limit.symSlider)
                {
                    adjustSymmetrical = (int)EditorGUILayout.Slider("Adjust symmetrical", adjustSymmetrical, 0f, maxRange);

                    if (preAdjust != adjustSymmetrical)
                    {
                        minValue = -adjustSymmetrical;
                        maxValue = adjustSymmetrical;
                        preAdjust = adjustSymmetrical;
                    }
                }

                FEditor_Styles.DrawUILine(new Color(0.55f, 0.55f, 0.55f, 0.7f));
                GUILayout.Space(5f);

                property.vector2Value = new Vector2(minValue, maxValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float size = 1f;
            return size;
        }
    }

}

