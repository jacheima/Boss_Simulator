using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Temp_Bathroom : MonoBehaviour
{
    public GameObject employee;

    void Update()
    {
       var colliders = Physics.OverlapSphere(this.gameObject.transform.position, 2);
       foreach (var hit in colliders)
       {
           if (hit.gameObject == employee)
           {
               employee.GetComponent<AI_Needs>().gottaPee = false;
               employee.GetComponent<AI_Needs>().GoBackToWork();
           }
       }
    }
}
