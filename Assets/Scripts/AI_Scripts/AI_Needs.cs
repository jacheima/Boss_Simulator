using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AI_Needs : MonoBehaviour
{
    public bool gottaPee;
    public NavMeshAgent agent;

    [Range(0,100)]
    public float bladder, hunger, focus, social, comfort, room;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        bladder = Random.Range(30,80);
    }

    void Update()
    {
        bladder -= 1 * Time.deltaTime;


        if (bladder <= 10)
        {
            gottaPee = true;
        }
        else
        {
            gottaPee = false;
        }
    }

    public void GoBackToWork()
    {
        
    }
}
