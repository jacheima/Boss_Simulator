using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public AI_Needs needs;
    public NavMeshAgent agent;

    public NeedsStates currentNeedState;
    public EmployeeStates currentEmployeeState;
    public float needStateStartTime;
    public float employeeStateStartTime;
    public Transform workstation;

    void Start()
    {
        needs = gameObject.GetComponent<AI_Needs>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    //----------Employee States----------\\

    public enum EmployeeStates
    {
        Work, Idle, FulfillNeed
    }

    public void ChangeEmployeeState(EmployeeStates newEmployeeState)
    {
        employeeStateStartTime = Time.time;
        currentEmployeeState = newEmployeeState;
    }

    public void Work()
    {
        agent.SetDestination(workstation.position);
        if (agent.remainingDistance <= .2f)
        {
            this.transform.rotation = workstation.rotation;
        }
    }

    //----------Needs States----------\\

    public enum NeedsStates
    {
        Fine, Hungry, GottaPee, Peeing, NoFocus, Lonely, Uncomfortable, Claustrophobic 
    }

    public void ChangeNeedsState(NeedsStates newNeedState)
    {
        needStateStartTime = Time.time;
        currentNeedState = newNeedState;
    }

    public void Fine()
    {
        //Todo: Go to work
    }

    public void Hungry()
    {
        //Todo: Find Food
    }

    public void GottaPee()
    {
        foreach (var source in GameManager.instance.SmartObjects)
        {
            if (source.gameObject.name == "Male Bathroom")
            {
                agent.SetDestination(source.transform.position);
            }
        }
    }

    public void Peeing()
    {
        if (Time.time >= needStateStartTime + 30f)
        {
            needs.bladder = 100;
        }
    }

    public void NoFocus()
    {
        //Todo: Get Focus
    }

    public void Lonely()
    {
        //Todo: Talk to Someone
    }

    public void Uncomfortable()
    {
        //Todo: Find something comfortable
    }

    public void Claustrophobic()
    {
        //Todo: Enter a better room
    }
}
