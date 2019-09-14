using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Bullet fly forward logic, instantiate and use Init() to launch it
    /// it moves forward on Z axis for gameObject's transform, so be sure for bullet to have correct rotation
    /// </summary>
    public class FBasic_ProjectileBase : MonoBehaviour
    {
        [Tooltip("Speed of the bullet")]
        public float FlySpeed = 100f;

        [Tooltip("How far bullet can fly then beeing destroyed")]
        public float DistanceLimit = 400f;

        private Vector3 initPosition;
        public LayerMask ProjectiletHitMask = 1 << 0;

        protected virtual void Start()
        {
            if (ProjectiletHitMask == 0) ProjectiletHitMask = ~(LayerMask)((1 << LayerMask.NameToLayer("TransparentFX")) | Physics.IgnoreRaycastLayer);
            transform.position += StepForward(.1f);
            initPosition = transform.position;
        }

        protected virtual void Update()
        {
            Vector3 newPosition = transform.position + StepForward();

            RaycastHit hit;
            Physics.Linecast(transform.position, newPosition, out hit, ProjectiletHitMask, QueryTriggerInteraction.Ignore);

            transform.position = newPosition;

            if (hit.transform) HitTarget(hit);

            if (Vector3.Distance(initPosition, transform.position) >= DistanceLimit) GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// When bullet hit target
        /// </summary>
        protected virtual void HitTarget(RaycastHit hit)
        {
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// Returning offset position for bullet movement speed
        /// </summary>
        internal Vector3 StepForward(float multiply = 1f)
        {
            return transform.forward * FlySpeed * multiply * Time.deltaTime;
        }
    }
}