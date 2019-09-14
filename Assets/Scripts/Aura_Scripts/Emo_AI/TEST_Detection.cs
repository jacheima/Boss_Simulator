using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Detection : MonoBehaviour
{
    [Range(0, 100)]
    public float fear;

    [Range(0, 100)]
    public float anger;

    public enum EMOTIONAL_STATES
    {
        Anger, Fear
    }


    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 5f);

        for (int i = 0; i < hits.Length; i++)
        {
            Transform target = hits[i].transform;

            if (hits != null)
            {
                if (hits[i].transform.root != transform)
                {
                    if(fear > anger)
                    {
                        
                    }

                    if (anger > fear)
                    {

                    }

                    
                }
            }
        }
    }
}
