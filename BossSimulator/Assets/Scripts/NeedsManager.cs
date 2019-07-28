using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsManager : MonoBehaviour
{
    [Header("Components")]
    public EmployeeData data;

    public GameObject employee;

    [Header("Needs")] public int happiness;
    public int productivity;
    public int social;
    public int bladder;
    public int stress;

    void Start()
    {
        data = employee.GetComponent<EmployeeData>();
    }

    void Update()
    {

    }

    void AddNeed()
    {

    }

    void DecayNeed()
    {

    }

}




//set and maintain the needs for each employee