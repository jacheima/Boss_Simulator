using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Components")]
    public EmployeeData eData;
    public Transform workStation;

    [Header("Variables")]
    public Transform[] breakSpots;
    public Transform restRoom;

    [Header("FSM Variables")]
    public float stateStartTime;
    public EmployeeStates currentState;

    void Awake()
    {
        eData = GetComponent<EmployeeData>();
    }

    public enum EmployeeStates
    {
        idle, work, slack, restroom
    }

    public void ChangeState(EmployeeStates newState)
    {
        stateStartTime = Time.time;
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
            eData.nav.Stop();
        }
    }

    public void Slack()
    {
        eData.nav.SetDestination(breakSpots[Random.Range(0, breakSpots.Length)].position);
    }

    public void Restroom()
    {
        eData.nav.SetDestination(restRoom.position);
    }

}
