using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{ 
    //[HideInInspector]
    public AI_Needs needs;
    //[HideInInspector]
    public NavMeshAgent agent;

    public Renderer renderer;
    public Material idleMat;
    public Material workMat;
    public Material needMat;

    public NeedsStates currentNeedState;
    public EmployeeStates currentEmployeeState;
    public float needStateStartTime;
    public float employeeStateStartTime;
    public Transform workstation;



    //----------Employee States----------\\

    public enum EmployeeStates
    {
        Work, Idle, FulfillNeed
    }

    public void ChangeEmployeeState(EmployeeStates newEmployeeState)
    {
        employeeStateStartTime = Time.time;
        Debug.Log("State Start Time = " + employeeStateStartTime);
        currentEmployeeState = newEmployeeState;
    }

    public void Work()
    {
        renderer.material = workMat;
        agent.SetDestination(workstation.position);
        if (agent.remainingDistance <= .2f)
        {
            this.transform.rotation = workstation.rotation;
        }
    }

    public void Idle()
    {
        renderer.material = idleMat;
    }

    public void FulfillNeed()
    {
        renderer.material = needMat;
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
