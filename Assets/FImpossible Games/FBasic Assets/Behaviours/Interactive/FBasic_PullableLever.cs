using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Example of using FBasic_Pullable to create 3D lever
    /// </summary>
    public class FBasic_PullableLever : FBasic_Pullable
    {
        /// <summary> Dragged value 0f-1f </summary>
        public float LeverValueY { get; private set; }

        /// <summary> Dragged value 0f-1f </summary>
        public float LeverValueX { get; private set; }

        public Vector2 RotationRangesY = new Vector2(80f, -6f);
        public Vector2 RotationRangesX = new Vector2(-40f, 40f);

        private Transform LeverTransform;

        protected override void Start()
        {
            base.Start();
            LeverTransform = transform.Find("Lever");

            LeverValueY = YValue / 100f;
            LeverValueX = XValue / 100f;

            UpdatePullableOrientation();
        }

        protected override void UpdatePullableOrientation()
        {
            Vector3 targetDir = Vector3.zero;
            if (YAxis) targetDir.x = Mathf.Lerp(RotationRangesY.x, RotationRangesY.y, LeverValueY);
            if (XAxis) targetDir.y = Mathf.Lerp(RotationRangesX.x, RotationRangesX.y, LeverValueX);
            LeverTransform.localRotation = Quaternion.Euler(targetDir);
        }

        protected override void UpdateIn()
        {
            base.UpdateIn();

            if (Holding)
            {
                canvasGroup.alpha = 0f;

                LeverValueY = Mathf.Clamp(YValue, 0f, 100f);
                LeverValueY /= 100f;

                LeverValueX = Mathf.Clamp(XValue, 0f, 100f);
                LeverValueX /= 100f;

                UpdatePullableOrientation();
            }
        }

    }

}