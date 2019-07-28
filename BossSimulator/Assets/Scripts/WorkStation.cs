using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour
{
    [Header("Components")]
    public EmployeeData eData;
    public string emplyoee;
    public Transform tf;

    void Awake()
    {
        tf = GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Employee" && other.transform.GetComponent<EmployeeData>().employeeName == emplyoee )
        {
            Debug.Log( emplyoee + " has entered her workstation");
            eData.tf.position = tf.position;
            eData.tf.rotation = tf.rotation;
        }
    }
}
