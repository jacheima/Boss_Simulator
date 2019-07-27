using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public FirstPersonController fpc;

    public float xSens;
    public float ySens;

    public bool isInteracting = false;

    //if isInInteraction is true
    //unlock mouse position
    //un-hide cursor
    //set camera position
    //freeze camera position and rotation
    //lock player movement

    void Start()
    {
        xSens = fpc.m_MouseLook.XSensitivity;
        ySens = fpc.m_MouseLook.YSensitivity;
    }

    void Update()
    {
        if (isInteracting)
        {
            DisableCameraMovement();
        }
        else
        {
            EnableCameraMovement();
        }
    }

    void DisableCameraMovement()
    {
        fpc.m_MouseLook.XSensitivity = 0;
        fpc.m_MouseLook.YSensitivity = 0;
    }

    void EnableCameraMovement()
    {
        
    }

    void DisablePlayerMovement()
    {

    }

    void EnablePlayerMovement()
    {

    }
}
