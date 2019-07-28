using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeController : AIController
{
    void Update()
    {
        switch (currentState)
        {
            case EmployeeStates.idle:
                Idle();
                if (Time.time > stateStartTime + 2f)
                {
                    ChangeState(EmployeeStates.work);
                }
                    break;

            case EmployeeStates.work:
                Work();
                if (eData.social <= 25f)
                {
                    ChangeState(EmployeeStates.slack);
                } 
                if (eData.bladder <= 25f)
                {
                    ChangeState(EmployeeStates.restroom);
                }
                   break;

            case EmployeeStates.slack:
                Slack();
                if (eData.social >= 98f)
                {
                    ChangeState(EmployeeStates.work);
                    eData.onBreak = false;
                    //breakSpotIndex = Random.Range(0, breakSpots.Length);
                }
                    break;

            case EmployeeStates.restroom:
                Restroom();
                if (eData.bladder >= 90f && eData.social >= 50f)
                {
                    ChangeState(EmployeeStates.work);
                }
                if (eData.bladder >= 90f && eData.social <= 50f)
                {
                    ChangeState(EmployeeStates.slack);
                }
                    break;
        }

        
    }

}
