using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotions : MonoBehaviour
{
    #region
    /// <summary>
    /// We will use the following scales to add/subtract values from
    /// in order to determine what emotions state the person/object is in
    /// </summary>
    [Header("EMOTIONAL SCALES")]
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

    /// <summary>
    /// the aura layer, 1, 2 or 3. we will likely calculate this using a distance check...
    /// the aura has three layers, therefor, the value of the aura is 1,2 or 3. This
    /// formula calculates the emotional gain/loss per second...
    /// int auraLayerEffect = -75 / 100 * (1 * 3);
    /// </summary>


    //List of emotion scales, we will initialize this list in OnEnable...
    List<int> LEmotions = new List<int>();

    public Aura_Master auraMaster;

    /// <summary>
    /// The current emotional state of the object/person...
    /// this is the highest valued emotional scale...
    /// we will set this in our checks, but each object will have a "Starting" emotional state...
    /// </summary>
    [Header("Current State")]
    public Globals.EMOTION emotionState;
    #endregion


    private void OnEnable()
    {
        auraMaster.CheckEmotionalState += CheckEmotionalState;
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
                emotionState = Globals.EMOTION.ANNOYANCE;
            }
            //if the scale is between 50 and 79 were in the second stage...
            else if (angerScale >= 50 && angerScale <= 79)
            {
                //so we set our state to the second stage...
                emotionState = Globals.EMOTION.ANGER;
            }
            //if the scale is between 80 and 100 (whats left)
            else
            {
                //we set our state the the third and final stage...
                emotionState = Globals.EMOTION.RAGE;
            }
        }

        //The rest of these statements follow the same format as the
        //code block above these lines...
        else if (highestValue == fearScale)
        {
            if (fearScale >= 0 && fearScale <= 49)
            {
                emotionState = Globals.EMOTION.APPREHENSION;
            }
            else if (fearScale >= 50 && fearScale <= 79)
            {
                emotionState = Globals.EMOTION.FEAR;
            }
            else
            {
                emotionState = Globals.EMOTION.TERROR;
            }
        }

        else if (highestValue == sadnessScale)
        {
            if (sadnessScale >= 0 && sadnessScale <= 49)
            {
                emotionState = Globals.EMOTION.GRIEF;
            }
            else if (sadnessScale >= 50 && sadnessScale <= 79)
            {
                emotionState = Globals.EMOTION.SADNESS;
            }
            else
            {
                emotionState = Globals.EMOTION.PENSIVENESS;
            }
        }

        else if (highestValue == disgustScale)
        {
            if (disgustScale >= 0 && disgustScale <= 49)
            {
                emotionState = Globals.EMOTION.BOREDOM;
            }
            else if (disgustScale >= 50 && disgustScale <= 79)
            {
                emotionState = Globals.EMOTION.DISGUST;
            }
            else
            {
                emotionState = Globals.EMOTION.LOATHING;
            }
        }

        else if (highestValue == trustScale)
        {
            if (trustScale >= 0 && trustScale <= 49)
            {
                emotionState = Globals.EMOTION.ACCEPTANCE;
            }
            else if (trustScale >= 50 && trustScale <= 79)
            {
                emotionState = Globals.EMOTION.TRUST;
            }
            else
            {
                emotionState = Globals.EMOTION.ADMIRATION;
            }
        }

        else if (highestValue == joyScale)
        {
            if (joyScale >= 0 && joyScale <= 49)
            {
                emotionState = Globals.EMOTION.SERENITY;
            }
            else if (joyScale >= 50 && joyScale <= 79)
            {
                emotionState = Globals.EMOTION.JOY;
            }
            else
            {
                emotionState = Globals.EMOTION.ECSTASY;
            }
        }

        else if (highestValue == surpriseScale)
        {
            if (surpriseScale >= 0 && surpriseScale <= 49)
            {
                emotionState = Globals.EMOTION.DISTRACTION;
            }
            else if (surpriseScale >= 50 && surpriseScale <= 79)
            {
                emotionState = Globals.EMOTION.SURPRISE;
            }
            else
            {
                emotionState = Globals.EMOTION.AMAZEMENT;
            }
        }

        else if (highestValue == anticipationScale)
        {
            if (anticipationScale >= 0 && anticipationScale <= 49)
            {
                emotionState = Globals.EMOTION.INTEREST;
            }
            else if (anticipationScale >= 50 && anticipationScale <= 79)
            {
                emotionState = Globals.EMOTION.ANTICIPATION;
            }
            else
            {
                emotionState = Globals.EMOTION.VIGILANCE;
            }
        }
    }
}
