using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Simple class which is adding physical impact on target rigidbody for provided time
    /// </summary>
    public class FBasic_AddPhysicalImpact : MonoBehaviour
    {
        public Rigidbody TargetRigidbody;
        public Vector3 PushDirection = Vector3.forward;
        public float PushDirectionSeconds = 1f;
        public Vector3 RandomDirMultiplier = Vector3.zero;
        public float PowerMultiplier = 10f;

        private Vector3 randomImpact = Vector3.zero;
        private float timer = 0f;

        private void Reset()
        {
            TargetRigidbody = GetComponentInChildren<Rigidbody>();
        }

        void Start()
        {
            if (TargetRigidbody == null)
            {
                Destroy(this);
                return;
            }

            if (RandomDirMultiplier.x != 0f) randomImpact.x = Random.Range(0f, RandomDirMultiplier.x);
            if (RandomDirMultiplier.y != 0f) randomImpact.y = Random.Range(0f, RandomDirMultiplier.y);
            if (RandomDirMultiplier.z != 0f) randomImpact.z = Random.Range(0f, RandomDirMultiplier.z);

            randomImpact.Normalize();
            float sum = randomImpact.x + randomImpact.y + randomImpact.z;
            randomImpact /= sum;
        }

        void Update()
        {
            TargetRigidbody.AddForce((PushDirection + randomImpact) * PowerMultiplier, ForceMode.Force);

            if (timer > PushDirectionSeconds)
            {
                Destroy(this);
            }

            timer += Time.deltaTime;
        }
    }
}