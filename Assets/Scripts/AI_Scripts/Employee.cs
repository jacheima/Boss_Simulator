using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : AI_Controller
{
    void Update()
    {
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
            
            case EmployeeStates.FulfillNeed:
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
