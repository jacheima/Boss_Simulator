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
    public bool usedRestrom;
    public bool onBreak;
    public float workDone;
    public bool isWorking;


    [Header("Needs")]
    public float happiness;
    public float productivity;
    public float social;
    public float bladder;

    [Header("Traits")]
    public string[] traits;

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
        bladder -= 0.8f * Time.deltaTime;
        productivity = happiness + social + bladder;

        if (onBreak == true)
        {
            social += +3f * Time.deltaTime;
        }
        else
        {
            social -= 0.8f * Time.deltaTime;
        }

        if (isWorking == true)
        {
            workDone += (productivity / 150) * Time.deltaTime;
        }
    }
}
