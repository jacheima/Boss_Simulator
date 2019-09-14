using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Main component to controll scripts responsible for character movement, animations etc. using Rigidbody component
    /// Ready to be inherited in any way.
    /// I don't use character controller because it is expensive when it goes to use for example 50 of them (CharacterController.Move() is expensive, just make test for 200 CharControllers and then 200 rigidbodies, difference is huge)
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class FBasic_RigidbodyMovement : FBasic_CharacterHorizontalBase
    {
        /// <summary> Just rigidbody needed to move object </summary>
        public Rigidbody CharacterRigidbody { get; protected set; }

        /// <summary> When character starts to float in very small height by rigidbody bump, we raycast from foots to check if we should stand on ground anyway </summary>
        public float SkinHeight = 0.1f;

        /// <summary> Fake gravity to have more controll on rigidbody </summary>
        protected float fakeYVelocity = -3f;

        /// <summary> Flag to determinate if we pushing forward on wall </summary>
        public bool FacingWall = false;

        /// <summary> Flag to determinate if we pushing to sides on wall </summary>
        public bool SideingWall = false;

        /// <summary> When character want to move diagonally, acceleration is multiplied by this value </summary>
        protected float diagonalMultiplier = 0.7f;

        /// <summary> Capsule collider to use some variables from it </summary>
        protected CapsuleCollider capsuleCollider;

        /// <summary> Remembered velocity for some helper actions on rigidbody </summary>
        protected Vector3 targetVelocity = Vector3.zero;

        /// <summary> To avoid detecting ground under controller after jump, OnCollisionStay is executed after FixedUpdate() </summary>
        protected bool jumpCollisionFrameOffset = false;

        [Header("When we go down the slope, preventing from bumping")]
        public float PushDownYVelocity = -15.0f;

        /// <summary> Layer mask for hitting ground by controller raycasts, you can override it with your layers </summary>
        public static LayerMask ChracterLayerMask = 0;

        protected override void Start()
        {
            base.Start();

            CharacterRigidbody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            if (ChracterLayerMask == 0)
            {
                ChracterLayerMask = ~(1 << LayerMask.NameToLayer("Water"));
            }
        }

        protected override void FixedUpdate()
        {
            if (Grounded)
            {
                // Modifiy main variables without interfering with it's smoothing formulas
                float accForw = accelerationForward;
                float accRight = accelerationRight;

                // If moving diagonally, speed should be compensated
                if (accForw != 0f && accRight != 0f)
                {
                    accForw *= diagonalMultiplier;
                    accRight *= diagonalMultiplier;
                }

                Vector3 newVelocity = new Vector3(lastTargetVelocityForward.x * accForw, CharacterRigidbody.velocity.y, lastTargetVelocityForward.z * accForw);
                newVelocity += new Vector3(lastTargetVelocityRight.x * accRight, 0f, lastTargetVelocityRight.z * accRight);
                targetVelocity = newVelocity;

                CharacterRigidbody.velocity = newVelocity;
            }

            base.FixedUpdate();
        }

        /// <summary>
        /// Update is executed after FixedUpdate()
        /// </summary>
        protected override void Update()
        {
            base.Update();
            CheckGroundPlacement();
        }
        /// <summary>
        /// We raycast to ground and placing character on ground when he starts to float for a little bit from rigidbody velocity
        /// </summary>
        protected void CheckGroundPlacement()
        {
            if (!Jumped)
            {
                if (!Grounded)
                {
                    RaycastHit groundHit;
                    Ray groundRay = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);

                    if (Physics.Raycast(groundRay, out groundHit, SkinHeight + 0.2f, ChracterLayerMask, QueryTriggerInteraction.Ignore))
                    {
                        // Calculating difference in y for bottom of collider and game object to place it on ground correctly
                        float colliderToTransformHeight = (capsuleCollider.bounds.center.y - capsuleCollider.bounds.extents.y) - transform.position.y;
                        Grounded = true;
                        transform.position = new Vector3(transform.position.x, groundHit.point.y - colliderToTransformHeight, transform.position.z);
                        CharacterRigidbody.velocity = new Vector3(CharacterRigidbody.velocity.x, fakeYVelocity, CharacterRigidbody.velocity.z);
                        CharacterRigidbody.AddForce(new Vector3(0, PushDownYVelocity * 5f, 0));
                        fakeYVelocity = -3f;
                    }
                }
            }
        }

        protected override void MovementCalculations()
        {
            if (inputJump)
            {
                if (Grounded) Jump();
                inputJump = false;
            }

            base.MovementCalculations();

            if (Grounded)
            {
                fakeYVelocity = -3f;
                // Pushing controller little to the ground, so it can go down slope without bumps
                CharacterRigidbody.AddForce(new Vector3(0, PushDownYVelocity, 0));
            }
            else // When character is in air
            {
                // Apply gravity in air, not by add force, because add force was stealing controll and we
                // would need to do some more calculations to prevent controller from stucking at walls when is in air
                CharacterRigidbody.velocity = new Vector3(CharacterRigidbody.velocity.x, fakeYVelocity, CharacterRigidbody.velocity.z);
                fakeYVelocity -= Time.fixedDeltaTime * GravityPower * CharacterRigidbody.mass;
            }

            Grounded = false;
            FacingWall = false;
            SideingWall = false;
        }

        protected override void RotationCalculations()
        {
            if (Grounded)
            {
                // Just rotating controller to camera front when trying to move
                if (verticalValue != 0f) targetDirection = inputDirection;
                else
                if (horizontalValue != 0f) targetDirection = inputDirection;

                base.RotationCalculations();
            }
        }

        protected override void Jump()
        {
            fakeYVelocity = CalculateJumpYVelocity() * 1.2f;
            CharacterRigidbody.MovePosition(transform.position + Vector3.up * fakeYVelocity * Time.fixedDeltaTime);

            Jumped = true;
            Grounded = false;
            jumpCollisionFrameOffset = true;
        }

        /// <summary>
        /// If we colliding with something, let's check if we stand on it or other
        /// </summary>
        private void OnCollisionStay(Collision collision)
        {
            if (jumpCollisionFrameOffset)
            {
                jumpCollisionFrameOffset = false; return;
            }

            float dotPrecision = 0.5f;

            // Check if we collide with something under our legs
            for (int i = 0; i < collision.contacts.Length; i++)
                if (Vector3.Dot(collision.contacts[i].point - transform.position, transform.up) < dotPrecision)
                {
                    Grounded = true;
                    Jumped = false;
                    break;
                }

            CheckIfFacingWall(collision);
        }

        /// <summary>
        /// If we hit something over character controller when jumping, we starting to fall down
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            if (Vector3.Dot(collision.contacts[0].point - transform.position, transform.up) > 1f)
            {
                if (fakeYVelocity > -0.02f) fakeYVelocity = -0.02f;
            }
        }

        /// <summary>
        /// Method used to check if controller is facing or sideing wall
        /// (used previously to prevent character stucking in walls when in air)
        /// </summary>
        private void CheckIfFacingWall(Collision collision)
        {
            int collisionVertical = -1;

            // Check if we facing wall if moving forward
            if (accelerationForward != 0f)
                for (int i = 0; i < collision.contacts.Length; i++)
                {
                    float verticalSign = 1f;
                    if (!onlyForward)
                    {
                        verticalSign *= Mathf.Sign(verticalValue);
                    }

                    if (Vector3.Dot(collision.contacts[i].point - transform.position, transform.forward * verticalSign) > 0.25f)
                    {
                        FacingWall = true;
                        collisionVertical = i;
                        break;
                    }
                }

            // Check if we facing wall if moving to sides
            if (accelerationRight != 0f)
                for (int i = 0; i < collision.contacts.Length; i++)
                {
                    if (i == collisionVertical) continue;

                    if (Vector3.Dot(collision.contacts[i].point - transform.position, transform.right * Mathf.Sign(horizontalValue)) > 0.25f)
                    {
                        SideingWall = true;
                        break;
                    }
                }
        }
    }

}