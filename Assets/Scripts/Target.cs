using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public NavMeshAgent Nav_Mesh;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
        Nav_Mesh = GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Nav_Mesh.SetDestination(target.transform.position);

    }
}
