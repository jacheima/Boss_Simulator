using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class using FBasic_CharacterMovementBase but overriding it with calculations to move sideways
    /// Should be inherited to work.
    /// </summary>
    public abstract class FBasic_CharacterHorizontalBase : FBasic_CharacterMovementBase
    {
        /// <summary> Right side acceleration value going positive and negative </summary>
        protected float accelerationRight = 0f;

        /// <summary> Velocity calculation variable, goes only positive </summary>
        protected Vector3 newVelocityRight = Vector3.zero;

        /// <summary> Remembered movement velocity, not setted when character is trying to stop </summary>
        protected Vector3 lastTargetVelocityRight = Vector3.zero;

        /// <summary> Input value in default from axis calculated in MovementCalculations() </summary>
        protected float horizontalValue = 0f;

        protected override void MovementCalculations()
        {
            if (Grounded) // If we aren't in air we have some controll
            {
                verticalValue = inputAxes.y;
                newVelocityForward = CalculateTargetVelocity(new Vector3(0f, 0f, Mathf.Abs(verticalValue) ));

                // Ranges to catch analog values - move forward
                if (verticalValue > 0f)
                    MoveForward(false);
                else
                    if (verticalValue < 0f) // move backward 
                    MoveForward(true);
                else // Not pressing any axis for movement forward or backward
                    StoppingMovement();

                // ----------------------------------------------

                horizontalValue = inputAxes.x;
                newVelocityRight = CalculateTargetVelocity(new Vector3(Mathf.Abs(horizontalValue), 0f, 0f) );

                // Ranges to catch analog values - move right
                if (horizontalValue > 0f)
                    MoveRight(false);
                else
                if (horizontalValue < 0f) // move left 
                    MoveRight(true);
                else // Not pressing any axis for movement forward or backward
                    StoppingSidewaysMovement();
            }
        }

        protected virtual void MoveRight(bool leftSide)
        {
            if (!leftSide) // Just moving to right axis side
            {
                lastTargetVelocityRight = newVelocityRight;
                accelerationRight = Mathf.Min(1f, accelerationRight + AccelerationSpeed * Time.fixedDeltaTime);
            }
            else // Moving left side
            {
                lastTargetVelocityRight = newVelocityRight;
                accelerationRight = Mathf.Max(-1f, accelerationRight - AccelerationSpeed * Time.fixedDeltaTime);
            }
        }

        protected virtual void StoppingSidewaysMovement()
        {
            if (accelerationRight > 0f) accelerationRight = Mathf.Max(0f, accelerationRight - DecelerateSpeed * Time.fixedDeltaTime);
            else
            if (accelerationRight < 0f) accelerationRight = Mathf.Min(0f, accelerationRight + DecelerateSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// You must set target direction by yourself or check other derived classes
        /// </summary>
        protected override void RotationCalculations()
        {
            // Lerping just one value instead of 4 from Quaternion.Slerp()
            animatedDirection = Mathf.LerpAngle(animatedDirection, targetDirection, Time.deltaTime * 20f);

            transform.rotation = Quaternion.Euler(0f, animatedDirection, 0f);
        }
    }
}