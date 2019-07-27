using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public FirstPersonController fpc;

    private float xSens;
    private float ySens;

    private float walkSpeed;
    private float runSpeed;

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

        walkSpeed = fpc.m_WalkSpeed;
        runSpeed = fpc.m_RunSpeed;

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

    public void DisableCameraMovement()
    {
        fpc.m_MouseLook.XSensitivity = 0;
        fpc.m_MouseLook.YSensitivity = 0;
    }

    public void EnableCameraMovement()
    {
        fpc.m_MouseLook.XSensitivity = xSens;
        fpc.m_MouseLook.YSensitivity = ySens;
    }

    public void DisablePlayerMovement()
    {
        fpc.m_WalkSpeed = 0;
        fpc.m_RunSpeed = 0;
    }

    public void EnablePlayerMovement()
    {
        fpc.m_WalkSpeed = walkSpeed;
        fpc.m_RunSpeed = runSpeed;
    }
}
