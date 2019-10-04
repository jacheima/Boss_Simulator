using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Needs : MonoBehaviour
{
    public bool gottaPee;
    public NavMeshAgent agent;

    public float bladder, hunger, focus, social, comfort, room;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        bladder = 0;
    }

    void Update()
    {
        StartCoroutine("EmptyBladder");


        if (bladder <= 10)
        {
            gottaPee = true;
        }
        else
        {
            gottaPee = false;
        }
    }

    IEnumerator EmptyBladder()
    {
        yield return new WaitForSeconds(180);
        bladder = 0;
        Debug.Log("I gotta pee");
    }

    public void GoBackToWork()
    {
        
    }
}
