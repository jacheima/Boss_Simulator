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
    [Range(-100, 100)]
    [Tooltip("+ = Happy, - = Sad")]
    public int happinessScale;
    [Range(-100, 100)]
    [Tooltip("+ = Anger, - = Fear")]
    public int angerScale;
    [Range(-100, 100)]
    [Tooltip("+ = Energized, - = Tired")]
    public int energyScale;
    [Range(-100, 100)]
    [Tooltip("+ = Confident, - = Embarrassed")]
    public int confidenceScale;
    [Range(-100, 100)]
    [Tooltip("+ = Surprised, - = Dissapointed")]
    public int interestScale;
    [Range(-100, 100)]
    [Tooltip("+ = Entertained, - = Bored")]
    public int entertainmentScale;

    //List of emotion scales, we will initialize this list in OnEnable...
    private List<int> LEmotions = new List<int>();

    [SerializeField]
    private Aura_Master auraMaster;
    [SerializeField]
    private Color_Master colorMaster;

    /// <summary>
    /// The current emotional state of the object/person...
    /// this is the highest valued emotional scale...
    /// we will set this in our checks, but each object will have a "Starting" emotional state...
    /// </summary>
    [Header("CURRENT EMOTIONAL STATE")]
    public Globals.EMOTIONS emotionState;
    #endregion

    private void OnEnable()
    {
        auraMaster.CheckEmotionalState += CheckEmotionalState;
        auraMaster.EventCheckEmotionalState();
    }

    private void OnDisable()
    {
        auraMaster.CheckEmotionalState -= CheckEmotionalState;
    }

    /// <summary>
    /// This function is lengthy, and will almost certainly
    /// be changed. this is to prove the concept and get things running.
    /// </summary>
    private void CheckEmotionalState()
    {
        int v = GetDominateEmotion();
        //check if the highest value equals one of your emotions, if it does, then we check
        //to see if the scale is greater than zero, if it is, then we set the emotion respectfully...
        if (v == happinessScale)
        {
            emotionState = happinessScale >= 0 ? Globals.EMOTIONS.HAPPY : Globals.EMOTIONS.SAD;
        }
        else if (v == angerScale)
        {
            emotionState = angerScale >= 0 ? Globals.EMOTIONS.ANGER : Globals.EMOTIONS.FEAR;
        }

        else if (v == energyScale)
        {
            emotionState = energyScale >= 0 ? Globals.EMOTIONS.ENERGIZED : Globals.EMOTIONS.TIRED;
        }

        else if (v == interestScale)
        {
            emotionState = interestScale >= 0 ? Globals.EMOTIONS.SURPRISED : Globals.EMOTIONS.DISAPPOINTMENT;
        }

        else if (v == confidenceScale)
        {
            emotionState = confidenceScale >= 0 ? Globals.EMOTIONS.CONFIDENT : Globals.EMOTIONS.EMBARRASSED;
        }

        else if (v == entertainmentScale)
        {
            emotionState = entertainmentScale >= 0 ? Globals.EMOTIONS.ENTERTAINED : Globals.EMOTIONS.BOREDOM;
        }
        //call the color change event...
        colorMaster.EventColorChange(emotionState);
    }

    private int GetDominateEmotion()
    {
        //clear the list first
        LEmotions.Clear();
        //add all the emotions to the list, so we can check their values quickly...
        LEmotions.Add(happinessScale);
        LEmotions.Add(angerScale);
        LEmotions.Add(energyScale);
        LEmotions.Add(interestScale);
        LEmotions.Add(confidenceScale);
        LEmotions.Add(entertainmentScale);

        //intial value for checking which emotion is the highest/lowest...
        int highestEmotionalValue = 0;
        int lowestEmotionalValue = 0;

        //loops through the list of values, and returns the highest value...
        foreach (int v in LEmotions)
        {
            highestEmotionalValue = highestEmotionalValue <= v ? v : highestEmotionalValue;
            lowestEmotionalValue = lowestEmotionalValue >= v ? v : lowestEmotionalValue;
        }

        //find out which value is higher (we need to get the absolute value)
        int highestValue = Mathf.Max(Mathf.Abs(lowestEmotionalValue), Mathf.Abs(highestEmotionalValue));

        //now that we have the highest value, we make it negative if it needs to be...
        if (Mathf.Abs(lowestEmotionalValue) > Mathf.Abs(highestEmotionalValue))
        {
            highestValue = -highestValue;
        }

        return highestValue;
    }
}
