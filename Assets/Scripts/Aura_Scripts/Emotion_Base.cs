using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion_Base : MonoBehaviour
{
    // PROPERTIES //
    private int emotionMin = 0;
    private int emotionMax = 100;
    private int emotionValue = 0;

    //Constructor for the Emotional base that all emotions will derive from...
    public Emotion_Base(int min, int max, int value)
    {
        emotionMin = min;
        emotionMax = max;
        emotionValue = value;
    }

    //public getter/setter for the emotions Minimum value...
    public int EmotionMin 
    {
        get 
        {
            return emotionMin;
        }
        set 
        {
            emotionMin = value;
        }
    }

    //public getter/setter for the emotions maximum value...
    public int EmotionMax 
    {
        get {
            return emotionMax;
        }
        set {
            emotionMax = value;
        }
    }

    //public getter/setter for the emotions actual value...
    public int EmotionValue
    {
        get {
            return emotionValue;
        }
        set {
            emotionValue = value;
        }
    }

}
