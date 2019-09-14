using UnityEditor;
using UnityEngine;

namespace FIMSpace.FEditor
{
    [CustomPropertyDrawer(typeof(FPD_FoldFrameAttribute))]
    public class FPD_FoldFrame : PropertyDrawer
    {
        FPD_FoldFrameAttribute Attribute { get { return ((FPD_FoldFrameAttribute)base.attribute); } }

        //private bool folded = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty[] props = new SerializedProperty[Attribute.VariablesToStore.Length];

            for (int i = 0; i < props.Length; i++)
            {
                props[i] = property.serializedObject.FindProperty(Attribute.VariablesToStore[i]);
            }

            GUILayout.BeginVertical(FEditor_Styles.Style(new Color32(250, 250, 250, 75)));
            EditorGUI.indentLevel++;

            GUIStyle foldBold = EditorStyles.foldout;
            foldBold.fontStyle = FontStyle.Bold;
            Attribute.Folded = EditorGUILayout.Foldout(Attribute.Folded, " " + Attribute.FrameTitle, true, foldBold);

            if (Attribute.Folded)
            {
                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i] != null)
                        EditorGUILayout.PropertyField(props[i]);
                    else
                        EditorGUILayout.LabelField("Wrong property name?");
                }
            }

            EditorGUI.indentLevel--;
            GUILayout.EndVertical();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float size = -EditorGUIUtility.singleLineHeight / 5f;
            return size;
        }
    }

}

