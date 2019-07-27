using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Dialogue dialogue;

    // Start is called before the first frame update
    public void StartInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }

    void EndInteraction()
    {

    }
}
