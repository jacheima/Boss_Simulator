using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public GameObject employee;
    public Transform chair;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(chair.position, 0.5f);

        foreach (var hit in hits)
        {
            if (hit.gameObject == employee)
            {
                employee.GetComponent<Employee>().isAtDesk = true;
            }
        }
    }
}
