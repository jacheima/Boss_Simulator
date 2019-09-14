using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic character movement input for hard coded keys
    /// </summary>
    public class FBasic_CharacterInputKeys : FBasic_CharacterInputBase
    {
        protected override void Update()
        {
            Vector2 inputValue = Vector2.zero;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) inputValue.x = -1; else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) inputValue.x = 1;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) inputValue.y = 1; else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) inputValue.y = -1;

            SetInputAxis(inputValue);
            SetInputDirection(Camera.main.transform.eulerAngles.y);
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }
    }
}
