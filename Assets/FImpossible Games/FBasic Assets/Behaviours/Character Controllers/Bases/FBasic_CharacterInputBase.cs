using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class which is controlling inputs for character controller movement
    /// Thanks to this you can make more custom character input controll or
    /// easier create AI behaviour for this controller
    /// </summary>
    public abstract class FBasic_CharacterInputBase : MonoBehaviour
    {
        protected FBasic_CharacterMovementBase characterController;

        protected virtual void Start()
        {
            characterController = GetComponent<FBasic_CharacterMovementBase>();
        }

        protected virtual void Update()
        {
            SetInputAxis(new Vector2(0f, 0f));
        }

        protected virtual void OnDisable()
        {
            SetInputAxis(new Vector2(0f, 0f));
        }

        /// <summary>
        /// Setting input axis for controller to define movement directions
        /// </summary>
        public void SetInputAxis(Vector2 inputAxis)
        {
            characterController.SetInputAxis(inputAxis);
        }

        /// <summary>
        /// Trigger jump action in controller
        /// </summary>
        public void Jump()
        {
            characterController.SetJumpInput();
        }

        /// <summary>
        /// Set reference direction for controller movement behaviour for Y axis (for player can be for example main camera Y rotation)
        /// </summary>
        public void SetInputDirection(float yDirection)
        {
            characterController.SetInputDirection(yDirection);
        }

        /// <summary>
        /// Calculate axis value clamped to -1, 1 and 0 with 0.1f range in order to analog axes
        /// </summary>
        protected float CalculateClampedAxisValue(string axis = "Vertical")
        {
            float axisInput = Input.GetAxis(axis);

            float value = 0f;

            if (axisInput < -0.2f || axisInput > 0.2f) value = Mathf.Sign(axisInput);

            return value;
        }
    }
}