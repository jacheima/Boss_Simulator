using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    //The GameEvent to be raised when this item is used
    private GameEvent OnItemUsed;

    //Call the event raiser if the NPC enter the trigger
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Employee")
        {
            
            OnItemUsed.Raise();
        }
    }
}
