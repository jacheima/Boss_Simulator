using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aura_Transcendence : MonoBehaviour
{

    public Emotions e;
    public float maxRadius = 20;
    [Tooltip("The amount of emotion passed during a transcendence")]
    public float tValue = 20;

    public GameObject angerPanel;
    public GameObject fearPanel;
    public GameObject happyPanel;
    public GameObject sadPanel;

    void Update()
    {
        CheckForTranscendence();   
    }

    void CheckForTranscendence()
    {
        if (e.happiness >= 100)
        {
            Transcend(Globals.EMOTIONS.HAPPY);
            e.happiness = 50.0f;
        }
        if (e.sadness >= 100)
        {
            Transcend(Globals.EMOTIONS.SAD);
            e.sadness = 50.0f;
        }
        if (e.anger >= 100)
        {
            Transcend(Globals.EMOTIONS.ANGER);
            e.anger = 50.0f;
        }
        if (e.fear >= 100)
        {
            Transcend(Globals.EMOTIONS.FEAR);
            e.fear = 50.0f;
        }
    }

    void Transcend(Globals.EMOTIONS emotion)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRadius);
        foreach (Collider c in hitColliders)
        {
            switch (emotion)
            {
                case Globals.EMOTIONS.ANGER:
                    Debug.Log("Transcend Anger");
                    c.gameObject.GetComponent<Emotions>().anger += tValue;
                    StartCoroutine(FlashPanel(angerPanel));
                    break;
                case Globals.EMOTIONS.FEAR:
                    Debug.Log("Transcend Fear");
                    c.gameObject.GetComponent<Emotions>().fear += tValue;
                    StartCoroutine(FlashPanel(fearPanel));
                    break;
                case Globals.EMOTIONS.HAPPY:
                    Debug.Log("Transcend Happy");
                    c.gameObject.GetComponent<Emotions>().happiness += tValue;
                    StartCoroutine(FlashPanel(happyPanel));
                    break;
                case Globals.EMOTIONS.SAD:
                    Debug.Log("Transcend Sad");
                    c.gameObject.GetComponent<Emotions>().sadness += tValue;
                    StartCoroutine(FlashPanel(sadPanel));
                    break;
            }
        }
    }

    IEnumerator FlashPanel(GameObject p)
    {
        Image i = p.GetComponent<Image>();
        Color c = i.color;
        c.a = 10;
        i.color = c;
        yield return new WaitForSeconds(.1f);
        c.a = 0;
        i.color = c;
    }
}
