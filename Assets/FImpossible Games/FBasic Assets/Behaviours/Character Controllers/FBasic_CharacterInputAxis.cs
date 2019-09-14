using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic character movement input
    /// </summary>
    public class FBasic_CharacterInputAxis : FBasic_CharacterInputBase
    {
        protected override void Update()
        {
            SetInputAxis(new UnityEngine.Vector2(CalculateClampedAxisValue("Horizontal"), CalculateClampedAxisValue("Vertical")));
            SetInputDirection(Camera.main.transform.eulerAngles.y);
            if (Input.GetButtonDown("Jump")) Jump();
        }
    }
}
