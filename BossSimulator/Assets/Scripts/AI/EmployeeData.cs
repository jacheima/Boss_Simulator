using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeData : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent nav;
    public GameObject employee;
    public Transform tf;
    public AIController AIController;

    [Header("Variables")]
    public string name;


    [Header("Needs")]
    public float happiness;
    public float productivity;
    public float social;

    [Header("Traits")]
    public string[] traits;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        tf = GetComponent<Transform>();
        employee = this.gameObject;
        AIController = GetComponent<EmployeeController>();
    }
}
