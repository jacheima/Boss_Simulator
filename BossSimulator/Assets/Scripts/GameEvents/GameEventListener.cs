using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Required to use the unity event class
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    //The GameEvent this GameEventListener will subscribe to
    private GameEvent gameEvent;

    [SerializeField]
    //The UnityEvent response that will be invoked when the GameEvent raises this GameEventListener
    private UnityEvent response;

    //Register the GameEvent to the GameEventListener when this GameObject is enabled
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    //Unregister the GameEvent from the GameEventListener when this GameObject is disabled
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    //Called when a GameEvent is raised causing the GameEventListener to invoke the UnityEvent
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
