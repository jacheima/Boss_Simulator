using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FIMSpace.FBasics
{
    /// <summary>
    /// FM: Class which is controlling UI conversation / dialogues system
    /// </summary>
    public class FBasic_Conversation : MonoBehaviour
    {
        [Space(3)]
        [Tooltip("Reference to UI setup for conversation system")]
        public GameObject UIPrefab;

        [Range(0.1f, 3f)]
        public float AnimationSpeed = 1f;

        [Tooltip("Name that will be shown inside reply (left empty to not show names)")]
        public string PlayerName = "";
        public Transform PlayerReference;
        public Vector3 PlayerHeadOffset;

        [Tooltip("Name that will be shown inside reply (left empty to not show names)")]
        public string NPCName = "NPC";
        public Transform NPCReference;
        public Vector3 NPCHeadOffset;

        [Tooltip("Dialogue which will run when conversation is starting (can be null -> then dialogue options will appear instead of dialogue)")]
        [HideInInspector]
        public FBasic_DialogueBase StarterDialogue;

        [Tooltip("List of avaiable dialogues, their titles will appear in UI dialogues selection window")]
        [HideInInspector]
        public List<FBasic_DialogueBase> AvailableDialogues;

        public bool IsWorking { get; protected set; }
        public bool IsHiding { get; protected set; }
        public FBasic_Conversation_UI ConversationUI { get; protected set; }

        public FBasic_DialogueBase ActiveDialogue { get; protected set; }
        public FBasic_ReplyBase ActiveReply { get; protected set; }

        public int ActiveReplyIndex { get; protected set; }


        #region VARIABLES - Non public

        /// <summary> Dialogue options generated as clickable UI Game Objects</summary>
        protected List<GameObject> generatedDialogueOptions;
        /// <summary> Dialogue options Ids equivalent to generatedDialogueOptions with indexes so we can easily choose correct dialogue options without need to create additional component to each dialogue option button </summary>
        protected List<FBasic_DialogueBase> generatedDialogueOptionsIds;

        /// <summary> When fade in/out of showing reply or dialogues animation is finished </summary>
        protected bool fadeAnimationFinished = true;
        /// <summary> If animation of showing reply etc. finished</summary>
        protected bool softAnimationFinished = true;

        protected bool hidingReply = false;

        private Canvas mainCanvas;
        private GraphicRaycaster canvasRaycaster;

        #endregion


        #region Base Component Methods


        /// <summary>
        /// When script is starting to live, we hide UI and prepare some variables
        /// </summary>
        protected virtual void Start()
        {
            IsWorking = false;
            IsHiding = true;

            ActiveDialogue = null;
            ActiveReply = null;
            ActiveReplyIndex = 0;

            ConversationUI = UIPrefab.GetComponent<FBasic_Conversation_UI>();
            ConversationUI.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            ConversationUI.name = ConversationUI.name.Replace("(Clone)", "");

            mainCanvas = ConversationUI.GetComponent<Canvas>();
            canvasRaycaster = ConversationUI.GetComponent<GraphicRaycaster>();

            OnScriptStarts();

            if (ConversationUI != null)
            {
                HideAllInstantly();
            }

            enabled = false;
        }


        /// <summary>
        /// Handling animating windows, fadeing in / out canvas etc.
        /// </summary>
        protected virtual void Update()
        {
            if (IsWorking)
            {
                // Animating text canvases
                AnimateSelectionWindow();
                AnimateReplyWindow();

                // Controlling switching replys etc.
                if (ActiveDialogue != null)
                {
                    ActiveDialogue.DialogueUpdate();

                    if (hidingReply)
                        if (softAnimationFinished)
                            if (ActiveDialogue.Replys != null)
                                if (ActiveReplyIndex >= ActiveDialogue.Replys.Count)
                                {
                                    EndDialogue(AvailableDialogues);
                                }
                                else
                                {
                                    ShowReply();
                                }
                }

                OnWorkingUpdate();

                if (ActiveReply != null) ActiveReply.ReplyUpdate();
            }

            // Controlling main canvas alpha
            if (!IsHiding)
            {
                ConversationUI.MainCanvasG.alpha = Mathf.Lerp(ConversationUI.MainCanvasG.alpha, 1.05f, Time.unscaledDeltaTime * 10f + Time.unscaledDeltaTime * 2f);
            }
            else
            {
                ConversationUI.MainCanvasG.alpha = Mathf.Lerp(ConversationUI.MainCanvasG.alpha, -0.05f, Time.unscaledDeltaTime * 10f + Time.unscaledDeltaTime * 2f);
                if (ConversationUI.MainCanvasG.alpha <= 0f) FinishingHidingConversationSystem();
            }
        }


        #endregion


        #region Conversation system controll methods


        /// <summary>
        /// Method called to start showing UI, override to lock stuff like camera / player movement inside your game code etc.
        /// </summary>
        public virtual void ShowConversation()
        {
            if (!IsWorking)
            {
                enabled = true;

                IsWorking = true;
                IsHiding = false;

                HideDialogueOptions();
                SkipAnimation();
                HideReply();
                SkipAnimation();

                if (mainCanvas) mainCanvas.enabled = true;
                if (canvasRaycaster) canvasRaycaster.enabled = true;

                ConversationUI.Conversation = this;
                ConversationUI.DialogueCanvasG.alpha = 0f;
                ConversationUI.ReplyCanvasG.alpha = 0f;

                OnShowConversation();

                SetDefaultCameraOrientation();

                StarterDialogoue();
            }
        }

        /// <summary>
        /// Starting procedure of closing conversation windows.
        /// Animation of hiding conversation must be finished to make all stuff like camera etc. unlock
        /// </summary>
        public virtual void EndConversation()
        {
            if (IsWorking)
            {
                IsWorking = false;
                HideDialogueOptions();
                HideReply();
                IsHiding = true;
            }
        }



        /// <summary>
        /// Starting choosed dialogue to progress
        /// </summary>
        protected virtual void StartDialogue(FBasic_DialogueBase dial)
        {
            ActiveDialogue = dial;
            dial.AssignOwner(this);

            dial.OnDialogueStart();
            ActiveReplyIndex = 0;
            ActiveReply = ActiveDialogue.Replys[ActiveReplyIndex];
            hidingReply = false;

            ShowReply();
        }

        /// <summary>
        /// Finishing showing replys and showing again dialogue options
        /// </summary>
        protected virtual void EndDialogue(List<FBasic_DialogueBase> dialogues)
        {
            ShowDialogueOptions(dialogues);
        }

        /// <summary>
        /// Adding dialogue to available dialogue options list
        /// </summary>
        internal void AddDialogue(FBasic_DialogueBase targetDialogue)
        {
            if (AvailableDialogues == null) AvailableDialogues = new List<FBasic_DialogueBase>();
            if (!AvailableDialogues.Contains(targetDialogue)) AvailableDialogues.Insert(0, targetDialogue);
        }

        /// <summary>
        /// Triggering starter dialogue if it exists, if not, we will show available dialogue buttons
        /// </summary>
        public virtual void StarterDialogoue()
        {
            if (StarterDialogue != null)
                if (StarterDialogue.Replys.Count > 0)
                {
                    StartDialogue(StarterDialogue);
                    return;
                }

            ShowDialogueOptions(AvailableDialogues);
        }

        /// <summary>
        /// Generating and showing dialogue options inside conversation
        /// </summary>
        protected virtual void ShowDialogueOptions(List<FBasic_DialogueBase> dialogues)
        {
            GenerateDialogueOptions(dialogues);

            ConversationUI.DialogueCanvasG.blocksRaycasts = true;
            ConversationUI.ReplyCanvasG.blocksRaycasts = false;
            ConversationUI.MainCanvasG.blocksRaycasts = true;

            OnShowDialogueOptions();

            if (ActiveDialogue != null) ActiveDialogue.OnDialogueEnd();
            if (ActiveReply != null) ActiveReply.OnReplySkip();

            ActiveDialogue = null;
            ActiveReply = null;

            SetDefaultCameraOrientation();
        }

        /// <summary>
        /// Triggering animation for hiding dialogue options of this conversation
        /// </summary>
        protected virtual void HideDialogueOptions()
        {
            ConversationUI.DialogueCanvasG.blocksRaycasts = false;
        }



        /// <summary>
        /// Skip key will skip appearing animation, then when pressed again it will jump to next reply / finish dialogue etc.
        /// If hard is set to true it will skip both animation and go to next reply etc. without need for two clicks.
        /// </summary>
        protected virtual void SkipKey(bool hard = false)
        {
            if (ActiveDialogue != null && ActiveDialogue.Replys != null)
            {
                if (ActiveReplyIndex >= ActiveDialogue.Replys.Count)
                {
                    EndDialogue(AvailableDialogues);
                }
                else
                {
                    SkipReply(hard);
                }
            }
            else
            {
                SkipAnimation();
            }
        }

        /// <summary>
        /// Skipping reply to trigger next one
        /// </summary>
        protected virtual void SkipReply(bool hard = false, bool hardSkipAnimation = true)
        {
            SkipAnimation();

            if (!softAnimationFinished) return;
            if (hidingReply) return;

            ActiveReplyIndex += 1;

            if (ActiveDialogue.Replys.Count > ActiveReplyIndex)
            {
                ActiveReply.OnReplySkip();
                ActiveReply = ActiveDialogue.Replys[ActiveReplyIndex];
            }

            HideReply();

            if (hard)
            {
                SkipAnimation();

                if (ActiveDialogue.Replys.Count > ActiveReplyIndex)
                {
                    ShowReply();
                    if (hardSkipAnimation) SkipAnimation();
                }
            }
        }

        /// <summary>
        /// Updating checking keys to skip messages etc.
        /// </summary>
        protected virtual void InputUpdate()
        {
            if (Input.GetMouseButtonDown(0)) SkipKey();
            else if (Input.GetKeyDown(KeyCode.Space)) SkipKey();
            else if (Input.GetKeyDown(KeyCode.Escape)) SkipKey(true);
        }



        /// <summary>
        /// Animating window which is viewing dialogue texts etc.
        /// </summary>
        protected virtual void AnimateReplyWindow(float skipProgress = 0f)
        {
            if (ConversationUI.ReplyCanvasG.blocksRaycasts)
            {
                ConversationUI.ReplyCanvasG.alpha = Mathf.Lerp(ConversationUI.ReplyCanvasG.alpha, 1.05f, Time.unscaledDeltaTime * 7f * AnimationSpeed + skipProgress) + Time.unscaledDeltaTime;
                if (ConversationUI.ReplyCanvasG.alpha == 1f) softAnimationFinished = true;
            }
            else
            {
                ConversationUI.ReplyCanvasG.alpha = Mathf.Lerp(ConversationUI.ReplyCanvasG.alpha, -0.05f, Time.unscaledDeltaTime * 8f * AnimationSpeed + skipProgress) - Time.unscaledDeltaTime;
                if (ConversationUI.ReplyCanvasG.alpha == 0f) softAnimationFinished = true;
            }
        }

        /// <summary>
        /// Animating window which containing dialogue options to choose
        /// </summary>
        protected virtual void AnimateSelectionWindow(float skipProgress = 0f)
        {
            if (ConversationUI.DialogueCanvasG.blocksRaycasts)
            {
                ConversationUI.DialogueCanvasG.alpha = Mathf.Lerp(ConversationUI.DialogueCanvasG.alpha, 1.05f, Time.unscaledDeltaTime * 7f * AnimationSpeed + skipProgress) + Time.unscaledDeltaTime;
            }
            else
            {
                ConversationUI.DialogueCanvasG.alpha = Mathf.Lerp(ConversationUI.DialogueCanvasG.alpha, -0.05f, Time.unscaledDeltaTime * 10f * AnimationSpeed + skipProgress) - Time.unscaledDeltaTime;
            }
        }

        /// <summary>
        /// Immedietely finishing appearing animation for reply or animation of showing dialogue options
        /// </summary>
        protected virtual void SkipAnimation()
        {
            AnimateReplyWindow(1f);
            AnimateSelectionWindow(1f);
        }



        /// <summary>
        /// Viewing reply images and texts
        /// </summary>
        protected virtual void ShowReply()
        {
            ConversationUI.ReplyCanvasG.blocksRaycasts = true;
            ConversationUI.DialogueCanvasG.blocksRaycasts = false;
            ConversationUI.MainCanvasG.blocksRaycasts = true;
            hidingReply = false;

            softAnimationFinished = false;

            ConversationUI.ReplyText.text = "";

            string content = "";

            content += ActiveReply.Text;
            ActiveReply.OnReplyStart();

            ConversationUI.ReplyText.text = content;

            SetReplyCameraOrientation();
        }

        /// <summary>
        /// Hiding reply images
        /// </summary>
        protected virtual void HideReply()
        {
            ConversationUI.ReplyCanvasG.blocksRaycasts = false;
            softAnimationFinished = false;
            hidingReply = true;
        }



        /// <summary>
        /// Hiding reply and dialogue images immedietely
        /// </summary>
        protected virtual void HideAllInstantly()
        {
            ConversationUI.DialogueCanvasG.blocksRaycasts = false;
            ConversationUI.ReplyCanvasG.blocksRaycasts = false;

            ConversationUI.MainCanvasG.alpha = 0f;
            ConversationUI.MainCanvasG.blocksRaycasts = false;

            FinishingHidingConversationSystem();
        }


        #endregion


        #region UI Events Etc.


        /// <summary>
        /// Generating buttons for dialogue options
        /// </summary>
        public virtual void GenerateDialogueOptions(List<FBasic_DialogueBase> dialogues)
        {
            ClearDialogueOptions();

            if (dialogues == null || dialogues.Count == 0)
            {
                EndConversation();
                Debug.LogError("Provided empty or null dialogue list!");
                return;
            }

            GameObject dialOption = ConversationUI.DialOptionPrefab;

            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject newOption = Instantiate(dialOption, dialOption.transform.parent);
                Text optionText = newOption.GetComponent<Text>();
                optionText.text = (i + 1) + ". " + dialogues[i].Title;
                optionText.color = dialogues[i].TitleColor;
                newOption.SetActive(true);

                generatedDialogueOptions.Add(newOption);
                generatedDialogueOptionsIds.Add(dialogues[i]);
            }
        }


        /// <summary>
        /// Clearing generated dialogue options
        /// </summary>
        public virtual void ClearDialogueOptions()
        {
            if (generatedDialogueOptions == null)
            {
                generatedDialogueOptions = new List<GameObject>();
                generatedDialogueOptionsIds = new List<FBasic_DialogueBase>();
            }

            for (int i = 0; i < generatedDialogueOptions.Count; i++)
            {
                if (generatedDialogueOptions[i] != null) Destroy(generatedDialogueOptions[i]);
            }

            generatedDialogueOptions.Clear();
            generatedDialogueOptionsIds.Clear();
        }


        /// <summary>
        /// When some option in conversation is selected to start dialogue
        /// </summary>
        public virtual void ClickedOption(GameObject button)
        {
            if (!ConversationUI.DialogueCanvasG.blocksRaycasts) return; // We can't start new dialogue when transitions are happening

            for (int i = 0; i < generatedDialogueOptions.Count; i++)
            {
                if (generatedDialogueOptions[i].GetInstanceID() == button.GetInstanceID())
                {
                    StartDialogue(generatedDialogueOptionsIds[i]);
                    break;
                }
            }
        }


        /// <summary>
        /// Method executed when conversation is finishing closing animation
        /// Override to UNLOCK stuff like camera / player movement inside your game code etc.
        /// </summary>
        protected virtual void FinishingHidingConversationSystem()
        {
            enabled = false;
            ConversationUI.ReplyCanvasG.alpha = 0f;
            ConversationUI.DialogueCanvasG.alpha = 0f;

            if ( mainCanvas ) mainCanvas.enabled = false;
            if (canvasRaycaster) canvasRaycaster.enabled = false;
        }


        #endregion


        #region Camera Positioning Methods


        /// <summary>
        /// Basic algorithm for setting talkers camera orientation accordingly to who is speaking etc.
        /// </summary>
        protected virtual void SetReplyCameraOrientation()
        {
            // If talker is triggered few times one reply after another we don't change anything in camera
            if (ActiveDialogue != null)
            {
                if (ActiveReplyIndex > 0)
                {
                    if (ActiveDialogue.Replys[ActiveReplyIndex].TalkActor == ActiveDialogue.Replys[ActiveReplyIndex - 1].TalkActor) return;
                }
            }

            //// Changing camera orientation to look at current actor or on both actors
            //if (Random.Range(0f, 1f) > 0.8f) // Sometimes we will set camera at position where camera look at both actors
            //{
            //    SetDefaultCameraOrientation();
            //}
            //else
            {
                if (ActiveDialogue != null)
                {
                    if (ActiveReply.TalkActor == FBasic_ReplyBase.FReplyTalker.NPC) SetNPCFocusCameraOrientation(); else SetPlayerFocusCameraOrientation();
                }
            }
        }


        /// <summary>
        /// Setting camera position every time dialogue options are going to be viewed
        /// </summary>
        protected virtual void SetDefaultCameraOrientation()
        {
            Transform targetCamera = Camera.main.transform;

            targetCamera.position = CalculateMidPosition(1f);
            float upFactor = .18f;

            Vector3 transformedHead = PlayerReference.TransformVector(PlayerHeadOffset);
            Vector3 transformedNPCHead = NPCReference.TransformVector(NPCHeadOffset);

            targetCamera.position += Vector3.up * Mathf.Lerp(Mathf.Lerp(PlayerReference.position.y, PlayerReference.position.y + transformedHead.y, upFactor), Mathf.Lerp(NPCReference.position.y, NPCReference.position.y + transformedNPCHead.y, upFactor), 0.5f);

            Vector3 lookPosition = CalculateMidPosition(-1f);
            upFactor = .17f;
            lookPosition.y = Mathf.Lerp(Mathf.Lerp(PlayerReference.position.y, PlayerReference.position.y + transformedHead.y, upFactor), Mathf.Lerp(NPCReference.position.y, NPCReference.position.y + transformedNPCHead.y, upFactor), 0.5f);
            targetCamera.rotation = Quaternion.LookRotation(lookPosition - targetCamera.position);
        }


        /// <summary>
        /// Setting camera orientation to look at npc actor inside conversation
        /// </summary>
        protected virtual void SetNPCFocusCameraOrientation()
        {
            Transform targetCamera = Camera.main.transform;

            Vector3 newPosition;
            float betweenDist = Vector3.Distance(PlayerReference.position, NPCReference.position) / 3.5f;
            newPosition = CalculateMidPosition(Random.Range(betweenDist / 8f, betweenDist));
            float upFactor = .2f;

            Vector3 transformedHead = PlayerReference.TransformVector(PlayerHeadOffset);
            Vector3 transformedNPCHead = NPCReference.TransformVector(NPCHeadOffset);

            newPosition += Vector3.up * Mathf.Lerp(Mathf.Lerp(PlayerReference.position.y, PlayerReference.position.y + transformedHead.y, upFactor), Mathf.Lerp(NPCReference.position.y, NPCReference.position.y + transformedNPCHead.y, upFactor), 0.5f);

            targetCamera.position = newPosition;

            Vector3 lookPosition = NPCReference.position + transformedNPCHead + Vector3.down * transformedNPCHead.y * 0.2f;

            newPosition += Quaternion.LookRotation(lookPosition - targetCamera.position) * Vector3.back * Random.Range(0f, 1f / 5f);

            targetCamera.rotation = Quaternion.LookRotation(lookPosition - targetCamera.position);
        }


        /// <summary>
        /// Setting camera orientation to look at player actor inside conversation
        /// </summary>
        protected virtual void SetPlayerFocusCameraOrientation()
        {
            Transform targetCamera = Camera.main.transform;

            Vector3 newPosition;
            float betweenDist = Vector3.Distance(PlayerReference.position, NPCReference.position) / 3.5f;
            newPosition = CalculateMidPosition(Random.Range(betweenDist / 8f, betweenDist));
            float upFactor = .2f;

            Vector3 transformedHead = PlayerReference.TransformVector(PlayerHeadOffset);
            Vector3 transformedNPCHead = NPCReference.TransformVector(NPCHeadOffset);

            newPosition += Vector3.up * Mathf.Lerp(Mathf.Lerp(PlayerReference.position.y, PlayerReference.position.y + transformedHead.y, upFactor), Mathf.Lerp(NPCReference.position.y, NPCReference.position.y + transformedNPCHead.y, upFactor), 0.5f);

            targetCamera.position = newPosition;

            Vector3 lookPosition = PlayerReference.position + transformedHead + Vector3.down * transformedHead.y * 0.2f;

            newPosition += Quaternion.LookRotation(lookPosition - targetCamera.position) * Vector3.back * Random.Range(0f, 1f / 5f);

            targetCamera.rotation = Quaternion.LookRotation(lookPosition - targetCamera.position);
        }


        #endregion


        #region Event Methods

        /// <summary>
        /// Method executed in Start() you can add custom code to be executed
        /// </summary>
        protected virtual void OnScriptStarts()
        {

        }

        /// <summary>
        /// Custom methods executed when conversation starting to be viewed
        /// </summary>
        protected virtual void OnShowConversation()
        {

        }

        /// <summary>
        /// Custom methods executed when dialogue options starting to be viewed
        /// </summary>
        protected virtual void OnShowDialogueOptions()
        {

        }

        /// <summary>
        /// Custom methods executed every frame when component is setted to work (not hide etc.)
        /// </summary>
        protected virtual void OnWorkingUpdate()
        {
            InputUpdate();
        }

        #endregion


        #region Misc. helper methods

        /// <summary>
        /// Calculating basic position for the camera in the scene
        /// PlayerReference and NPCReference variables cannot be null
        /// </summary>
        protected Vector3 CalculateMidPosition(float outOffset = 0.4f)
        {
            Vector3 midPoint = Vector3.Lerp(PlayerReference.position, NPCReference.position, 0.5f);

            Vector3 transformedPlayerHead = PlayerReference.TransformVector(PlayerHeadOffset);
            Vector3 transformedNPCHead = NPCReference.TransformVector(NPCHeadOffset);

            midPoint.y = Mathf.Lerp(PlayerReference.position.y + transformedPlayerHead.y, NPCReference.position.y + transformedNPCHead.y, 0.5f);

            Quaternion outDir = Quaternion.LookRotation(NPCReference.position - PlayerReference.position) * Quaternion.Euler(0f, 90f, 0f);
            midPoint += outDir * Vector3.forward * Vector3.Distance(PlayerReference.position, NPCReference.position) * outOffset;

            return midPoint;
        }


        /// <summary>
        /// Drawing guide elements inside Scene View
        /// </summary>
        protected virtual void OnDrawGizmosSelected()
        {
            if (PlayerReference)
            {
                Gizmos.color = new Color(0.15f, 1f, 0.15f, 0.35f);
                Gizmos.DrawWireSphere(PlayerReference.position + PlayerReference.TransformVector(PlayerHeadOffset), 0.3f);
                Gizmos.DrawLine(PlayerReference.position + PlayerReference.TransformVector(PlayerHeadOffset), PlayerReference.position);
            }

            if (NPCReference)
            {
                Gizmos.color = new Color(0.15f, 1f, 0.15f, 0.35f);
                Gizmos.DrawWireSphere(NPCReference.position + NPCReference.TransformVector(NPCHeadOffset), 0.3f);
                Gizmos.DrawLine(NPCReference.position + NPCReference.TransformVector(NPCHeadOffset), NPCReference.position);
            }
        }

        #endregion

    }

    /// <summary>
    /// FM: Class which is holding basic setup for dialogue and Replys
    /// </summary>
    public class FBasic_DialogueBase
    {
        /// <summary> Conversation which is using this dialogue class </summary>
        public FBasic_Conversation OwnerConversation { get; private set; }

        internal bool DrawDialogue = true;
        internal bool DrawReplys = true;

        [Tooltip("If this dialogue should be coloured in some way")]
        public Color TitleColor = Color.white;

        [Tooltip("Title of the dialogue, this text will be drawed on dialogue selection window")]
        public string Title;

        [Tooltip("List of replys inside this one dialogue")]
        public List<FBasic_ReplyBase> Replys;


        public FBasic_DialogueBase(string title)
        {
            Title = title;
            Replys = new List<FBasic_ReplyBase>();
        }

        public void AssignOwner(FBasic_Conversation conversation)
        {
            OwnerConversation = conversation;
            for (int i = 0; i < Replys.Count; i++) Replys[i].AssignOwner(this);
        }

        /// <summary>
        /// Adding reply to replys list
        /// </summary>
        public void AddReply(FBasic_ReplyBase reply)
        {
            if (Replys == null) Replys = new List<FBasic_ReplyBase>();
            if (!Replys.Contains(reply)) Replys.Add(reply);
        }

        /// <summary>
        /// When dialogue is triggered to be played
        /// </summary>
        public virtual void OnDialogueStart()
        {
        }

        /// <summary>
        /// When dialogue is ending
        /// </summary>
        public virtual void OnDialogueEnd()
        {
        }

        /// <summary>
        /// When dialogue is in progress and drawing replys etc.
        /// </summary>
        public virtual void DialogueUpdate()
        {
        }

    }

    /// <summary>
    /// FM: Simple class which is holding one reply info
    /// </summary>
    public class FBasic_ReplyBase 
    {
        /// <summary> Dialogue in which this reply is used </summary>
        public FBasic_DialogueBase OwnerDialogue { get; private set; }

        [Tooltip("Who is speaking this reply")]
        public FReplyTalker TalkActor = FReplyTalker.NPC;
        public enum FReplyTalker { Player, NPC }

        [Tooltip("The string content of this reply")]
        public string Text = "";

        /// <summary> If this reply should be closing whole conversation </summary>
        public bool EndConversation = false;

        private bool executed = false;


        /// <summary>
        /// Making link to owner dialogue class
        /// </summary>
        public void AssignOwner(FBasic_DialogueBase dialogue)
        {
            OwnerDialogue = dialogue;
        }


        /// <summary>
        /// When reply is starting to be showed
        /// </summary>
        public virtual void OnReplyStart()
        {
            executed = false;
        }


        /// <summary>
        /// When reply is skipped using input or just switched to next reply
        /// </summary>
        public virtual void OnReplySkip()
        {
            if (!executed) ExecuteEvent();
        }


        /// <summary>
        /// Constant Update() loop for single reply
        /// </summary>
        public virtual void ReplyUpdate()
        {
        }


        /// <summary>
        /// Executting event assigned to reply
        /// </summary>
        public virtual void ExecuteEvent()
        {
            if (EndConversation) OwnerDialogue.OwnerConversation.EndConversation();
            executed = true;
        }
    }
}