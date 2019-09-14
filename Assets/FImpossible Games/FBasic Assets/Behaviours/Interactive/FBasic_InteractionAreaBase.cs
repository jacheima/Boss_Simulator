using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Class for creating interaction area to interact with target object
    /// Update tick called only when object is in trigger range
    /// </summary>
    public abstract class FBasic_InteractionAreaBase : MonoBehaviour
    {
        public static List<FBasic_InteractionAreaBase> EnteredInteractions = new List<FBasic_InteractionAreaBase>();
        public static FBasic_InteractionAreaBase LastEnteredInteraction = null;

        /// <summary> If you are pulling doors or something and it goes out of trigger range, this variable will allow component to update still, after setting to null object will stop updateing </summary>
        public static FBasic_InteractionAreaBase LockedInteraction = null;

        [Tooltip("If character need to look at this object with camera direction, useful for FPS games. Value = 1 Camera can look anywhere interaction always popup, Value = 0.25 camera must look almost directly on object for interaction popup")]
        [Range(0.15f, 1f)]
        public float LookAtRange = 1f;

        [Tooltip("We can add rigidbody for example to objects which are using this component's transform as additional child object fpr pbject's features, OnTriggerEnter will be sent only to this rigidbody instead of parent's rigidbody")]
        public bool AddRigidbody = false;

        /// <summary> Could have same value as CanvasObjectOffset in FBasic_InteractionAreaCanvas for more precision </summary>
        protected Vector3 toLookPositionOffset = Vector3.zero;

        /// <summary> When you derives from this classes and doing animations in UpdateIn() method, object exits player range and you want still to update until animation finish (for example door hinge animation) </summary>
        protected bool? conditionalExit = null;

        protected Collider triggerArea;
        protected Rigidbody rigidBody;

        public bool Entered { get; protected set; }

        /// <summary> Changes every time when player enters / left trigger, ignoring dependencies </summary>
        public bool EnteredFlag { get; protected set; }

        /// <summary> If we enter few interaction areas, one we can use have this variable setted to true </summary>
        public bool Focused { get; protected set; }

        public Transform EnteredTransform { get; protected set; }

        /// <summary> Used when 'NeedToLookAt' is enabled, helps to define which object is focused the most </summary>
        protected float VisibleFactor;// { get; protected set; }

        protected virtual void Start()
        {
            Entered = false;
            EnteredFlag = false;
            Focused = false;
            VisibleFactor = 1f;
            GetTrigger();

            if (AddRigidbody)
                if (!rigidBody)
                {
                    rigidBody = gameObject.AddComponent<Rigidbody>();
                    rigidBody.isKinematic = true;
                    rigidBody.useGravity = false;
                    transform.localPosition += Vector3.right * 0.0001f; // It's Unity Bug? Or something? Object needs to be moved in any local position offset for trigger collider to work correctly if in parent is another collider which is inside this trigger (even layers are ignored) pretty annoying thing
                }
        }

        protected virtual void OnValidate()
        {
            GetTrigger();
        }

        /// <summary>
        /// Returning sphere collider trigger, creates one if not exists already
        /// </summary>
        protected virtual Collider GetTrigger()
        {
            if (!triggerArea)
            {
                triggerArea = GetComponent<Collider>();

                if (!triggerArea)
                {
                    SphereCollider triggerSphere = gameObject.AddComponent<SphereCollider>();
                    triggerArea = triggerSphere;
                    triggerSphere.radius = 1f;
                    triggerArea.isTrigger = true;

                    gameObject.layer = LayerMask.NameToLayer("IgnoreRaycast");
                }
            }

            return triggerArea;
        }

        /// <summary>
        /// Method called every frame when player is in trigger range
        /// </summary>
        protected virtual void UpdateIn()
        {

        }

        /// <summary>
        /// Coroutine called when player is in trigger range
        /// </summary>
        protected virtual IEnumerator UpdateIfInRange()
        {
            while (true)
            {
                Focused = false;

                if (LockedInteraction == null)
                {
                    // Letting one unique object to be marked as focused
                    if (LookAtRange >= 1f)
                    {
                        if (LastEnteredInteraction == this) Focused = true;
                    }
                    else
                    {
                        // If object's position point is visible in camera view
                        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position + transform.TransformVector(toLookPositionOffset));

                        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
                        {
                            float xFactor = screenPoint.x; xFactor = Mathf.Abs(0.5f - xFactor);
                            float yFactor = screenPoint.y; yFactor = Mathf.Abs(0.5f - yFactor);

                            // Calculating looking at factor
                            VisibleFactor = (xFactor + yFactor);

                            // Checking which entered interaction zone is focused the most
                            if (VisibleFactor < LookAtRange)
                            {
                                if (EnteredInteractions.Count > 1)
                                {
                                    float mostFocused = EnteredInteractions[0].VisibleFactor;
                                    int mostFocusedI = 0;

                                    for (int i = 1; i < EnteredInteractions.Count; i++)
                                    {
                                        if (EnteredInteractions[i].VisibleFactor < mostFocused)
                                        {
                                            mostFocused = EnteredInteractions[i].VisibleFactor;
                                            mostFocusedI = i;
                                        }
                                    }

                                    if (EnteredInteractions[mostFocusedI] == this) Focused = true;
                                }
                                else
                                    Focused = true;
                            }
                        }
                    }
                }
                else // If we lock interaction, for example when we using doors or something, we can focus only on this one object
                {
                    if (LockedInteraction == this) Focused = true;
                }

                // Running update loop when needed
                UpdateIn();

                // Exit conducted by custom script actions
                if (conditionalExit == false)
                {
                    if (EnteredFlag == false)
                    {
                        conditionalExit = null;
                        OnExit();
                        StopAllCoroutines();
                    }
                }

                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                EnteredFlag = true;
            }


            if (Entered) return;

            if (EnteredFlag)
            {
                EnteredTransform = other.transform;
                StartCoroutine(UpdateIfInRange());
                OnEnter();
            }
        }

        protected virtual void OnEnter()
        {
            Entered = true;
            EnteredInteractions.Add(this);
            LastEnteredInteraction = this;
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                EnteredFlag = false;
            }

            if (!Entered) return;

            if (EnteredFlag == false)
            {
                if (conditionalExit != true)
                {
                    if (LockedInteraction != this)
                    {
                        StopAllCoroutines();
                        OnExit();
                    }
                }
            }
        }

        protected virtual void OnExit()
        {
            Entered = false;
            EnteredTransform = null;
            EnteredInteractions.Remove(this);

            if (LastEnteredInteraction == this)
            {
                LastEnteredInteraction = null;
                if (EnteredInteractions.Count > 0) LastEnteredInteraction = EnteredInteractions[EnteredInteractions.Count - 1];
            }
        }

        private void OnDestroy()
        {
            if (LockedInteraction == this) UnlockInteraction();
        }

        public static void UnlockInteraction()
        {
            if (LockedInteraction != null)
            {
                if (!LockedInteraction.Entered)
                {
                    LockedInteraction.StopAllCoroutines();
                    LockedInteraction.OnExit();
                }
            }

            LockedInteraction = null;
        }
    }
}
