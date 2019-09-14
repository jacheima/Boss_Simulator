using UnityEngine;
using UnityEngine.Events;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Support for events in trigger
    /// </summary>
    public class FBasic_TriggerEvents : MonoBehaviour
    {
        public string EnteringTag = "Player";

        // V1.1.1
        public UnityEvent OnAwakeEvent;
        public UnityEvent OnStartEvent;

        public UnityEvent OnTriggerEnterEvents;
        public UnityEvent OnTriggerExitEvents;


        // V1.1.1
        private void Awake()
        {
            if (OnAwakeEvent != null) OnAwakeEvent.Invoke();
        }

        private void Start()
        {
            if (OnStartEvent != null) OnStartEvent.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ( other.gameObject.tag == EnteringTag )
            {
                if (OnTriggerEnterEvents != null) OnTriggerEnterEvents.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == EnteringTag)
            {
                if (OnTriggerExitEvents != null) OnTriggerExitEvents.Invoke();
            }
        }
    }
}
