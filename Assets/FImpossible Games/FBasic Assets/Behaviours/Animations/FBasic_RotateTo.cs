using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Basic script to rotate transform towards choosed rotation
    /// </summary>
    public class FBasic_RotateTo : MonoBehaviour
    {
        public Vector3 TargetRotation = new Vector3(0f, 0f, 0f);

        /// <summary> Multiplies deltaTime </summary>
        public float RotationSpeed = 5f;

        /// <summary> If animator should go on for example during game pause (useful for UI) </summary>
        public bool UnscaledDeltaTime = false;

        public Transform TargetTransform;

        protected virtual void Update()
        {
            float delta;
            if (UnscaledDeltaTime) delta = Time.unscaledDeltaTime; else delta = Time.deltaTime;

            // !UPDATE!
            if (TargetTransform)
                TargetRotation = Quaternion.LookRotation(TargetTransform.position - transform.position).eulerAngles;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(TargetRotation), delta * RotationSpeed);
        }
    }
}
