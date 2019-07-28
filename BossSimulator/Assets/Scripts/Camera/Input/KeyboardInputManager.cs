using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : InputManager
{
    //EVENTS
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static ZoomInputHandler OnZoomInput;

    void Update()
    {
        //Move Functionality
        if (Input.GetKey(KeyCode.W))
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnMoveInput?.Invoke(Vector3.right);
        }

        //Rotate Functionality
        if (Input.GetKey(KeyCode.Q))
        {
            OnRotateInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            OnRotateInput?.Invoke(1f);
        }

        //Zoom Functionality
        if (Input.GetKey(KeyCode.Z))
        {
            OnZoomInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.X))
        {
           OnZoomInput?.Invoke(1f);
        }
    }
}
