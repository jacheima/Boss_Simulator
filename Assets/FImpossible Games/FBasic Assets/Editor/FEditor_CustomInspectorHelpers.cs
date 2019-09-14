using UnityEditor;
using UnityEngine;

namespace FIMSpace.FEditor
{
    public static class FEditor_CustomInspectorHelpers
    {

        public static void DrawMinMaxSphere(float from, float to, float radius, float ghostRange = 0f)
        {
            Vector2 center = GUILayoutUtility.GetRect(radius * 3, radius * 2).center;

            Vector2 discCenter = center;

            DrawDisc(discCenter, radius, GetColorValue(0.65f));

            DrawDisc(discCenter, radius * 0.7f, GetColorValue(0.55f));

            float addStart = 0f;
            float addFrom = 0f;

            //Ghost Sphere
            float ghostTo = to + ghostRange;
            float ghostFrom = from - ghostRange;

            if (ghostFrom > 0f) addStart = -ghostFrom;
            if (ghostTo < 0f) addFrom = ghostTo;

            if (ghostTo > 0f)
            {
                if (addStart < 0f)
                    DrawArc(discCenter, 270f + addStart, ghostTo + addStart, radius * 0.96f, GetColorValue(0.44f));
                else
                    DrawArc(discCenter, 270f, ghostTo, radius * 0.96f, GetColorValue(0.44f));
            }

            if (ghostFrom < 0f)
            {
                if (addFrom < 0f)
                    DrawArc(discCenter, 270f - addFrom, ghostFrom - addFrom, radius * 0.96f, GetColorValue(0.44f));
                else
                    DrawArc(discCenter, 270f, ghostFrom, radius * 0.96f, GetColorValue(0.44f));
            }


            if (from > 0f) addStart = -from;
            if (to < 0f) addFrom = to;

            // Base Sphere
            if (to > 0f)
            {
                if (addStart < 0f)
                    DrawArc(discCenter, 270f + addStart, to + addStart, radius * 0.85f, GetColorValue(0.95f));
                else
                    DrawArc(discCenter, 270f, to, radius * 0.85f, GetColorValue(0.95f));
            }

            if (from < 0f)
            {
                if (addFrom < 0f)
                    DrawArc(discCenter, 270f - addFrom, from - addFrom, radius * 0.85f, GetColorValue(0.95f));
                else
                    DrawArc(discCenter, 270f, from, radius * 0.85f, GetColorValue(0.95f));
            }


        }

        public static void DrawMinMaxVertSphere(float from, float to, float radius, float ghostRange = 0f)
        {
            Vector2 center = GUILayoutUtility.GetRect(radius * 3, radius * 2).center;

            Vector2 discCenter = center;

            DrawArc(discCenter, -90f, 180f, radius, GetColorValue(0.65f));
            DrawArc(discCenter, -90f, 180f, radius * 0.7f, GetColorValue(0.55f));

            float addStart = 0f;
            float addFrom = 0f;


            //Ghost Sphere
            float ghostTo = to + ghostRange;
            float ghostFrom = from - ghostRange;

            if (ghostFrom > 0f) addStart = -ghostFrom;
            if (ghostTo < 0f) addFrom = ghostTo;

            if (ghostTo > 0f)
            {
                if (addStart < 0f)
                    DrawArc(discCenter, 180f + addStart, ghostTo + addStart, radius * 0.97f, GetColorValue(0.44f));
                else
                    DrawArc(discCenter, 180f, ghostTo, radius * 0.97f, GetColorValue(0.44f));
            }

            if (ghostFrom < 0f)
            {
                if (addFrom < 0f)
                    DrawArc(discCenter, 180f - addFrom, ghostFrom - addFrom, radius * 0.97f, GetColorValue(0.44f));
                else
                    DrawArc(discCenter, 180f, ghostFrom, radius * 0.97f, GetColorValue(0.44f));
            }


            if (from > 0f) addStart = -from;
            if (to < 0f) addFrom = to;

            if (to > 0f)
            {
                if (addStart < 0f)
                    DrawArc(discCenter, 180f + addStart, to + addStart, radius * 0.85f, GetColorValue(0.95f));
                else
                    DrawArc(discCenter, 180f, to, radius * 0.85f, GetColorValue(0.95f));
            }

            if (from < 0f)
            {
                if (addFrom < 0f)
                    DrawArc(discCenter, 180f - addFrom, from - addFrom, radius * 0.85f, GetColorValue(0.95f));
                else
                    DrawArc(discCenter, 180f, from, radius * 0.85f, GetColorValue(0.95f));
            }


        }

        private static Color GetColorValue(float val, float alpha = 1f)
        {
            return new Color(val, val, val, alpha);
        }

        public static void DrawDisc(Vector2 center, float radius, Color fill)
        {
            Handles.color = fill;
            Handles.DrawSolidDisc(center, Vector3.forward, radius);
        }

        public static void DrawArc(Vector2 center, float startAngle, float to, float radius, Color fill)
        {
            Vector2 start = new Vector2
            {
                x = -Mathf.Cos(Mathf.Deg2Rad * startAngle),
                y = Mathf.Sin(Mathf.Deg2Rad * startAngle)
            };

            Handles.color = fill;
            Handles.DrawSolidArc(center, Vector3.forward, start, to, radius);
        }
    }
}
