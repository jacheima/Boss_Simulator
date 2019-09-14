using UnityEngine;
using UnityEngine.UI;

namespace FIMSpace.FBasics
{
    /// <summary>
    /// FM: References needed for conversation setup to work
    /// </summary>
    public class FBasic_Conversation_UI : MonoBehaviour
    {
        public FBasic_Conversation Conversation;
        public CanvasGroup MainCanvasG;

        [Space(4f)]
        public CanvasGroup DialogueCanvasG;

        [Space(4f)]
        public CanvasGroup ReplyCanvasG;
        public Image ReplyBG;
        public Text ReplyText;

        [Space(4f)]
        public GameObject DialOptionPrefab;


        public void ClickedDialogueOption(GameObject clicked)
        {
            Conversation.ClickedOption(clicked);
        }
    }
}