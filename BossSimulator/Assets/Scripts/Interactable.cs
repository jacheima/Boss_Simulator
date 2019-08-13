using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialouge;
    public DialogueManager dialogueManager;

    public float radius = 2f;

    public void StartInteraction()
    {
        //Start the dialogue by going to the StartDialogue function in the dialogue manager
        dialogueManager.StartDialogue(dialouge);
    }

    void OnDrawGizmosSelected()
    {
        //Show the distance in the editor

        //Set the color to yellow
        Gizmos.color = Color.yellow;

        //Draw the sphere so the designers can see the area the player needs to be in to interact with the character
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
