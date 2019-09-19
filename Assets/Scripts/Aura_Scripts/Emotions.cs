using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour
{
    #region
    /// <summary>
    /// We will use the following scales to add/subtract values from
    /// in order to determine what emotions state the person/object is in
    /// </summary>
    [Header("EMOTIONS")]
    // NEGATIVE EMOTIONS //
    public float happiness = Mathf.Clamp(0.0f, 1.0f, 100.0f);
    public float sadness = Mathf.Clamp(0.0f, 1.0f, 100.0f);
    public float anger = Mathf.Clamp(0.0f, 1.0f, 100.0f);
    public float fear = Mathf.Clamp(0.0f, 1.0f, 100.0f);

    //List of emotion scales, we will initialize this list in OnEnable...
    private List<float> LEmotions = new List<float>();

    [SerializeField]
    private Aura_Master auraMaster;
    [SerializeField]
    private Color_Master colorMaster;
    [SerializeField]
    private Aura_GUI auraGUI = null;
    public Color_Change colorChange;

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
    public void CheckEmotionalState()
    {
        float v = Mathf.Round(GetDominateEmotion());
        //check if the highest value equals one of your emotions, if it does, then we check
        //to see if the scale is greater than zero, if it is, then we set the emotion respectfully...
        if (v == Mathf.Round(happiness))
        {
            emotionState = Globals.EMOTIONS.HAPPY;
        }
        else if (v == Mathf.Round(anger))
        {
            emotionState = Globals.EMOTIONS.ANGER;
        }

        else if (v == Mathf.Round(sadness))
        {
            emotionState = Globals.EMOTIONS.SAD;
        }

        else if (v == Mathf.Round(fear))
        {
            emotionState = Globals.EMOTIONS.FEAR;
        }

        //change color if were allowed to..
        if (colorChange != null)
        {
            colorChange.PerformColorChange(emotionState);
        }
        //check to see if we have a gui to change

        if(auraGUI != null)
        {
            //set the meters values
            auraGUI.SetMeters();
            //set the current emotion image above the player to the correct image...
            auraGUI.SetCurrentEmotion(emotionState);
        }
    }

    private float GetDominateEmotion()
    {
        //clear the list first
        LEmotions.Clear();
        //add all the emotions to the list, so we can check their values quickly...
        LEmotions.Add(happiness);
        LEmotions.Add(sadness);
        LEmotions.Add(anger);
        LEmotions.Add(fear);

        float max = 0;
        foreach (float f in LEmotions)
        {
            // VARIABLE = CONDITON ? TRUE : FALSE;
            // this means, if f is greater than max
            // ? = ternaray operator ( tells the compiler this is a conditional statement)
            // value if true : value if false;
            // so this says if f is greater than max, if true max = f, else max = max;
            max = (f > max) ? f : max;
        }
        // return the largest value from the list...
        return max;
    }
}
