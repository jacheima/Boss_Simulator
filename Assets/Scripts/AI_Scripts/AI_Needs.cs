using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Needs : MonoBehaviour
{
    public bool gottaPee;
    public NavMeshAgent agent;
    public Transform target;
    public Transform chair;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Bladder");
    }

    void Update()
    {
        if (gottaPee == true)
        {
            agent.SetDestination(target.position);
        }
    }

    IEnumerator Bladder()
    {
        yield return new WaitForSeconds(5);
        gottaPee = true;
        Debug.Log("I gotta pee");
    }

    public void GoBackToWork()
    {
        agent.SetDestination(chair.position);
    }
}
