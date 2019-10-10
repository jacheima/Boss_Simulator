using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject hireButton;
    public GameObject workButton;
    public GameObject FPC;

    // Start is called before the first frame update
    void Start()
    {
        hireButton.SetActive(false);
        workButton.SetActive(false);
        mainCamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, 2f);

        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.tag == "Desk")
                {
                    Debug.Log("I'm near the desk");
                    RaycastHit rayHit;

                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out rayHit))
                    {
                        if (rayHit.collider.tag == "Desk")
                        {
                            Debug.Log("I see the Desk");

                            if (Input.GetMouseButtonDown(0))
                            {
                               hireButton.SetActive(true);
                               FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = false;
                            }
                        }
                    }
                }

                if (hit[i].gameObject.tag == "Employee")
                {
                    Debug.Log("I'm near an employee");

                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit rHit))
                    {
                        if (rHit.collider.tag == "Employee")
                        {
                            Debug.Log("I See the Employee");

                            if (Input.GetMouseButtonDown(0))
                            {
                                workButton.SetActive(true);
                                FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ClickNotOnAButton()
    {
        if (hireButton.activeSelf)
        {
            hireButton.SetActive(false);
            FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = true;
        }

        if (workButton.activeSelf)
        {
            workButton.SetActive(false);
            FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = true;
        }
    }
}
