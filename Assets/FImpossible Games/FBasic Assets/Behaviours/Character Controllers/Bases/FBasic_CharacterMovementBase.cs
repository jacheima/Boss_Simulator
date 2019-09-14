using System;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Main component to calculate basic variables for movement, and hold base variables.
    /// Need to be inherited to work.
    /// You can use it for AI but you need to change axes calculations to own.
    /// Recommending to use rigidbody movement, because character controller's Move() method is expensive when it comes to use by about 50 objects
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class FBasic_CharacterMovementBase : MonoBehaviour
    {
        [Header("Main speed factor")]
        public float MaxSpeed = 8f;

        /// <summary> Flag to determinate if we standing on something</summary>
        public bool Grounded = false;

        /// <summary> Flag to determinate when character is in air to know if he was jumping or he if just falling </summary>
        public bool Jumped = false;

        /// <summary> Target direction, in which controller should move </summary>
        protected float targetDirection = 0f;
        /// <summary> Variable to smooth translate to desired rotation </summary>
        protected float animatedDirection = 0f;

        /// <summary> If acceleration value should go only positive </summary>
        protected bool onlyForward = false;

        /// <summary> Gravity power on object </summary>
        public float GravityPower = 25f;

        [Header("How quick acceleration should be lerped")]
        public float AccelerationSpeed = 10f;

        [Header("How quick deceleration should be lerped")]
        public float DecelerateSpeed = 6f;

        /// <summary> Main acceleration value going positive and negative </summary>
        protected float accelerationForward = 0f;

        /// <summary> Velocity calculation variable, goes only positive </summary>
        protected Vector3 newVelocityForward = Vector3.zero;

        /// <summary> Remembered movement velocity, not setted when character is trying to stop </summary>
        protected Vector3 lastTargetVelocityForward = Vector3.zero;

        /// <summary> Input value in default from axis calculated in MovementCalculations() </summary>
        protected float verticalValue = 0f;


        /// <summary> Input data for controller </summary>
        protected Vector2 inputAxes = Vector2.zero;

        /// <summary> Input data for direction </summary>
        protected float inputDirection = 0f;

        /// <summary> Jump input flag </summary>
        protected bool inputJump = false;

        /// <summary>
        /// Override for own stuff
        /// </summary>
        protected virtual void Start()
        {
            inputDirection = transform.eulerAngles.y;
            targetDirection = inputDirection;
        }

        /// <summary> 
        /// Default clock update 
        /// </summary>
        protected virtual void Update()
        {
            RotationCalculations();
        }

        /// <summary> 
        /// Physical clock update 
        /// </summary>
        protected virtual void FixedUpdate()
        {
            MovementCalculations();
        }

        /// <summary> 
        /// Calculate basic rotation for controller - override whole for your own needs
        /// </summary>
        protected virtual void RotationCalculations()
        {
            targetDirection += inputAxes.x * Time.deltaTime * 150f;

            animatedDirection = Mathf.Lerp(animatedDirection, targetDirection, Time.deltaTime * 20f);
            transform.rotation = Quaternion.Euler(0f, animatedDirection, 0f);
        }

        /// <summary> 
        /// Physical calculations to move rigidbody as character controller 
        /// </summary>
        protected virtual void MovementCalculations()
        {
            if (Grounded) // If we aren't in air we have some controll
            {
                verticalValue = inputAxes.y;
                newVelocityForward = new Vector3(0f, 0f, verticalValue);

                // Ranges to catch analog values - move forward
                if (verticalValue > 0f)
                {
                    MoveForward(false);
                }
                else if (verticalValue < 0f) // move backward 
                {
                    MoveForward(true);
                }
                else // Not pressing any axis for movement forward or backward
                {
                    StoppingMovement();
                }
            }
        }

        // Calculate new velocity for controller to move in desired direction
        protected virtual Vector3 CalculateTargetVelocity(Vector3 direction)
        {
            Vector3 newVelocity = direction;
            newVelocity = transform.TransformDirection(newVelocity);
            newVelocity *= MaxSpeed;

            return newVelocity;
        }

        /// <summary>
        /// Calculating acceleration for forward / backward movement
        /// </summary>
        protected virtual void MoveForward(bool backward)
        {
            if ( !backward ) // Just moving forward
            {
                lastTargetVelocityForward = newVelocityForward;
                accelerationForward = Mathf.Min(1f, accelerationForward + AccelerationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                lastTargetVelocityForward = newVelocityForward;

                if (onlyForward) // Can be used when we want character to run towards camera when choosing going back
                {
                    accelerationForward = Mathf.Min(1f, accelerationForward + AccelerationSpeed * Time.fixedDeltaTime);
                }
                else // Default setting, character will step backward, directed to front in this setting
                {
                    accelerationForward = Mathf.Max(-1f, accelerationForward - AccelerationSpeed * Time.fixedDeltaTime);
                }
            }
        }

        /// <summary>
        /// To be executed when vertical axis isn't used, changing acceleration linearly to 0
        /// </summary>
        protected virtual void StoppingMovement()
        {
            if (accelerationForward > 0f) accelerationForward = Mathf.Max(0f, accelerationForward - DecelerateSpeed * Time.fixedDeltaTime);
            else
            if (accelerationForward < 0f) accelerationForward = Mathf.Min(0f, accelerationForward + DecelerateSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Fill with own stuff
        /// </summary>
        protected virtual void Jump()
        {
        }

        /// <summary>
        /// Setting input data for controller
        /// </summary>
        internal void SetInputAxis(Vector2 inputAxis)
        {
            inputAxes = inputAxis;
        }

        /// <summary>
        /// Setting target reference look rotation for controller in Y axis
        /// </summary>
        internal void SetInputDirection(float yDirection)
        {
            inputDirection = yDirection;
        }

        /// <summary>
        /// Setting flag to start jump for controller, used as a flag because jump calculations should be executed in different lines of code
        /// </summary>
        internal void SetJumpInput()
        {
            inputJump = true;
        }

        #region Helper Methods

        protected float CalculateJumpYVelocity()
        {
            return Mathf.Sqrt(4f * GravityPower);
        }

        #endregion

    }
}