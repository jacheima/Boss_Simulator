using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public Item item;
    public SphereCollider sphereCollider;
    public List<GameObject> objectsInTrigger;

    void Update()
    {
        if (objectsInTrigger.Count > 2f)
        {
            foreach (GameObject item in objectsInTrigger)
            {
                
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Emplyoee" && other.transform.GetComponent<EmployeeData>().AIController.currentState == AIController.EmployeeStates.slack)
        {
            objectsInTrigger.Add(other.gameObject);
        }
    }
}
