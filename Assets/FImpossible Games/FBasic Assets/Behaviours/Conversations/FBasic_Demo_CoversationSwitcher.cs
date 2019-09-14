using FIMSpace.FBasics;
using UnityEngine;

public class FBasic_Demo_CoversationSwitcher : MonoBehaviour
{
    public CanvasGroup Button;
    public FBasic_Conversation Conversation;

	public void StartConversation ()
    {
        Conversation.ShowConversation();
	}

    public void Update()
    {
        if (Conversation.IsWorking)
        {
            Button.alpha -= Time.deltaTime * 5f;
            Button.interactable = false;
        }
        else
        {
            Button.alpha += Time.deltaTime * 5f;
            Button.interactable = true;
        }
    }
}
