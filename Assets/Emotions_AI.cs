using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotions_AI : MonoBehaviour
{ 

    [Range(0, 100)]
    public float Anger;
    private float angerMin = 0;
    private float angerMax = 100;


    [Range(0, 100)]
    public float Fear;
    private float fearMin = 0;
    private float fearMax = 100;


    //Happy
    //Sad

    //Confident
    //Embarrassed

    //tired
    //energetic

    //disappointed
    //surprised

    //bored
    //entertained

    public float Transference;

    public float AuraLayer;

    public EMOTIONAL_STATE CurrentEmotionalState;

    public enum EMOTIONAL_STATE
    {
        Anger, Fear
    }


    void Update()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, 5f);
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log(hit[i].name);
        }
        

        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                EMOTIONAL_STATE incomingState = hit[i].gameObject.GetComponent<Emotions_AI>().CurrentEmotionalState;
                Debug.Log(incomingState);

                if (Vector3.Distance(transform.position, hit[i].gameObject.transform.position) <= 1)
                {
                    AuraLayer = 1;

                }

                if (Vector3.Distance(transform.position, hit[i].gameObject.transform.position) >= 1 && Vector3.Distance(transform.position, hit[i].gameObject.transform.position) <= 3)
                {
                     AuraLayer = 2;

                }

                if (Vector3.Distance(transform.position, hit[i].gameObject.transform.position) >= 3 && Vector3.Distance(transform.position, hit[i].gameObject.transform.position) <= 5)
                {
                    AuraLayer = 3;

                }

                switch (incomingState)
                {
                    case EMOTIONAL_STATE.Anger:
                        //if the incoming emotion is anger, we want to increase fear in ourselves
                        AddFear(hit[i].gameObject.GetComponent<Emotions_AI>().Anger, AuraLayer);
                        Debug.Log("Adding Fear");
                        break;
                    case EMOTIONAL_STATE.Fear:
                        //if the incoming emotion is fear, we want to increase anger in ourselves
                        AddAnger();

                        break;
                }

            }
        }

        if (Anger > Fear)
        {
            CurrentEmotionalState = EMOTIONAL_STATE.Anger;
        }

        if (Fear > Anger)
        {
            CurrentEmotionalState = EMOTIONAL_STATE.Fear;
        }
    }

    void AddFear(float incomingAnger, float distance)
    {
        
        float increaseFearPerSec = TransferenceRate(incomingAnger, angerMax, Transference, distance);
        Debug.Log("increaseFearPerSec:" + increaseFearPerSec);

        StartCoroutine(increaseFear(increaseFearPerSec));

        //if our fear is greater than or = to fifty, then add incoming state to anger
        if (Fear >= 50)
        { 
            StartCoroutine(increaseAnger(increaseFearPerSec));
        }

        if (Fear >= fearMax)
        {
            //explode outward
            //reduce fear 75% of the value of 10s
            StartCoroutine(reduceFear());

            if (Fear <= 25f)
            {
                StopCoroutine(reduceFear());
            }

        }
    }

    IEnumerator increaseFear(float value)
    {
        Fear += value;
        Debug.Log("CurrentFearValue:" + Fear);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator reduceFear()
    {
        if (Fear >= 25f)
        {
            Fear -= (Fear * .75f)/10;
        }
        yield return new WaitForSeconds(1f);
    }

    void AddAnger()
    {
        //if our anger is >= 50, then add incoming state to fear

        //if our fear == max, then reduce current emotional state by 75%/10s
    }

    IEnumerator increaseAnger(float value)
    {
        yield return new WaitForSeconds(1f);
    }

    float TransferenceRate(float c, float x, float t, float a)
    {
        // (c/|x|) * ta
        float deltaEmotion = (c / Mathf.Abs(x)) * (t * a);

        return deltaEmotion;

    }

}
