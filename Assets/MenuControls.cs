using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    public GameObject hireButton;
    public GameObject workButton;
    public GameObject bob;
    public Transform chair;
    public GameObject clone;
    public Interactable interactable;

    private Vector3 pos;
    private Quaternion rotation;

    public void HireButton()
    {
        hireButton.SetActive(false);
#pragma warning disable CS0436 // Type conflicts with imported type
        interactable.FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = true;
#pragma warning restore CS0436 // Type conflicts with imported type
        clone = Instantiate(bob, chair.position, chair.rotation);
        clone.GetComponent<Employee>().workstation = chair;
        Debug.Log("Instantiating Bob");
    }

    //if the player presses the work button from the canvas...
    public void WorkButton()
    {
        workButton.SetActive(false);
#pragma warning disable CS0436 // Type conflicts with imported type
        interactable.FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = true;
#pragma warning restore CS0436 // Type conflicts with imported type
        clone.GetComponent<AI_Controller>().ChangeEmployeeState(AI_Controller.EmployeeStates.Work);
        //this gets the controller script and calls the work function...
        clone.GetComponent<AI_Controller>().Work();
        Debug.Log("Getting To Work");
    }
}
