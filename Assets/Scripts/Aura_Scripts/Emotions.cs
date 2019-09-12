using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotions : MonoBehaviour
{
    // This enum holds all of our Emotions
    public enum EMOTION
    {
        ANNOYANCE, ANGER, RAGE,             // Anger scale //
        APPREHENSION, FEAR, TERROR,         // Fear scale //
        GRIEF, SADNESS, PENSIVENESS,        // Sadness Scale //
        BOREDOM, DISGUST, LOATHING,         // Disgust Scale //
        ACCEPTANCE, TRUST, ADMIRATION,      // Trust Scale //
        SERENITY, JOY, ECSTASY,             // Joy Scale //
        DISTRACTION, SURPRISE, AMAZEMENT,   // Surprise Scale //
        INTEREST, ANTICIPATION, VIGILANCE   // Anticipation Scale //
    }

    #region
    /// <summary>
    /// We will use the following scales to add/subtract values from
    /// in order to determine what emotions state the person/object is in
    /// </summary>
    // NEGATIVE EMOTIONS //
    [Range(0, 100)]
    public int angerScale;
    [Range(0, 100)]
    public int fearScale;
    [Range(0, 100)]
    public int sadnessScale;
    [Range(0, 100)]
    public int disgustScale;

    // POSITIVE EMOTIONS //
    [Range(0, 100)]
    public int trustScale;
    [Range(0, 100)]
    public int joyScale;
    [Range(0, 100)]
    public int surpriseScale;
    [Range(0, 100)]
    public int anticipationScale;

    // EMOTION CONTROL VALUES //
    [Range(0,10)]
    float emotionalTransferenceRate = 2.0f;

    // TIMERS //
    float timeToNextTransference = 0.0f;

    // the aura layer, 1, 2 or 3. we will likely calculate this using a distance check...

    // the aura has three layers, therefor, the value of the aura is 1,2 or 3. This
    // formula calculates the emotional gain/loss per second...
    //int auraLayerEffect = -75 / 100 * (1 * 3);
    
    public float EmotionModifier = 10.0f;

    //List of emotion scales, we will initialize this list in OnEnable...
    List<int> LEmotions = new List<int>();

    //The current emotional state of the object/person...
    //this is the highest valued emotional scale...
    //we will set this in our checks, but each object will have a "Starting" emotional state...
    [Header("Current State")]
    public EMOTION emotionState;
    #endregion


    /// <summary>
    /// This function runs once when we enter a trigger (aura)
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        //here we are setting the timer to the other persons emotional transference rate...
        timeToNextTransference = col.gameObject.GetComponent<Emotions>().emotionalTransferenceRate;
    }

    /// <summary>
    /// This function runs as long as were inside the trigger (aura)
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerStay(Collider col)
    {
        //now we count down on the timer...
        timeToNextTransference -= Time.deltaTime;
        //check to see if the timer has reached 0...
        if (timeToNextTransference <= 0)
        {
            //if the timer reached zero, we call the modify emotion functions, pass in the state of the other person, and their modifier value...
            ModifyEmotion(col.gameObject.GetComponent<Emotions>().emotionState,col.gameObject.GetComponent<Emotions>().EmotionModifier);
            //reset the transference rate back to the other objects transference rate (this covers if we stay in their aura)
            //if we leave the aura and enter a different the above function will be ran, and the rate will be set accordingly...
            timeToNextTransference = col.gameObject.GetComponent<Emotions>().emotionalTransferenceRate;
        }
    }


    /// <summary>
    /// This function currently handles all our emotion changing
    /// this will likely be changed into an event that is broken up
    /// to improve its function..
    /// </summary>
    /// <param name="e"></param>
    /// <param name="transferenceValue"></param>
    public void ModifyEmotion(EMOTION e, float transferenceValue)
    {
        //check which emotion the other person is transfering...
        switch (e)
        {
            // anger Scale
            case EMOTION.ANNOYANCE:
                //if the emotion is annoyance, we call the transference function
                //and pass in the angerscale, and transference value, and then
                //assign the result to the angerscale.
                angerScale = Transference(angerScale, (int)transferenceValue);
                break;
            case EMOTION.ANGER:
                angerScale = Transference(angerScale, (int)transferenceValue);
                break;
            case EMOTION.RAGE:
                angerScale = Transference(angerScale, (int)transferenceValue);
                break;

            // fearScale
            case EMOTION.APPREHENSION:
                fearScale = Transference(fearScale, (int)transferenceValue);
                break;
            case EMOTION.FEAR:
                fearScale = Transference(fearScale, (int)transferenceValue);
                break;
            case EMOTION.TERROR:
                fearScale = Transference(fearScale, (int)transferenceValue);
                break;

            // sadnessScale
            case EMOTION.GRIEF:
                sadnessScale = Transference(sadnessScale, (int)transferenceValue);
                break;
            case EMOTION.SADNESS:
                sadnessScale = Transference(sadnessScale, (int)transferenceValue);
                break;
            case EMOTION.PENSIVENESS:
                sadnessScale = Transference(sadnessScale, (int)transferenceValue);
                break;

            // disgustScale
            case EMOTION.BOREDOM:
                disgustScale = Transference(disgustScale, (int)transferenceValue);
                break;
            case EMOTION.DISGUST:
                disgustScale = Transference(disgustScale, (int)transferenceValue);
                break;
            case EMOTION.LOATHING:
                disgustScale = Transference(disgustScale, (int)transferenceValue);
                break;

            // trustScale
            case EMOTION.ACCEPTANCE:
                trustScale = Transference(trustScale, (int)transferenceValue);
                break;
            case EMOTION.TRUST:
                trustScale = Transference(trustScale, (int)transferenceValue);
                break;
            case EMOTION.ADMIRATION:
                trustScale = Transference(trustScale, (int)transferenceValue);
                break;

            // joyScale
            case EMOTION.SERENITY:
                joyScale = Transference(joyScale, (int)transferenceValue);
                break;
            case EMOTION.JOY:
                joyScale = Transference(joyScale, (int)transferenceValue);
                break;
            case EMOTION.ECSTASY:
                joyScale = Transference(joyScale, (int)transferenceValue);
                break;

            // surpriseScale
            case EMOTION.DISTRACTION:
                surpriseScale = Transference(surpriseScale, (int)transferenceValue);
                break;
            case EMOTION.SURPRISE:
                surpriseScale = Transference(surpriseScale, (int)transferenceValue);
                break;
            case EMOTION.AMAZEMENT:
                surpriseScale = Transference(surpriseScale, (int)transferenceValue);
                break;

            // anticipationScale
            case EMOTION.INTEREST:
                anticipationScale = Transference(anticipationScale, (int)transferenceValue);
                break;
            case EMOTION.ANTICIPATION:
                anticipationScale = Transference(anticipationScale, (int)transferenceValue);
                break;
            case EMOTION.VIGILANCE:
                anticipationScale = Transference(anticipationScale, (int)transferenceValue);
                break;
            default:
                break;
        }
        //call the check emotional state function...
        CheckEmotionalState();
    }

    /// <summary>
    /// This function will take in two integer values
    /// one is the current scale were modifying
    /// the second is the value in which were modifying
    /// then returns the modified value.
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private int Transference(int scale, int value)
    {
        //this sets scale to scale plus the value
        //and checks if scale has reached 100 or greater
        //if it did reach 100+ then we set it to 100.
        scale = scale > 100 ? 100 : scale += value;
        //this does the same as above but checks for 0 or
        //less and then sets accordingly...
        scale = scale <= 0 ? 0 : scale += value;
        return scale;
    }

    /// <summary>
    /// This function is lengthy, and will almost certainly
    /// be changed. this is to prove the concept and get things running.
    /// </summary>
    private void CheckEmotionalState()
    {
        //clear the list first
        LEmotions.Clear();
        //add all the emotions to the list, so we can check their values quickly...
        LEmotions.Add(angerScale);
        LEmotions.Add(fearScale);
        LEmotions.Add(sadnessScale);
        LEmotions.Add(disgustScale);
        LEmotions.Add(trustScale);
        LEmotions.Add(joyScale);
        LEmotions.Add(surpriseScale);
        LEmotions.Add(anticipationScale);

        //intial value for checking which emotion is the highest...
        int highestValue = 0;

        //loops through the list of values, and returns the highest value...
        foreach (int v in LEmotions)
        {
            if (highestValue <= v)
            {
                highestValue = v;
            }
        }

        //the following nested if statements find out
        //which emotion was the highest value we found above
        //a switch statement won't work here because the scales
        //are not static. but this section will need to be
        //improved upon, and preferably simplified...
        if (highestValue == angerScale)
        {
            //if the scale is between 0 and 49 were in the first stage
            if (angerScale >= 0 && angerScale <= 49)
            {
                //so we set our state to the first stage...
                emotionState = EMOTION.ANNOYANCE;
            }
            //if the scale is between 50 and 79 were in the second stage...
            else if (angerScale >= 50 && angerScale <= 79)
            {
                //so we set our state to the second stage...
                emotionState = EMOTION.ANGER;
            }
            //if the scale is between 80 and 100 (whats left)
            else
            {
                //we set our state the the third and final stage...
                emotionState = EMOTION.RAGE;
            }
        }

        //The rest of these statements follow the same format as the
        //code block above these lines...
        else if (highestValue == fearScale)
        {
            if (fearScale >= 0 && fearScale <= 49)
            {
                emotionState = EMOTION.APPREHENSION;
            }
            else if (fearScale >= 50 && fearScale <= 79)
            {
                emotionState = EMOTION.FEAR;
            }
            else
            {
                emotionState = EMOTION.TERROR;
            }
        }

        else if (highestValue == sadnessScale)
        {
            if (sadnessScale >= 0 && sadnessScale <= 49)
            {
                emotionState = EMOTION.GRIEF;
            }
            else if (sadnessScale >= 50 && sadnessScale <= 79)
            {
                emotionState = EMOTION.SADNESS;
            }
            else
            {
                emotionState = EMOTION.PENSIVENESS;
            }
        }

        else if (highestValue == disgustScale)
        {
            if (disgustScale >= 0 && disgustScale <= 49)
            {
                emotionState = EMOTION.BOREDOM;
            }
            else if (disgustScale >= 50 && disgustScale <= 79)
            {
                emotionState = EMOTION.DISGUST;
            }
            else
            {
                emotionState = EMOTION.LOATHING;
            }
        }

        else if (highestValue == trustScale)
        {
            if (trustScale >= 0 && trustScale <= 49)
            {
                emotionState = EMOTION.ACCEPTANCE;
            }
            else if (trustScale >= 50 && trustScale <= 79)
            {
                emotionState = EMOTION.TRUST;
            }
            else
            {
                emotionState = EMOTION.ADMIRATION;
            }
        }

        else if (highestValue == joyScale)
        {
            if (joyScale >= 0 && joyScale <= 49)
            {
                emotionState = EMOTION.SERENITY;
            }
            else if (joyScale >= 50 && joyScale <= 79)
            {
                emotionState = EMOTION.JOY;
            }
            else
            {
                emotionState = EMOTION.ECSTASY;
            }
        }

        else if (highestValue == surpriseScale)
        {
            if (surpriseScale >= 0 && surpriseScale <= 49)
            {
                emotionState = EMOTION.DISTRACTION;
            }
            else if (surpriseScale >= 50 && surpriseScale <= 79)
            {
                emotionState = EMOTION.SURPRISE;
            }
            else
            {
                emotionState = EMOTION.AMAZEMENT;
            }
        }

        else if (highestValue == anticipationScale)
        {
            if (anticipationScale >= 0 && anticipationScale <= 49)
            {
                emotionState = EMOTION.INTEREST;
            }
            else if (anticipationScale >= 50 && anticipationScale <= 79)
            {
                emotionState = EMOTION.ANTICIPATION;
            }
            else
            {
                emotionState = EMOTION.VIGILANCE;
            }
        }
    }
}
