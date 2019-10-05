using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float totalIncome = 0f;
    public float winCondition = 10000f;
    public float incomeValue = 100f;

    public List<GameObject> employeeDesks;

    public GameObject employee;

    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        employeeDesks.Add(GameObject.Find("Desk"));
    }
    void Update()
    {
        if (employeeDesks != null)
        {

        }

        //check to see if we've met our win condition
        if (totalIncome < winCondition && employee.GetComponent<AI_Controller>().isAtDesk)
        {
            GenerateIncome();
        }
        else
        {
            Debug.Log("You Have Maximized your profit margin, Congratulations!");
        }
    }
    public void GenerateIncome()
    {
      
            totalIncome += incomeValue * Time.deltaTime;
            Debug.Log("Current Profit: " + totalIncome);

    }
}
