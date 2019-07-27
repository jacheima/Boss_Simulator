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
                    break;
        }
    }
}
