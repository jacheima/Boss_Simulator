using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Basic script to rotate transform in choosed axis with spinning sinusoidal manner
    /// </summary>
    public class FBasic_RotateSpinSin : MonoBehaviour
    {
        [Tooltip("In which axis object should rotate")]
        public Vector3 RotationAxis = Vector3.up;

        [Tooltip("How far can go rotation")]
        public float RotationRange = 40f;

        [Tooltip("How fast object should rotate to it's ranges")]
        public float SinSpeed = 2f;

        private float time;

        private void Start()
        {
            time = Random.Range(-Mathf.PI, Mathf.PI);
        }

        void Update()
        {
            // Simple use of trigonometric function
            time += Time.deltaTime * SinSpeed;
            transform.Rotate(RotationAxis * Time.deltaTime * RotationRange * Mathf.Sin(time));
        }
    }
}