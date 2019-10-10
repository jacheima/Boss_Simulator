using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Temp_Bathroom : MonoBehaviour
{
    public Employee employee;

    void Start()
    {
        GameManager.instance.SmartObjects.Add(gameObject);
    }

    void Update()
    {
       var colliders = Physics.OverlapSphere(this.gameObject.transform.position, 0.5f);
       foreach (var hit in colliders)
       {
           if (hit.gameObject.tag == "Employee" && hit.gameObject.GetComponent<Employee>().currentNeedState == AI_Controller.NeedsStates.GottaPee)
           {
               employee = hit.gameObject.GetComponent<Employee>();
               employee.ChangeNeedsState(AI_Controller.NeedsStates.Peeing);
           }
       }
    }
}
