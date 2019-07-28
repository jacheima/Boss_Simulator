using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    public GameManager gm;



    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {

        //Set the condition for opening the menu to true
        animator.SetBool("isOpen", true);

        //clear sentences
        sentences.Clear();

        //if there is a textFile
        if (dialogue.textDialogueFile)
        {
            dialogue.sentences = dialogue.textDialogueFile.text.Split("\n"[0]);
        }

        //for each sentence in the dialogue section
        foreach (string sentence in dialogue.sentences)
        {
            //Add them to the queue
            sentences.Enqueue(sentence);

        }

        //set the isInteraction bool to true in the gameManager
        gm.isInteracting = true;

        //Set the name in the dialogue box to the name in the dialogue script
        nameText.text = dialogue.name;

        //Display the next sentence
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //If there are no more sentences in the queue
        if (sentences.Count == 0)
        {
            //Go to the end dialogue function
            EndDialogue();
            return;
        }

        //Dequeue the sentence the player just saw
        string sentence = sentences.Dequeue();
        //Show the next sentence in the queue
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        //Set is Interating to false in the game manager
        gm.isInteracting = false;

        //close the interaction menu by setting its parameter to false
        animator.SetBool("isOpen", false);


    }
}
