using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Emotions : MonoBehaviour
{

    public enum EMOTION
    {
        FEAR = 1,
        ANGER = 2,
        JOY = 3,
        SADNESS = 4 
    }

    [Header("Emotions")]
    [Range(0, 100)]
    public float Fear;
    [Range(0, 100)]
    public float Anger;
    [Range(0, 100)]
    public float Joy;
    [Range(0, 100)]
    public float Sadness;

    public float EmotionModifier = .002f;

    [Header("Current State")]
    public EMOTION emotionState;


    public void ModifyEmotion(EMOTION e, float value)
    {
        switch (e)
        {
            case EMOTION.FEAR:
                Fear += value * Time.deltaTime;
                Fear = Fear >= 100 ? 100 : Fear;
                Fear = Fear <= 0 ? 0 : Fear;
                CheckHighestvalue((int)Fear,(int)Anger,(int)Joy,(int)Sadness);
                break;
            case EMOTION.ANGER:
                Anger += value * Time.deltaTime;
                Anger = Anger >= 100 ? 100 : Anger;
                Anger = Anger <= 0 ? 0 : Anger;
                CheckHighestvalue((int)Fear, (int)Anger, (int)Joy, (int)Sadness);
                break;
            case EMOTION.JOY:
                Joy += value * Time.deltaTime;
                Joy = Joy >= 100 ? 100 : Joy;
                Joy = Joy <= 0 ? 0 : Joy;
                CheckHighestvalue((int)Fear, (int)Anger, (int)Joy, (int)Sadness);
                break;
            case EMOTION.SADNESS:
                Sadness += value * Time.deltaTime;
                Sadness = Sadness >= 100 ? 100 : Sadness;
                Sadness = Sadness <= 0 ? 0 : Sadness;
                CheckHighestvalue((int)Fear, (int)Anger, (int)Joy, (int)Sadness);
                break;
            default:
                break;

        }
    }

    public void OnTriggerStay(Collider col)
    {
        this.ModifyEmotion(col.GetComponent<Emotions>().emotionState, col.GetComponent<Emotions>().EmotionModifier);
        this.ModifyEmotion(col.GetComponent<Emotions>().OppositeEmotion
            (col.GetComponent<Emotions>().emotionState), - col.GetComponent<Emotions>().EmotionModifier);

    }

    private void CheckHighestvalue(int a, int b, int c, int d)
    {
        int index = 0;

        if (a > b && a > c && a > d)
        {
            index = 1;
        }
        else if (b > a && b > c && b > d)
        {
            index = 2;
        }
        else if (c > a && c > b && c > d)
        {
            index = 3;
        }
        else if (d > a && d > b && d > c)
        {
            index = 4;
        }
        
        switch (index)
        {
            case 1:
                emotionState = EMOTION.FEAR;
                Debug.Log("Fear");
                break;
            case 2:
                emotionState = EMOTION.ANGER;
                Debug.Log("Anger");
                break;
            case 3:
                emotionState = EMOTION.JOY;
                Debug.Log("Joy");
                break;
            case 4:
                emotionState = EMOTION.SADNESS;
                Debug.Log("Happyness");
                break;

        }
    }

    private EMOTION OppositeEmotion(EMOTION e)
    {
        switch (e)
        {
            case EMOTION.FEAR:
                return EMOTION.ANGER;
            case EMOTION.ANGER:
                return EMOTION.FEAR;
            case EMOTION.JOY:
                return EMOTION.SADNESS;
            case EMOTION.SADNESS:
                return EMOTION.JOY;
        }

        return emotionState;
    }

}
