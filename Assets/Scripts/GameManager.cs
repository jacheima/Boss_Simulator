using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Smart Objects")]
    public List<GameObject> SmartObjects;

    [Header("Employees")]
    public List<Employee> Employees = new List<Employee>();

    [Header("Budget Variables")]
    public float totalIncome = 100f;
    public float winCondition = 10000f;
    public float incomeValue = 0f;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeRepeating("GenerateIncome", 0f, 1f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("Cursor Lock State is " + Cursor.lockState);
        }

        foreach (var employee in Employees)
        {
            incomeValue =+ employee.currentIncome;
        }

        //check to see if we've met our win condition
       /* if (totalIncome < winCondition && employee.GetComponent<Employee>().isWorking)
        {
            GenerateIncome();
        }
        else
        {
            Debug.Log("You Have Maximized your profit margin, Congratulations!");
        } */
    }
    public void GenerateIncome()
    {
            totalIncome += incomeValue;
            Debug.Log("Current Money: " + totalIncome);
        }
}
