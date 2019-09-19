using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Transference : MonoBehaviour
{
    #region  ** VARIABLES **
    [SerializeField]
    [Tooltip("This is how often I affect others")]
    public float emotionalTransferenceRate;

    [SerializeField]
    [Tooltip("This is how my emotions are positively affected")]
    private float emotionalGainRate;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    [Tooltip("This is my maximum emotion scale ammount")]
    private float emotionalMax;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    [Tooltip("This is my minimum emotion scale ammount")]
    private float emotionalMin;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    [Tooltip("This is the time in between transferences for me")]
    private float timeToNextTransference;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    [Tooltip("This is the time in between transferences for me")]
    private float internalTransferenceRate;
    #endregion

    public GameObject Aura;
    public Aura_Master auraMaster;
    public Emotions e;
    public float distance;
    public float maximumTransferenceDistance = 3;

    Coroutine cr;

    // Initialize these values on start...

    #region RunTime
    void OnEnable()
    {
        Aura.SetActive(false);
    }

    void OnDisable()
    {
    }

    /// <summary>
    /// This function runs once when we enter a trigger (aura)
    /// </summary>
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag != "Player")
        {
            if (col.gameObject.GetComponent<Aura_Transference>() != null)
            {
                timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(false);
    }

    void FixedUpdate()
    {
        InternalTransferrence(e.emotionState);
    }

    /// <summary>
    /// This function runs as long as were inside the trigger (aura)
    /// </summary>
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            if (col.gameObject.GetComponent<Aura_Transference>() != null)
            {
                //check this distance between this object and the collided object...
                distance = Vector3.Distance(col.gameObject.transform.position, transform.position);
                //if the distance is less or equal to the maximum transference distance of the colided object...
                if (distance <= col.gameObject.GetComponent<Aura_Transference>().maximumTransferenceDistance)
                {
                    //col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(true);
                    //StartCoroutine (ShowAura(col.gameObject.GetComponent<Aura_Transference>()));
                    cr = StartCoroutine(ShowAura(col.gameObject.GetComponent<Aura_Transference>()));
                    //start the timer...
                    timeToNextTransference -= Time.deltaTime;
                    //check if the timer is less or equal to zero
                    if (timeToNextTransference <= 0)
                    {

                        StopCoroutine(cr);
                        //if the timer reached zero, we call the modify emotion functions, 
                        //pass in the state of the other person, and their modifier value...
                        //auraMaster.EventModifyEmotion(col.gameObject.GetComponent<Emotions>().emotionState);
                        ModifyEmotion(col.gameObject.GetComponent<Emotions>().emotionState);
                        //col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(false);
                        //reset the transference rate back to the other objects transference rate (this covers if we stay in their aura)
                        //if we leave the aura and enter a different the above function will be ran, and the rate will be set accordingly...
                        timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
                    }
                }
            }
        }
    }
    #endregion RunTime

    IEnumerator ShowAura(Aura_Transference at)
    {
        at.Aura.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }
    /// <summary>
    /// This function currently handles all our emotion changing
    /// this will likely be changed into an event that is broken up
    /// to improve its function..
    /// </summary>
    public void ModifyEmotion(Globals.EMOTIONS emotion)
    {
        switch (emotion)
        {
            // happiness Scale
            case Globals.EMOTIONS.HAPPY:
                e.happiness += e.happiness / emotionalMax * emotionalGainRate * distance;
                e.sadness = e.sadness > emotionalMin ? -e.sadness / emotionalMax * emotionalGainRate * distance : emotionalMin;
                e.anger = e.anger > emotionalMin ? -e.anger / emotionalMax * emotionalGainRate * distance : emotionalMin;
                e.fear = e.fear > emotionalMin ? -e.fear / emotionalMax * emotionalGainRate * distance : emotionalMin;

                break;

            case Globals.EMOTIONS.SAD:
                e.sadness += e.sadness / emotionalMax * emotionalGainRate * distance;
                e.happiness -= e.happiness / emotionalMax * emotionalGainRate * distance;
                break;
            
            // anger scale
            case Globals.EMOTIONS.ANGER:
                e.anger += e.anger / emotionalMax * emotionalGainRate * distance;
                e.happiness -= e.happiness / emotionalMax * emotionalGainRate * distance;
                if (e.anger >= 50)
                {
                    e.sadness += e.sadness / emotionalMax * emotionalGainRate * distance;
                }
                break;

            case Globals.EMOTIONS.FEAR:
                e.fear += e.fear / emotionalMax * emotionalGainRate * distance;
                e.happiness -= e.happiness / emotionalMax * emotionalGainRate * distance;

                if (e.fear >= 50)
                {
                    e.anger += e.anger / emotionalMax * emotionalGainRate * distance;
                }
                break;

            default:
                break;
        }
        auraMaster.EventCheckEmotionalState();
    }


    public void InternalTransferrence(Globals.EMOTIONS emotion)
    {
        switch (emotion)
        {
            case Globals.EMOTIONS.HAPPY:
                if (e.happiness <= 100)
                {
                    e.happiness += ((e.happiness / emotionalMax) * internalTransferenceRate);
                }
                else
                {
                    e.happiness = emotionalMax;
                }
                break;
            case Globals.EMOTIONS.SAD:
                if (e.sadness <= 100)
                {
                    e.sadness += ((e.sadness / emotionalMax) * internalTransferenceRate);
                }
                else
                {
                    e.sadness = emotionalMax;
                }
                break;
            case Globals.EMOTIONS.ANGER:
                if (e.anger <= 100)
                {
                    e.anger += ((e.anger / emotionalMax) * internalTransferenceRate);
                }
                else
                {
                    e.anger = emotionalMax;
                }
                break;
            case Globals.EMOTIONS.FEAR:
                if (e.fear <= 100)
                {
                    e.fear += ((e.fear / emotionalMax) * internalTransferenceRate);
                }
                else
                {
                    e.fear = emotionalMax;
                }
                break;
        }
        e.CheckEmotionalState();
    }

}
