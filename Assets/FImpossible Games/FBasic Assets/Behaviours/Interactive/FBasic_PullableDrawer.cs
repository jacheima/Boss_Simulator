using System.Collections;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Example of using FBasic_Pullable to create hinge pullable object
    /// </summary>
    public class FBasic_PullableDrawer : FBasic_Pullable
    {
        public float PullValue { get; protected set; }

        public Renderer ToEmmit;

        public Vector3 DrawedPositionOffset;
        public bool ReversePull = false;
        [Range(0.5f, 10f)]
        public float Deceleration = 6f;

        [Tooltip("Reverse pull direction if behind object (objects opened up and down (Y-Axis) should have it at false)")]
        public bool ReverseIfBack = true;
        public bool NoDiffVelo = false;

        protected Vector3 initialPosition;
        protected Vector3 initialLocalPosition;

        private Vector3 targetPositionOffset;
        private bool animationFinished;
        private float startSensitivity;
        private float translationIncreaser = 1f;
        private float velocity = 0f;
        private float lookDot = 0f;

        private void Reset()
        {
            DrawedPositionOffset = Vector3.forward;
            textInCanvas = "HOLD AND PULL";
            Sensitivity = 0.5f;
        }

        protected override void Start()
        {
            base.Start();
            initialPosition = transform.position;
            initialLocalPosition = transform.localPosition;

            PullValue = YValue / 100f;
            UpdatePullableOrientation();
            startSensitivity = Sensitivity;

            UpdatePullableOrientation();
            transform.localPosition = initialLocalPosition + transform.TransformVector(targetPositionOffset);
        }

        protected override void UpdatePullableOrientation()
        {
            base.UpdatePullableOrientation();
            targetPositionOffset = Vector3.Lerp(Vector3.zero, DrawedPositionOffset, PullValue);
        }

        protected override void UpdateIn()
        {
            base.UpdateIn();

            Vector3 startPos = transform.localPosition;
            Vector3 startPosW = transform.position;

            transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPosition + transform.TransformVector(targetPositionOffset), Time.deltaTime * 7f * translationIncreaser);
            Vector3 postPosW = transform.position;

            float diff = Vector3.Distance(transform.localPosition, initialLocalPosition + transform.TransformVector(targetPositionOffset));
            translationIncreaser = Mathf.Lerp(translationIncreaser, Mathf.Lerp(1f, 3f, Mathf.InverseLerp(0.001f, 0.3f, diff)), Time.deltaTime * 8f);

            if (diff < 0.005f && velocity < 0.005f) animationFinished = true; else animationFinished = false;

            if (Holding)
            {
                canvasGroup.alpha = 0f;

                Sensitivity = -startSensitivity;

                if (ReversePull) Sensitivity *= -1f;

                if (EnteredTransform) if (lookDot > 0f) Sensitivity *= -1f;

                PullValue = Mathf.Clamp(YValue, 0f, 100f);
                PullValue /= 100f;

                if (YValue < 0f) YValue = 0f;
                if (YValue > 100f) YValue = 100f;

                UpdatePullableOrientation();

                float diffVel;
                if (NoDiffVelo)
                {
                    //Debug.Log("PRe pos, now pos: " + startPos + " , " + transform.position + " sum = " + SumVector(startPos) + " , " + SumVector(transform.position) + " diff = " + ((SumVector(startPos) - SumVector(transform.position))));
                    diffVel = diff * Mathf.Sign((initialLocalPosition - transform.localPosition).magnitude - targetPositionOffset.magnitude);
                }
                else
                {
                    //diffVel = diff * Mathf.Sign(SumVector(transform.position - startPos));
                    diffVel = diff * Mathf.Sign(SumVector(transform.InverseTransformVector(postPosW) - transform.InverseTransformVector(startPosW)));
                }

                //if (!ReversePull) diffVel = -diffVel;
                velocity = Mathf.Lerp(velocity, diffVel, Time.deltaTime * 12f);
            }
            else
            {
                velocity = Mathf.Lerp(velocity, 0f, Time.deltaTime * Deceleration);
                YValue += velocity * (40 / DrawedPositionOffset.magnitude) * Time.deltaTime * 12f;

                if (YValue < 0f) YValue = 0f;
                if (YValue > 100f) YValue = 100f;

                PullValue = Mathf.Clamp(YValue, 0f, 100f);
                PullValue = YValue / 100f;

                targetPositionOffset = Vector3.Lerp(Vector3.zero, DrawedPositionOffset, PullValue);

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
            if ( ReverseIfBack) lookDot = Vector3.Dot( transform.TransformVector( DrawedPositionOffset).normalized, Camera.main.transform.forward);
            conditionalExit = true;
            base.StartHolding();
        }


        private float SumVector(Vector3 v)
        {
            return v.x + v.y + v.z;
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = new Color(0.4f, 0.4f, 1f, 0.95f);
                Gizmos.DrawWireSphere(transform.position, 0.2f);

                Gizmos.color = new Color(1f, 0.4f, 0.4f, 0.95f);
                Gizmos.DrawWireSphere(transform.position + transform.TransformVector(DrawedPositionOffset) , 0.2f);

                if (StartValueY > 0f)
                {
                    Gizmos.color = new Color(0.5f, 1f, 0.5f, 0.95f);
                    Gizmos.DrawWireSphere(Vector3.Lerp(transform.position, transform.position + transform.TransformVector(DrawedPositionOffset), StartValueY), 0.2f);
                }
            }
        }
    }
}