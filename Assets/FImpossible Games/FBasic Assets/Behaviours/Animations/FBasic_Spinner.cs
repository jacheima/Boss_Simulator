using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Component which spins object in choosed axis and decelerating to 0
    /// </summary>
    public class FBasic_Spinner : FBasic_Rotator
    {

        [Tooltip("How quick spinning should slow down")]
        public float DeceleratePower = 300f;

        private void Start()
        {
            RotationSpeed = 0f;
        }

        protected override void Update()
        {
            // Animating and slowing down acceleration for spinner object
            float delta;
            if (UnscaledDeltaTime) delta = Time.unscaledDeltaTime; else delta = Time.deltaTime;

            if (RotationSpeed > 0f) RotationSpeed = Mathf.Max(0f, RotationSpeed - DeceleratePower * delta);
            if (RotationSpeed < 0f) RotationSpeed = Mathf.Min(0f, RotationSpeed + DeceleratePower * delta);

            transform.Rotate(RotationAxis, delta * RotationSpeed);
        }

        /// <summary>
        /// Adding acceleration to object rapidly
        /// </summary>
        public void Spin(float power = 500f)
        {
            RotationSpeed = power;
        }
    }
}
