using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour
{
    [Header("Components")]
    public EmployeeData eData;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Employee")
        {
            Debug.Log("Carol has entered her workstation");
            eData.tf.position = new Vector3(-1.592f, 1.549f, 4.231f);
            eData.tf.eulerAngles = new Vector3(-20.935f, -142.818f, 0);
        }
    }
}
