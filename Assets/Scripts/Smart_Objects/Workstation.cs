using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workstation : MonoBehaviour
{
    public GameObject employee;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has entered the workstation");
        if (other.gameObject == employee)
        {
            employee.GetComponent<Employee>().isAtDesk = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " has exited the workstation");
        if (other.gameObject == employee)
        {
            employee.GetComponent<Employee>().isAtDesk = false;
        }
    }
}
