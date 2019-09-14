using System.Collections;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Example of using FBasic_FBasic_InteractionAreaCanvas to create draggable physical functionality
    /// </summary>
    public class FBasic_Draggable : FBasic_InteractionAreaCanvas
    {
        public bool Holding { get; protected set; }

        [Header("< Draggable Parameters >")]

        public Rigidbody TargetRigidbody;

        [Range(0f, 3f)]
        public float HardFollow = 1f;
        [Range(0f, 1f)]
        public float FollowMassRatio = 1f;

        [Range(0f, 3f)]
        public float ThrowMultiplier = 1f;

        //[Tooltip("Raycast check range")]
        //public float ObstacleCheckRange = 0.5f;

        private Camera refCamera;

        private Vector3 holdOffset;
        private Vector3 preTargetPos;
        private Vector3 holdVelocity;
        private Quaternion holdCameraOrientation;
        private Collider rigColl;

        private void Reset()
        {
            textInCanvas = "Hold";
            GetTrigger();
            TargetRigidbody = GetComponentInChildren<Rigidbody>();
            if (!TargetRigidbody) TargetRigidbody = GetComponentInParent<Rigidbody>();
        }


        protected override void Start()
        {
            Holding = false;
            EventOnInteraction.AddListener(StartHolding);

            fixedDelay = new WaitForFixedUpdate();

            if (!TargetRigidbody)
            {
                TargetRigidbody = GetComponentInChildren<Rigidbody>();
                if (!TargetRigidbody)
                {
                    Debug.Log("There is no Rigidbody in " + name + " destroying 'FBasic_Draggable' component!");
                    Destroy(this);
                    return;
                }
            }

            refCamera = Camera.main;

            rigColl = TargetRigidbody.GetComponent<Collider>();

            base.Start();
        }


        protected override Collider GetTrigger()
        {
            Collider[] colls = GetComponents<Collider>();

            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].isTrigger)
                {
                    triggerArea = colls[i];
                    break;
                }
            }

            if (!triggerArea)
            {
                triggerArea = gameObject.AddComponent<SphereCollider>();
                triggerArea.isTrigger = true;
            }

            return triggerArea;
        }



        protected virtual void StartHolding()
        {
            if (!Holding)
            {
                LockedInteraction = this;
                holdOffset = refCamera.transform.position - transform.position;
                holdOffset = Quaternion.LookRotation(holdOffset, refCamera.transform.forward) * Vector3.back * holdOffset.magnitude;
                holdCameraOrientation = refCamera.transform.rotation;
                Holding = true;
            }
        }


        protected override void UpdateIn()
        {
            base.UpdateIn();

            if (Holding)
            {
                canvasGroup.alpha = 0f;
                if (Input.GetKeyUp(InteractionKey)) StopHolding();
            }
        }

        private readonly float pow = 40f;
        //private readonly ForceMode mode = ForceMode.Acceleration;
        //private readonly bool wasObstacle = false;

        private float lerpedDistBlender = 0.1f;
        private WaitForFixedUpdate fixedDelay;

        protected virtual IEnumerator UpdateInFixed()
        {
            while (true)
            {
                if (Holding)
                {
                    Vector3 targetPosition = refCamera.transform.position + (refCamera.transform.rotation * Quaternion.Inverse(holdCameraOrientation)) * holdOffset;
                    holdVelocity = Vector3.Lerp(holdVelocity, targetPosition - preTargetPos, Time.fixedDeltaTime * 15f);

                    float rayLen;
                    if (rigColl) rayLen = rigColl.bounds.extents.magnitude; else rayLen = 1f;

                    Ray ray = new Ray(transform.position, targetPosition - transform.position);
                    RaycastHit obstacle;
                    Physics.Raycast(ray, out obstacle, ray.direction.magnitude * rayLen, ~0, QueryTriggerInteraction.Ignore);

                    float dist = Vector3.Distance(TargetRigidbody.position, targetPosition);
                    float distPower = Mathf.Lerp(1f, 0.1f, Mathf.InverseLerp(rigColl.bounds.size.magnitude, 0.1f, dist));

                    Vector3 targetSmoothPos = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * (15f * HardFollow) / Mathf.Lerp(1f, (0.25f + TargetRigidbody.mass / 1.5f), FollowMassRatio));

                    float moveLerper = 0f;

                    if (obstacle.transform)
                    {
                        moveLerper = Mathf.Lerp(0.75f, 1f, Mathf.InverseLerp(1f, 0.1f, distPower));
                        distPower /= 2.5f;
                        //Debug.Log("OBST! lerper = " + moveLerper + " distpow = " + distPower);
                    }
                    else
                    {
                        if (distPower < 0.5f)
                        {
                            moveLerper = Mathf.Lerp(.5f, 0.1f, Mathf.InverseLerp(1f, 0.5f, distPower));
                        }
                        else
                        {
                            moveLerper = Mathf.Lerp(0.0f, 1f, Mathf.InverseLerp(0.5f, 0.01f, distPower));
                        }
                    }

                    lerpedDistBlender = Mathf.Lerp(lerpedDistBlender, moveLerper, Time.fixedDeltaTime * 10f);

                    TargetRigidbody.MovePosition(Vector3.Lerp(targetSmoothPos, TargetRigidbody.position, lerpedDistBlender));

                    Vector3 finalForce = ((targetPosition - TargetRigidbody.transform.position) * HardFollow * pow) / (Mathf.Max(1f, (TargetRigidbody.mass * FollowMassRatio) / 3f));
                    TargetRigidbody.AddForce(Vector3.Lerp(finalForce, Vector3.zero, distPower));

                    TargetRigidbody.useGravity = false;
                    TargetRigidbody.angularVelocity = Vector3.Lerp(TargetRigidbody.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 1f);

                    preTargetPos = targetSmoothPos;

                    //TargetRigidbody.velocity = (holdVelocity * (10f * HardFollow) / Mathf.Lerp(1f, (0.1f + TargetRigidbody.mass), FollowMassRatio)) * ThrowMultiplier * 0.45f;

                    // If we go too far from dragged object when we stuck it in something
                    if (Vector3.Distance(transform.position, refCamera.transform.position) > triggerArea.bounds.extents.magnitude * 2f)
                    {
                        StopHolding();
                    }
                }

                yield return fixedDelay;
            }
        }

        /// <summary>
        /// Resetting values for holding object
        /// </summary>
        protected virtual void StopHolding()
        {
            if (LockedInteraction == this) UnlockInteraction();

            lerpedDistBlender = 0.1f;
            Holding = false;
            TargetRigidbody.constraints = RigidbodyConstraints.None;
            TargetRigidbody.useGravity = true;
            TargetRigidbody.velocity = (holdVelocity * (10f * HardFollow) / Mathf.Lerp(1f, (0.1f + TargetRigidbody.mass), FollowMassRatio)) * ThrowMultiplier;

            if (!EnteredFlag)
            {
                conditionalExit = false;
                OnTriggerExit(EnteredTransform.GetComponent<Collider>());
            }

            //if (!Entered)
            //{
            //    StopAllCoroutines();
            //    OnExit();
            //}
        }



        protected override IEnumerator UpdateIfInRange()
        {
            StartCoroutine("UpdateInFixed");
            yield return base.UpdateIfInRange();
        }

    }
}