using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    public GameObject hireButton;
    public GameObject bob;
    public Transform chair;
    public GameObject clone;
    public Interactable interactable;

    private Vector3 pos;
    private Quaternion rotation;

    public void HireButton()
    {
        hireButton.SetActive(false);
        interactable.FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = true;
        clone = Instantiate(bob, chair.position, chair.rotation);
        clone.GetComponent<Employee>().workstation = chair;
        Debug.Log("Instantiating Bob");
    }
}
