using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public NeedsStates currentState;
    public float stateStartTime;

    public enum NeedsStates
    {
        Fine, Hungry, GottaPee, NoFocus, Lonely, Uncomfortable, Claustrophobic 
    }

    public void ChangeState(NeedsStates newState)
    {
        stateStartTime = Time.time;
        currentState = newState;
    }
}
