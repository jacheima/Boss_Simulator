using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    databaseParser talking = new databaseParser();
    public Text txtAI;
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    public ButtonStart buttonStart;
    private int currentConvo = 0;

    void Start()
    {
        // Load data into talking class by calling parseDataFromDB function
        talking.ParseDataFromDB();
        displayText();
    }

    // Pull data from the chat class and display the strings on the text objects defined in the inspector
    public void displayText()
    {
        txtAI.text = talking.convo[currentConvo].Conversation;
        txt1.text = talking.convo[currentConvo].Reply1;
        txt2.text = talking.convo[currentConvo].Reply2;
        txt3.text = talking.convo[currentConvo].Reply3;
        txt4.text = talking.convo[currentConvo].Reply4;
    }

    // Set current conversation to the next conversation based on input provided by user button interaction.
    /// <param name="nextConvo">nextConvo is set in the UI buttons in the inspector</param>
    public void takeResponse(int nextConvo)
    {
        // Put a comment here about what the algorithm inside does
        if (currentConvo != 9)
        {
            switch (nextConvo)
            {

                case 1:
                    currentConvo = talking.convo[currentConvo].Replylink1;
                    break;
                case 2:
                    currentConvo = talking.convo[currentConvo].Replylink2;
                    break;
                case 3:
                    currentConvo = talking.convo[currentConvo].Replylink3;
                    break;
                case 4:
                    currentConvo = talking.convo[currentConvo].Replylink4;
                    break;
                default:

                    break;
            }
            displayText();
        }
        else
        {
            resetConvo();
        }
        disableInactiveReplies();
    }

    public void resetConvo()
    {
        currentConvo = 0;
        takeResponse(currentConvo);
        buttonStart.ConversationContainerFalse();
    }

    public void disableInactiveReplies()
    {
        if (talking.convo[currentConvo].Reply1 == "")
        {
            buttonStart.txt1.SetActive(false);
        }
        if (talking.convo[currentConvo].Reply2 == "")
        {
            buttonStart.txt2.SetActive(false);
        }
        if (talking.convo[currentConvo].Reply3 == "")
        {
            buttonStart.txt3.SetActive(false);
        }
        if (talking.convo[currentConvo].Reply4 == "")
        {
            buttonStart.txt4.SetActive(false);
        }
    }
}
