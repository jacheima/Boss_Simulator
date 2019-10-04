using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UsingComputer : MonoBehaviour
{
    public ButtonStart buttonconfalse;
    public GameObject Computer;
    public GameObject Canvas;
    public GameObject player;
    public GameObject playcam;
    

    void Awake()
    {

    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Scriptcontrole()
    {

    }
    void usComputer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
    void OnTriggerEnter(Collider sphere)
    {
        Canvas.GetComponent<Canvas>().enabled = true;
       // player.GetComponent<characterController>().enabled = false;
        playcam.GetComponent<camMouseLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnTriggerExit(Collider sphere)
    {
        Canvas.GetComponent<Canvas>().enabled = false;
        playcam.GetComponent<camMouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
      buttonconfalse.ConversationContainerFalse();
    }
}

