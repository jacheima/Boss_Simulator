using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Main component to controll scripts responsible for character movement, animations etc. using CharacterController component
    /// Ready to be inherited in any way.
    /// I don't use character controller because it is expensive when it goes to use for example 50 of them (CharacterController.Move() is expensive).
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterController))]
    public class FBasic_CharacterController : FBasic_CharacterHorizontalBase
    {
        public Rigidbody CharacterRigidbody { get; protected set; }

        /// <summary> Character controller to operate on </summary>
        protected CharacterController characterController;

        protected Vector3 translationVector;
        protected float yVelocity = 0f;

        [Tooltip("Switch to use SimpleMove() method in CharacterController instead of Move()")]
        public bool SimpleMove = false;

        [Header("When we go down the slope, preventing from bumping")]
        public float pushDownYVelocity = -0.085f;

        protected override void Start()
        {
            base.Start();

            characterController = GetComponent<CharacterController>();
            CharacterRigidbody = GetComponent<Rigidbody>();

            if (CharacterRigidbody)
            {
                CharacterRigidbody.isKinematic = true;
                CharacterRigidbody.freezeRotation = false;
                CharacterRigidbody.useGravity = false;
            }
        }

        protected override void MovementCalculations()
        {
            // Calculating translation vector and others them moveing using CharacterController
            Grounded = characterController.isGrounded;
            if (Grounded) Jumped = false;

            base.MovementCalculations();

            translationVector = lastTargetVelocityForward * accelerationForward;
            translationVector += lastTargetVelocityRight * accelerationRight;
            translationVector *= Time.fixedDeltaTime * 0.72f;

            if (inputJump)
            {
                if (Grounded) Jump();
                inputJump = false;
            }

            if (Grounded)
            {
                yVelocity = pushDownYVelocity;
            }
            else
            {
                yVelocity -= GravityPower * Time.fixedDeltaTime / 55.5f;
            }

            translationVector.y = yVelocity;

            if (SimpleMove)
                characterController.SimpleMove(translationVector * 50f);
            else
                characterController.Move(translationVector);
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
            Grounded = false;
            Jumped = true;
            yVelocity = CalculateJumpYVelocity() / 51.8f;
        }

        /// <summary>
        /// When collider hit something over it during jump
        /// </summary>
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (Vector3.Dot(hit.point - transform.position, transform.up) > 1f)
            {
                if (yVelocity > -0.02f) yVelocity = -0.02f;
            }
        }
    }

}
