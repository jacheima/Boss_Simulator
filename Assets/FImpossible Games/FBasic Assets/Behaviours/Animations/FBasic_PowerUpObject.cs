using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class with basic behaviour for power up object animations and logic
    /// </summary>
    public class FBasic_PowerUpObject : MonoBehaviour
    {
        [Header("Variables to controll animation of power up")]
        public float YFloatingValue = 0.25f;
        public float FloatingSpeedRate = 4f;
        public float RotationSpeed = 125f;

        [Header("If collision is detected with object of given tag, power up will be collected")]
        public string TagToCollideWith = "Player";

        /// <summary> Remember initial position to offset it with trigonometric function </summary>
        private Vector3 initPosition;

        /// <summary> Random value to offset a little time for randomness if there is more power ups in the same time </summary>
        private float randomOffset;

        /// <summary> 
        /// Initial settings 
        /// </summary>
        void Start()
        {
            initPosition = transform.position;

            randomOffset = Random.Range(-Mathf.PI, Mathf.PI);

            transform.Rotate(0f, Random.Range(-180f, 180f), 0f);
        }

        /// <summary>
        /// Update animation for power up object
        /// </summary>
        void Update()
        {
            transform.Rotate(0f, RotationSpeed * (Time.deltaTime ), 0f);
            transform.position = initPosition + new Vector3(0f, Mathf.Sin((Time.time + randomOffset) * FloatingSpeedRate) * YFloatingValue, 0f);
        }

        /// <summary>
        /// When object's collider meets player
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == TagToCollideWith)
            {
                Collect();
            }
        }

        /// <summary>
        /// Method which can be overrided, executed when player (with rigidbody) collide with this object by trigger collider
        /// </summary>
        protected void Collect()
        {
            Destroy(gameObject);
        }
    }
}
