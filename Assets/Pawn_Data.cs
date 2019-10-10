using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Data : MonoBehaviour
{
    public float pee = 25;
    public float social = 10;
    public float eat;


    void Update()
    {
        pee += 1 * Time.deltaTime;
        social += 1 * Time.deltaTime;
        eat += 1 * Time.deltaTime;

        Mathf.Clamp(pee, 1f, 100f);
        Mathf.Clamp(social, 1f, 100f);
        Mathf.Clamp(eat, 1f, 100f);

        if (pee >= 100f)
        {
            pee = 0;
        }

        if (social >= 100f)
        {
            social = 0;
        }

        if (eat >= 100f)
        {
            eat = 0;
        }
    }
}
