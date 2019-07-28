using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Components")]
    public EmployeeData eData;
    public Transform workStation;

    [Header("Variables")]
    // public Transform[] breakSpots;
    public Transform breakSpot;
    public Transform restRoom;
    public int breakSpotIndex;

    [Header("FSM Variables")]
    public float stateStartTime;
    public EmployeeStates currentState;

    void Awake()
    {
        eData = GetComponent<EmployeeData>();
        //breakSpotIndex = Random.Range(0, breakSpots.Length);
    }

    public enum EmployeeStates
    {
        idle, work, slack, restroom
    }

    public void ChangeState(EmployeeStates newState)
    {
        stateStartTime = Time.time;
        eData.nav.isStopped = false;
        currentState = newState;
    }

    public void Idle()
    {
        //Do nothing
    }

    public void Work()
    {
        eData.nav.SetDestination(workStation.position);

        if (!eData.nav.pathPending && eData.nav.remainingDistance <= 1f)
        {
            eData.nav.isStopped = true;
        }
    }

    public void Slack()
    {
        eData.nav.SetDestination(breakSpot.position);

        if (!eData.nav.pathPending && eData.nav.remainingDistance <= 1f)
        {
            stateStartTime = Time.time;
            eData.nav.isStopped = true;
            eData.tf.rotation = breakSpot.rotation;
            eData.onBreak = true;
        }
    }

    public void Restroom()
    {
        eData.nav.SetDestination(restRoom.position);

        if (!eData.nav.pathPending && eData.nav.remainingDistance <= 1f && eData.usingRestroom == false)
        {
            stateStartTime = Time.time;
            eData.usingRestroom = true;
        }

        if (Time.time >= stateStartTime + 15f && eData.usingRestroom == true)
        {
            eData.bladder = 100;
            eData.usingRestroom = false;
        }
    }

}
