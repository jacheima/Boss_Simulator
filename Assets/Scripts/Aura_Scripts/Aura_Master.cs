using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Aura_Master : object
{

    //system that holds all the aura events
    /// <summary>
    /// Here is the delegate that acts as a container for all our events that we will create...
    /// </summary>
    public delegate void AuraSystem();

    /// <summary>
    /// here we are nesting the delegates, so we can utilize AuraSystem to hold all our...
    /// AuraModify events...
    /// </summary>
    /// <param name="value"></param>
    /// <param name="modifier"></param>
    /// <returns></returns>
    public delegate AuraSystem AuraModify(float value, float modifier);


    // ::: EVENTS ::: //

    private static event AuraSystem CheckEmotion;

    private static event AuraModify IncreaseAnger;
    private static event AuraModify IncreaseHappyness;
    private static event AuraModify IncreaseFear;
    private static event AuraModify IncreaseSadness;


    // ::: CALLS ::: //

    public static void EventCheckEmotion()
    {
        CheckEmotion();
    }

    public static void EventIncreaseAnger(float value, float modifier)
    {
        IncreaseAnger(value, modifier);
    }

    public static void EventIncreaseHappyness(float value, float modifier)
    {
        IncreaseHappyness(value, modifier);
    }

    public static void EventIncreaseFear(float value, float modifier)
    {
        IncreaseFear(value, modifier);
    }

    public static void EventIncreaseSadness(float value, float modifier)
    {
        IncreaseSadness(value, modifier);
    }

}