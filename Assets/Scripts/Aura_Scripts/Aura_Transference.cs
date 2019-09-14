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
    private float emotionalGainStrength;

    [SerializeField]
    [Range(-100, 100)]
    [Tooltip("This is my maximum emotion scale ammount")]
    private int emotionalScaleMax;

    [SerializeField]
    [Range(-100, 100)]
    [Tooltip("This is my minimum emotion scale ammount")]
    private int emotionalScaleMin;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    [Tooltip("This is the time in between transferences for me")]
    private float timeToNextTransference;
    #endregion

    Emotions eScript;
    public GameObject Aura;
    public Aura_Master auraMaster;
    public float distance;
    public float maximumTransferenceDistance = 3;

    // Initialize these values on start...
    void Init()
    {
        eScript = gameObject.GetComponent<Emotions>();
    }

    void OnEnable()
    {
        Init();
        auraMaster.ModifyEmotion += ModifyEmotion;
        Aura.SetActive(false);
    }

    void OnDisable()
    {
        auraMaster.ModifyEmotion -= ModifyEmotion;
    }

    /// <summary>
    /// This function runs once when we enter a trigger (aura)
    /// </summary>
    private void OnTriggerEnter(Collider col)
    {
        timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
    }

    private void OnTriggerExit(Collider col)
    {
        col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(false);
    }

    /// <summary>
    /// This function runs as long as were inside the trigger (aura)
    /// </summary>
    private void OnTriggerStay(Collider col)
    {
        //check this distance between this object and the collided object...
        distance = Vector3.Distance(col.gameObject.transform.position, this.transform.position);
        //if the distance is less or equal to the maximum transference distance of the colided object...
        if (distance <= col.gameObject.GetComponent<Aura_Transference>().maximumTransferenceDistance)
        {
            col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(true);
            //start the timer...
            timeToNextTransference -= Time.deltaTime;
            //check if the timer is less or equal to zero
            if (timeToNextTransference <= 0)
            {
                //if the timer reached zero, we call the modify emotion functions, 
                //pass in the state of the other person, and their modifier value...
                auraMaster.EventModifyEmotion(col.gameObject.GetComponent<Emotions>().emotionState, (emotionalGainStrength / distance));
                col.gameObject.GetComponent<Aura_Transference>().Aura.SetActive(false);
                //reset the transference rate back to the other objects transference rate (this covers if we stay in their aura)
                //if we leave the aura and enter a different the above function will be ran, and the rate will be set accordingly...
                timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
            }
        } 
    }

    /// <summary>
    /// This function currently handles all our emotion changing
    /// this will likely be changed into an event that is broken up
    /// to improve its function..
    /// </summary>
    public void ModifyEmotion(Globals.EMOTIONS emotion, float transferenceValue)
    {
        switch (emotion)
        {
            // happiness Scale
            case Globals.EMOTIONS.HAPPY:
                eScript.happinessScale = Transference(eScript.happinessScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.SAD:
                eScript.happinessScale = Transference(eScript.happinessScale, -(int)transferenceValue);
                break;
            
            // anger scale
            case Globals.EMOTIONS.ANGER:
                eScript.angerScale = Transference(eScript.angerScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.FEAR:
                eScript.angerScale = Transference(eScript.angerScale, -(int)transferenceValue);
                break;

            // energy scale
            case Globals.EMOTIONS.ENERGIZED:
                eScript.energyScale = Transference(eScript.energyScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.TIRED:
                eScript.energyScale = Transference(eScript.energyScale, -(int)transferenceValue);
                break;

            // confidence scale
            case Globals.EMOTIONS.CONFIDENT:
                eScript.confidenceScale = Transference(eScript.confidenceScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.EMBARRASSED:
                eScript.confidenceScale = Transference(eScript.confidenceScale, -(int)transferenceValue);
                break;

            // interest scale
            case Globals.EMOTIONS.SURPRISED:
                eScript.interestScale = Transference(eScript.interestScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.DISAPPOINTMENT:
                eScript.interestScale = Transference(eScript.interestScale, -(int)transferenceValue);
                break;

            // entertainment scale
            case Globals.EMOTIONS.BOREDOM:
                eScript.entertainmentScale = Transference(eScript.entertainmentScale, (int)transferenceValue);
                break;
            case Globals.EMOTIONS.ENTERTAINED:
                eScript.entertainmentScale = Transference(eScript.entertainmentScale, -(int)transferenceValue);
                break;

            default:
                break;
        }
        auraMaster.EventCheckEmotionalState();
    }

    /// <summary>
    /// This function will take in two integer values
    /// one is the current scale were modifying
    /// the second is the value in which were modifying
    /// then returns the modified value.
    /// </summary>
    private int Transference(int scale, int value)
    {
        //Add the value, could be adding a negative, or adding a positive...
        scale += value;
        //check if the scale is over the max...
        scale = (scale > emotionalScaleMax) ? emotionalScaleMax : scale;
        //check if the scale is over the min...
        scale = (scale < emotionalScaleMin) ? emotionalScaleMin : scale;
        return scale;
    }

}
