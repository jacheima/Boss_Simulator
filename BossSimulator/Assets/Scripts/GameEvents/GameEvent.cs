using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adds GameEvent as an asset in the asset menu
[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)]

//The game event is a scriptable object so it needs to derive from scriptable object
public class GameEvent : ScriptableObject
{
    //A list of GameEventListeners that will subscribe to a GameEvent
   private  List<GameEventListener> listeners = new List<GameEventListener>();

    //A method to invoke all the subscribers of a game event
    public void Raise()
    {
        //The last GameEventListener to be subscribed will be the first to get invoked (last in, first out)
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            //Invokes each GameEventListener
            listeners[i].OnEventRaised();
        }
    }

    //A method to allow GameEventListeners to subscribe to this GameEvent
    public void RegisterListener(GameEventListener listener)
    {
        //Add the listener to the listeners list
        listeners.Add(listener);
    }

    //A method to allow GameEventListeners to unsubscribe to this GameEvent
    public void UnregisterListener(GameEventListener listener)
    {
        //remove listener from the listeners list
        listeners.Remove(listener);
    }
}
