using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateNPC : NeedsManager
{
    //reference to the employee data script
    public EmployeeData employeeData;

    void Start()
    {
        employeeData = GetComponent<EmployeeData>();
    }
    public void UpdateNeeds(ItemData itemData)
    {

    }
}
