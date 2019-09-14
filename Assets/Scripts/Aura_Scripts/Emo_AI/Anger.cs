using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Anger : MonoBehaviour
{

    public float anger;
    private float min = 0f;
    private float max = 100f;

    public float emotionalTransference;

    void Start()
    {

    }

    void Update()
    {
        if (anger > max)
        {
            anger = max;
        }

        if (anger < min)
        {
            anger = min;
        }


        Collider[] hit = Physics.OverlapSphere(transform.position, 5f);

        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("Get the current emotional state of the person in the sphere");
        }

    }

    public void Rage()
    {

    }





}
