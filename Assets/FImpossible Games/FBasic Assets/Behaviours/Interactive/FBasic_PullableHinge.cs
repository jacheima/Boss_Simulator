using System.Collections;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Example of using FBasic_Pullable to create hinge pullable object
    /// </summary>
    public class FBasic_PullableHinge : FBasic_Pullable
    {
        public float PullValue { get; protected set; }

        public Renderer ToEmmit;

        public Vector2 RotationRanges = new Vector2(0f, 90f);
        public Vector3 RotationAxis = new Vector3(0f, 1f, 0f);
        public bool ReversePull = false;

        [Range(0.5f, 10f)]
        public float Deceleration = 3f;

        protected Quaternion initialRotation;

        private Quaternion offsetRotation;
        //private Quaternion closeRotation;

        //private Vector3 initForward;

        private bool animationFinished;
        private float startSensitivity;
        private float rotationIncreaser = 1f;
        private float velocity = 0f;
        private float lookDot = 0f;


        protected override void Start()
        {
            initialRotation = transform.localRotation;

            base.Start();

            //closeRotation = Quaternion.Euler(RotationAxis.x * RotationRanges.x, RotationAxis.y * RotationRanges.x, RotationAxis.z * RotationRanges.x);
            //initForward = closeRotation * Vector3.forward;

            startSensitivity = Sensitivity;

            PullValue = StartValueY;

            UpdatePullableOrientation();
            transform.localRotation = initialRotation * offsetRotation;

            velocity = 0f;
        }


        protected override void UpdatePullableOrientation()
        {
            base.UpdatePullableOrientation();
            float range = Mathf.Lerp(RotationRanges.x, RotationRanges.y, PullValue);
            offsetRotation = Quaternion.Euler(range * RotationAxis.x, range * RotationAxis.y, range * RotationAxis.z);
        }

        protected override void UpdateIn()
        {
            base.UpdateIn();

            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation * offsetRotation, Time.deltaTime * 9f * rotationIncreaser);

            float angleDiff = Quaternion.Angle(transform.localRotation, offsetRotation);

            if (Holding)
            {
                Vector3 forwardA = transform.localRotation * Vector3.forward;
                Vector3 forwardB = (initialRotation * offsetRotation) * Vector3.forward;
                float angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
                float angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;
                float diff = -Mathf.DeltaAngle(angleA, angleB);
                if (ReversePull) diff = -diff;

                if (YValue < 0f) YValue = 0f;
                if (YValue > 100f) YValue = 100f;

                velocity = Mathf.Lerp(velocity, diff, Time.deltaTime * 9f);
            }
            else
            {
                velocity = Mathf.Lerp(velocity, 0f, Time.deltaTime * Deceleration);
                //YValue += velocity * 0.5f;
                //PullValue += (velocity / 100f) * 0.5f;
                float yAdd = velocity * 2.5f; // * Mathf.Abs(RotationRanges.x - RotationRanges.y)
                YValue += yAdd * Time.deltaTime * 12f;

                if (YValue < 0f) YValue = 0f;
                if (YValue > 100f) YValue = 100f;

                PullValue = YValue / 100f;
                //PullValue += yAdd / 100f;

                float range = Mathf.Lerp(RotationRanges.x, RotationRanges.y, PullValue);
                offsetRotation = Quaternion.Euler(range * RotationAxis.x, range * RotationAxis.y, range * RotationAxis.z);
            }

            rotationIncreaser = Mathf.Lerp(rotationIncreaser, Mathf.Lerp(2f, 4f, Mathf.InverseLerp(5f, 45f, angleDiff)), Time.deltaTime * 10f);

            if (angleDiff < 0.01f && velocity < 0.01f) animationFinished = true; else animationFinished = false;

            if (Holding)
            {
                canvasGroup.alpha = 0f;
                Sensitivity = startSensitivity;
                if (ReversePull) Sensitivity *= -1f;

                // Checking if pull should behave in reverse way
                if (EnteredTransform) if (lookDot < 0f) Sensitivity *= -1f;

                PullValue = Mathf.Clamp(YValue, 0f, 100f);
                PullValue /= 100f;

                UpdatePullableOrientation();
            }
            else
            {
                if (animationFinished) conditionalExit = false;
            }

            if (ToEmmit)
            {
                if (mouseEntered)
                    FColorMethods.LerpMaterialColor(ToEmmit.material, "_EmissionColor", Color.white * 0.3f);
                else
                    FColorMethods.LerpMaterialColor(ToEmmit.material, "_EmissionColor", Color.black);
            }
        }


        protected override void StartHolding()
        {
            lookDot = Vector3.Dot(Camera.main.transform.forward, transform.forward);
            conditionalExit = true;
            base.StartHolding();
        }


        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                Vector3 size = new Vector3(1.5f, 0.2f, 0.2f);
                Gizmos.color = new Color(0.4f, 0.4f, 1f, 0.95f);
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.right * size.x / 2, size);

                Gizmos.color = new Color(1f, 0.4f, 0.4f, 0.95f);
                Vector3 eulLimB = new Vector3(RotationAxis.x * RotationRanges.y, RotationAxis.y * RotationRanges.y, RotationAxis.z * RotationRanges.y);
                Quaternion limitAngleB = Quaternion.Euler(eulLimB);
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation * limitAngleB, Vector3.one);
                Gizmos.DrawWireCube(Vector3.right * size.x / 2, size);

                Gizmos.color = new Color(1f, 0.4f, 0.4f, 0.95f);
                size = new Vector3(1.3f, 0.15f, 0.15f);
                Vector3 eulLimA = new Vector3(RotationAxis.x * RotationRanges.x, RotationAxis.y * RotationRanges.x, RotationAxis.z * RotationRanges.x);
                Quaternion limitAngleA = Quaternion.Euler(eulLimA);
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation * limitAngleA, Vector3.one);
                Gizmos.DrawWireCube(Vector3.right * size.x / 2, size);

                Gizmos.color = new Color(0.6f, 1f, 0.6f, 0.8f);
                Quaternion startRotation = Quaternion.Euler(Vector3.Lerp(eulLimA, eulLimB, StartValueY));
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation * startRotation, Vector3.one);
                size = new Vector3(1.2f, 0.1f, 0.1f);
                Gizmos.DrawWireCube(Vector3.right * size.x / 2, size);
            }
        }
    }
}