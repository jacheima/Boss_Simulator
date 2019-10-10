using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Employee : AI_Controller
{

    public bool isAtDesk;
    public bool isWorking;
    public float currentIncome;
    public float baselineIncome;
    

    //when were using an event system always use OnEnable
    //rather than Start
    void OnEnable()
    {
        needs = gameObject.GetComponent<AI_Needs>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        GameManager.instance.Employees.Add(this);
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (currentEmployeeState == EmployeeStates.Work)
        {
            isWorking = true;
        }
        else
        {
            isWorking = false;
        }

        if (isWorking == true && isAtDesk == true)
        {
            currentIncome = baselineIncome;
        }
        else
        {
            currentIncome = 0;
        }

        switch (currentEmployeeState)
        {
            case EmployeeStates.Work:
                Work();
                if (Time.time >= employeeStateStartTime + 20f)
                {
                    ChangeEmployeeState(EmployeeStates.Idle);
                }
                if (currentNeedState != NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.FulfillNeed);
                }
                break;
            
            case EmployeeStates.Idle:
                Idle();
                if (currentNeedState != NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.FulfillNeed);
                }
                break;
            
            case EmployeeStates.FulfillNeed:
                FulfillNeed();
                if (currentNeedState == NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.Work);
                }
                break;
        }

        switch (currentNeedState)
        {
            case NeedsStates.Fine:
                Fine();
                if (needs.gottaPee == true)
                {
                    ChangeNeedsState(NeedsStates.GottaPee);
                }
                break;

            case NeedsStates.GottaPee:
                GottaPee();
                break;

            case NeedsStates.Peeing:
                Peeing();
                if (needs.gottaPee == false)
                {
                    ChangeNeedsState(NeedsStates.Fine);
                }
                break;
        }
    }
}
