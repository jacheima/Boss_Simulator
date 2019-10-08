using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Change : MonoBehaviour
{
    //all the materials i created to show emotion transference...
    public Material mAnger, MBoredom, mConfident, mDisappointment, mEmbarrassed, mEnergized,
        mEntertained, mFear, mHappy, mSad, mSurprised, mTired, mSociopath;
    public Color_Master colorMaster;

    void OnEnable()
    {
        colorMaster.ColorChange += ColorChange;
    }

    void OnDisable()
    {
        colorMaster.ColorChange -= ColorChange;
    }

    /// <summary>
    /// This function takes in a parameter of emotion
    /// and then depending on the emotion that is passed in
    /// changes the material of the object to the corresponding color...
    /// </summary>
    private void ColorChange(Globals.EMOTIONS e)
    {
        switch (e)
        {
            case Globals.EMOTIONS.ANGER:
                GetComponent<Renderer>().material = mAnger;
                break;
            case Globals.EMOTIONS.BOREDOM:
                GetComponent<Renderer>().material = MBoredom;
                break;
            case Globals.EMOTIONS.CONFIDENT:
                GetComponent<Renderer>().material = mConfident;
                break;
            case Globals.EMOTIONS.DISAPPOINTMENT:
                GetComponent<Renderer>().material = mDisappointment;
                break;
            case Globals.EMOTIONS.EMBARRASSED:
                GetComponent<Renderer>().material = mEmbarrassed;
                break;
            case Globals.EMOTIONS.ENERGIZED:
                GetComponent<Renderer>().material = mEnergized;
                break;
            case Globals.EMOTIONS.ENTERTAINED:
                GetComponent<Renderer>().material = mEntertained;
                break;
            case Globals.EMOTIONS.FEAR:
                GetComponent<Renderer>().material.color = mFear.color;
                break;
            case Globals.EMOTIONS.HAPPY:
                GetComponent<Renderer>().material = mHappy;
                break;
            case Globals.EMOTIONS.SAD:
                GetComponent<Renderer>().material = mSad;
                break;
            case Globals.EMOTIONS.SURPRISED:
                GetComponent<Renderer>().material = mSurprised;
                break;
            case Globals.EMOTIONS.TIRED:
                GetComponent<Renderer>().material = mTired;
                break;
            default:
                GetComponent<Renderer>().material = mSociopath;
                break;
        }
    }
}
