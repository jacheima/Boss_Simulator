using UnityEngine;
using UnityEditor;

namespace FIMSpace.FEditor
{
    [CustomPropertyDrawer(typeof(FPD_MinMaxSliderAttribute))]
    public class FPropDrawers_MinMaxSlider : PropertyDrawer
    {
        int adjustSwitcherValue = 60;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent content)
        {
            FPD_MinMaxSliderAttribute minMax = attribute as FPD_MinMaxSliderAttribute;

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                float minValue = property.vector2Value.x;
                float maxValue = property.vector2Value.y;

                float minRange = minMax.MinValue;
                float maxRange = minMax.MaxValue;

                EditorGUI.MinMaxSlider(rect, content, ref minValue, ref maxValue, minRange, maxRange);
                rect.y += EditorGUIUtility.singleLineHeight;

                Vector2 vec = new Vector2();
                vec.x = minValue;
                vec.y = maxValue;

                property.vector2Value = vec;

                float preAdjust = adjustSwitcherValue;

                adjustSwitcherValue = EditorGUI.IntField(rect, "Adjust Both: ", adjustSwitcherValue);
                if (adjustSwitcherValue < 1) adjustSwitcherValue = 1;
                if (adjustSwitcherValue > minMax.MaxValue) adjustSwitcherValue = (int)minMax.MaxValue;

                Vector2 val = new Vector2(minValue, maxValue);
                Vector2 preVal = val;

                rect.y += EditorGUIUtility.singleLineHeight;
                val = EditorGUI.Vector2Field(rect, "Range: ", val);

                if (adjustSwitcherValue != preAdjust)
                    property.vector2Value = new Vector2(-adjustSwitcherValue, adjustSwitcherValue);

                if (val != preVal)
                    property.vector2Value = new Vector2(val.x, val.y);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float size = EditorGUIUtility.singleLineHeight;
            size += EditorGUIUtility.singleLineHeight * 3;

            return size;
        }
    }
}


[CustomPropertyDrawer(type: typeof(BackgroundColorAttribute))]
public class BackgroundColorDecorator : DecoratorDrawer
{
    BackgroundColorAttribute Attribute { get { return (BackgroundColorAttribute)base.attribute; } }
    public override float GetHeight() { return 0; }

    public override void OnGUI(Rect position)
    {
        GUI.backgroundColor = Attribute.Color;
    }
}
