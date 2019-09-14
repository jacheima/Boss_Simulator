using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FBasics
{
    public class FBasic_Demo_AddDialogueToConversation : MonoBehaviour
    {
        public FBasic_Conversation TargetConversation;
        public string DialogueTitle = "New Dialogue";

        [Space(6f)]
        public List<string> Replys;
        [Space(1f)]
        public List<bool> PlayerReply;

        [Space(7f)]
        public bool StarterDialogue = false;
        public bool EndingDialogue = false;


        private void Reset()
        {
            TargetConversation = GetComponent<FBasic_Conversation>();
        }


        void Start()
        {
            if ( TargetConversation == null ) TargetConversation = GetComponent<FBasic_Conversation>();

            if (Replys.Count != PlayerReply.Count)
            {
                Debug.LogError("Wrong reply count in order to 'PlayerReply' actor ids' count");
                Destroy(this);
                return;
            }

            FBasic_DialogueBase targetDialogue = new FBasic_DialogueBase(DialogueTitle);
            targetDialogue.Replys = new List<FBasic_ReplyBase>();

            for (int i = 0; i < Replys.Count; i++)
            {
                FBasic_ReplyBase reply = new FBasic_ReplyBase();
                reply.Text = Replys[i];

                if (PlayerReply[i] == true) reply.TalkActor = FBasic_ReplyBase.FReplyTalker.Player; else reply.TalkActor = FBasic_ReplyBase.FReplyTalker.NPC;

                targetDialogue.AddReply(reply);
            }

            if (EndingDialogue) targetDialogue.Replys[targetDialogue.Replys.Count - 1].EndConversation = true;

            if (StarterDialogue)
                TargetConversation.StarterDialogue = targetDialogue;
            else
                TargetConversation.AddDialogue(targetDialogue);
        }


        private void OnValidate()
        {
            if (Replys.Count != PlayerReply.Count)
            {
                if (Replys[0] == "") Replys[0] = DialogueTitle;

                PlayerReply.Clear();

                for (int i = 0; i < Replys.Count; i++)
                {
                    PlayerReply.Add(i % 2 == 0);
                }
            }
        }
    }
}