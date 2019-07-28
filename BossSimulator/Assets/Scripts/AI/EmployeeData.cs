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
    public string employeeName;
    public bool usingRestroom;
    public bool onBreak;
    public float workDone;
    public bool isWorking;


    [Header("Needs")]
    public float happiness = 100;
    public float productivity;
    public float social;
    public float bladder;
    public float stress;

    [Header("Traits")]
    public string[] traits;

    [Header("Deays")]
    public float bladderDecay = .3f;
    public float hapinessDecay = .1f;
    public float socialDeacy = .5f;
    

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        tf = GetComponent<Transform>();
        employee = this.gameObject;
        AIController = GetComponent<EmployeeController>();

        social = Random.Range(35, 100);
        bladder = Random.Range(35, 100);
    }

    void Update()
    {
        if (isWorking == true)
        {
            workDone += (productivity / 150) * Time.deltaTime;
        }
    }
}
