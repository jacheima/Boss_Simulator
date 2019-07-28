using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StopMovement : MonoBehaviour
{
    public GameManager gm;
    public void DisableMouseLook()
    {
        gm.DisableCameraMovement();
    }

    public void EnableMouseLook()
    {
        gm.EnableCameraMovement();
    }

    public void DisablePlayerMovement()
    {
        gm.DisablePlayerMovement();
    }

    public void EnablePlayerMovement()
    {
        gm.EnablePlayerMovement();
    }
}
