using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsManager : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector]
    public EmployeeData eData;

    public GameObject employee;

    void Start()
    {
        eData = employee.GetComponent<EmployeeData>();
    }

    void Update()
    {
        //Decay the bladder over time (Over time they will have to go to the bathroom)
        eData.bladder -= eData.bladderDecay * Time.deltaTime;

        //Set Productivity, as it changes based on the other stats
        eData.productivity = eData.happiness + eData.bladder + eData.social + eData.stress;

        //Decay Happiness over time
        eData.happiness -= eData.hapinessDecay * Time.deltaTime;

        //if the NPC is on break
        if (eData.onBreak)
        {
            AddSocial(eData.socialDeacy);
        }
        else
        {
            //Decay Social Overtime
            eData.social -= eData.socialDeacy * Time.deltaTime;
        }
    }

    public void AddHappiness(float happinessEffect)
    {
        eData.happiness += happinessEffect * Time.deltaTime;
    }
    public void AddProductivity(float productivityEffect)
    {
        eData.productivity += productivityEffect * Time.deltaTime;
    }

    public void AddSocial(float socialEffect)
    {
        
        eData.social += socialEffect * Time.deltaTime;

    }

    public void AddStress()
    {

    }

}




//set and maintain the needs for each employee