using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;
    public FirstPersonController FPController;
    public Camera managementCamera;

    private AudioListener FirstPersonListener;
    private AudioListener ManagementListener;

    public bool isManagement = false;

    void Awake()
    {
        FirstPersonListener = firstPersonCamera.GetComponent<AudioListener>();
        ManagementListener = managementCamera.GetComponent<AudioListener>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Switching Camera");
            if (isManagement == false)
            {
                firstPersonCamera.enabled = false;
                FirstPersonListener.enabled = false;
                FPController.enabled = false;
                managementCamera.enabled = true;
                ManagementListener.enabled = true;
                isManagement = true;
            }

            else if (isManagement == true)
            {
                firstPersonCamera.enabled = true;
                FirstPersonListener.enabled = true;
                FPController.enabled = true;
                managementCamera.enabled = false;
                ManagementListener.enabled = false;
                isManagement = false;
            }
        }
    }
}
