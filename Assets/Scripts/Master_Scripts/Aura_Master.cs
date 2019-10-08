using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Master : MonoBehaviour
{
    //system that holds all the aura events
    /// <summary>
    /// Here is the delegate that acts as a container for all our events that we will create...
    /// </summary>
    public delegate void AuraSystem(Globals.EMOTIONS emotion, float transferenceValue);
    public delegate void AuraChecks();
    // ::: EVENTS ::: //

    public event AuraSystem ModifyEmotion;
    public event AuraChecks CheckEmotionalState;

    // ::: CALLS ::: //

    public void EventModifyEmotion(Globals.EMOTIONS emotion, float transferenceValue)
    {
        ModifyEmotion?.Invoke(emotion,transferenceValue);
    }

    public void EventCheckEmotionalState()
    {
        CheckEmotionalState?.Invoke();
    }


}