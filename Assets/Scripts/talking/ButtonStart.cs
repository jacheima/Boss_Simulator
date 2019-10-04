using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStart : MonoBehaviour
{
    //Grabbing all button associated with talking
    public Canvas canvas;
    public GameObject txtAI;
    public GameObject txt1;
    public GameObject txt2;
    public GameObject txt3;
    public GameObject txt4;
    public GameObject talkButton;
    public GameObject player;

    public GameObject talkButtonText;

    //disabling talkingbutton and activating and activaiting text buttons
    public void StartingConversation()
    {

        txt1.SetActive(true);
        txt2.SetActive(true);
        txt3.SetActive(true);
        txt4.SetActive(true);
        txtAI.SetActive(true);
        talkButton.SetActive(false);
        player.GetComponent<characterController>().enabled = false;

        //canvas.enabled = false;

    }

    public void ConversationContainerFalse()
    {
        txt1.SetActive(false);
        txt2.SetActive(false);
        txt3.SetActive(false);
        txt4.SetActive(false);
        txtAI.SetActive(false);
        talkButton.SetActive(true);
        player.GetComponent<characterController>().enabled = true;
    }
}