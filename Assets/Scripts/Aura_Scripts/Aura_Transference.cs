using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Transference : MonoBehaviour
{
    #region  ** VARIABLES **
    [SerializeField]
    [Tooltip("This is how often I affect others")]
    private float emotionalTransferenceRate;

    [SerializeField]
    [Tooltip("This is how my emotions are positively affected")]
    private float emotionalGainStrength;

    [SerializeField]
    [Tooltip("This is how my emotions are negativly affected")]
    private float emotionalLossStrength;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("This is my maximum emotion scale ammount")]
    private int emotionalScaleMax;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("This is my minimum emotion scale ammount")]
    private int emotionalScaleMin;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    [Tooltip("This is the time in between transferences for me")]
    private float timeToNextTransference;
    #endregion

    Emotions eScript;
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
    }

    void OnDisable()
    {

    }

    /// <summary>
    /// This function runs once when we enter a trigger (aura)
    /// </summary>
    private void OnTriggerEnter(Collider col)
    {
        timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
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
            //start the timer...
            timeToNextTransference -= Time.deltaTime;
            //check if the timer is less or equal to zero
            if (timeToNextTransference <= 0)
            {
                //if the timer reached zero, we call the modify emotion functions, 
                //pass in the state of the other person, and their modifier value...
                auraMaster.EventModifyEmotion(col.gameObject.GetComponent<Emotions>().emotionState, (emotionalGainStrength / distance));
                //reset the transference rate back to the other objects transference rate (this covers if we stay in their aura)
                //if we leave the aura and enter a different the above function will be ran, and the rate will be set accordingly...
                timeToNextTransference = col.gameObject.GetComponent<Aura_Transference>().emotionalTransferenceRate;
            }
        }
    }

    //private void OnTriggerExit(Collider col)
    //{
    //    //TODO:: call Aura exit event if needed
    //}

    /// <summary>
    /// This function currently handles all our emotion changing
    /// this will likely be changed into an event that is broken up
    /// to improve its function..
    /// </summary>
    public void ModifyEmotion(Globals.EMOTION emotion, float transferenceValue)
    {
        switch (emotion)
        {
            // anger Scale
            case Globals.EMOTION.ANNOYANCE:
                //if the emotion is annoyance, we call the transference function
                //and pass in the angerscale, and transference value, and then
                //assign the result to the angerscale.
                eScript.angerScale = Transference(eScript.angerScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.ANGER:
                //angerScale = Transference(angerScale, (int)transferenceValue);
                eScript.fearScale = Transference(eScript.fearScale, (int)transferenceValue);
                eScript.joyScale = Transference(eScript.joyScale, -((int)transferenceValue) / 2);
                break;
            case Globals.EMOTION.RAGE:
                eScript.angerScale = Transference(eScript.angerScale, (int)transferenceValue);
                break;

            // fearScale
            case Globals.EMOTION.APPREHENSION:
                eScript.fearScale = Transference(eScript.fearScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.FEAR:
                eScript.fearScale = Transference(eScript.fearScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.TERROR:
                eScript.fearScale = Transference(eScript.fearScale, (int)transferenceValue);
                break;

            // sadnessScale
            case Globals.EMOTION.GRIEF:
                eScript.sadnessScale = Transference(eScript.sadnessScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.SADNESS:
                eScript.sadnessScale = Transference(eScript.sadnessScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.PENSIVENESS:
                eScript.sadnessScale = Transference(eScript.sadnessScale, (int)transferenceValue);
                break;

            // disgustScale
            case Globals.EMOTION.BOREDOM:
                eScript.disgustScale = Transference(eScript.disgustScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.DISGUST:
                eScript.disgustScale = Transference(eScript.disgustScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.LOATHING:
                eScript.disgustScale = Transference(eScript.disgustScale, (int)transferenceValue);
                break;

            // trustScale
            case Globals.EMOTION.ACCEPTANCE:
                eScript.trustScale = Transference(eScript.trustScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.TRUST:
                eScript.trustScale = Transference(eScript.trustScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.ADMIRATION:
                eScript.trustScale = Transference(eScript.trustScale, (int)transferenceValue);
                break;

            // joyScale
            case Globals.EMOTION.SERENITY:
                eScript.joyScale = Transference(eScript.joyScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.JOY:
                eScript.joyScale = Transference(eScript.joyScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.ECSTASY:
                eScript.joyScale = Transference(eScript.joyScale, (int)transferenceValue);
                break;

            // surpriseScale
            case Globals.EMOTION.DISTRACTION:
                eScript.surpriseScale = Transference(eScript.surpriseScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.SURPRISE:
                eScript.surpriseScale = Transference(eScript.surpriseScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.AMAZEMENT:
                eScript.surpriseScale = Transference(eScript.surpriseScale, (int)transferenceValue);
                break;

            // anticipationScale
            case Globals.EMOTION.INTEREST:
                eScript.anticipationScale = Transference(eScript.anticipationScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.ANTICIPATION:
                eScript.anticipationScale = Transference(eScript.anticipationScale, (int)transferenceValue);
                break;
            case Globals.EMOTION.VIGILANCE:
                eScript.anticipationScale = Transference(eScript.anticipationScale, (int)transferenceValue);
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
