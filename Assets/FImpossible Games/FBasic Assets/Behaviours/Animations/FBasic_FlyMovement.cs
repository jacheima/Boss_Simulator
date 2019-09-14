using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class to animate axis values to create procedural animation of randomized fly movement
    /// </summary>
    public class FBasic_FlyMovement : MonoBehaviour
    {
        [Tooltip("How quick model should fly on it's trajectory")]
        public float MainSpeed = 1f;

        [Tooltip("How far should fly our object")]
        public Vector3 RangeValue = Vector3.one;

        [Tooltip("Multiplier for range value but applied to all axes")]
        public float RangeMul = 5f;

        [Tooltip("Additional translation on y axis if you want movement to be little like butterfly flight")]
        public float AddYSin = 1f;
        public float AddYSinTimeSpeed = 1f;

        [Tooltip("How quick object should rotate to it's forward movement direction")]
        public float RotateForwardSpeed = 10f;

        private float time;

        /* Calculations variables for trigonometric movement */
        private Vector3 offset;
        private Vector3 initPos;
        private Vector3 preOffsetNoYAdd;
        private Vector3 posOffsetNoYAdd;
        private Vector3 speeds;

        private Vector3 randomVector1;
        private Vector3 randomVector2;


        void Start()
        {
            initPos = transform.position;
            time = Random.Range(-Mathf.PI * 3, Mathf.PI * 3);

            // Choosing random value vectors to make trigonometric methods behave kinda random
            randomVector1 = FVectorMethods.RandomVector(4.5f, 6.5f);

            randomVector2.x = Random.Range(10f, 12f);
            randomVector2.y = Random.Range(3.25f, 4.75f);
            randomVector2.z = Random.Range(2.55f, 4.25f);
        }

        void Update()
        {
            time += Time.deltaTime * MainSpeed;

            // Getting smooth curved motion using trigonometric functions with randomized parameters
            float sinX = (Mathf.Sin(time) * randomVector1.x + Mathf.Sin(time * 1.5f + 3.5f) * 4f + Mathf.Cos(time * 1.5f + 3.5f) * randomVector2.x) * MainSpeed;
            float sinY = (Mathf.Cos(time) * randomVector1.y + Mathf.Sin(time * 1.75f + 4.2f) * randomVector2.y) * MainSpeed;
            float sinZ = (Mathf.Cos(time) * randomVector1.z + Mathf.Cos(time * 1.25f - 2.2f) * randomVector2.z) * MainSpeed;

            offset.x += sinX;
            offset.y += sinY;
            offset.z += sinZ;

            // Calculating multipliers and translation
            Vector3 targetOffsetedPos = offset;
            targetOffsetedPos.x *= RangeValue.x;
            targetOffsetedPos.y *= RangeValue.y;
            targetOffsetedPos.z *= RangeValue.z;

            targetOffsetedPos.y += ((Mathf.Cos(time * AddYSinTimeSpeed * 2.2f + 4.2f) * 7f + Mathf.Sin(time * AddYSinTimeSpeed * 3.0f + 4.2f) * 5f) * AddYSin) * MainSpeed;

            // Calculating position for look rotation
            Vector3 trueTargetPos = initPos + targetOffsetedPos * RangeMul * 0.001f;

            if (RotateForwardSpeed > 0f)
            {
                Quaternion look = Quaternion.LookRotation(trueTargetPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * RotateForwardSpeed);
            }

            // Setting new position
            transform.position = trueTargetPos;
        }
    }
}
