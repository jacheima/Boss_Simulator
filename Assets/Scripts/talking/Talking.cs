using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talking : MonoBehaviour
{
    private List<chat> Emotion = new List<chat>();
    public Text emotionText;
    public float delayBeforeNext;


    // Start is called before the first frame update
    void Start()
    {
        TextAsset Emotiondata = Resources.Load<TextAsset>("EmotionPlaytest");
        string[] data = Emotiondata.text.Split(new char[] {'\n'});

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[ ]{','});
            if (row[1] != "")
            {
                chat q = new chat();
                int.TryParse(row[0], out q.ID);
                q.C1 = row[1];
                q.C2 = row[2];
                q.C3 = row[3];
                q.C4 = row[4];
                q.C5 = row[5];

                Emotion.Add(q);
            }
        }

        //foreach (chat q in Emotion)
        //{
        //    Debug.Log(q.C1 + "," + q.C3);
        //}

        StartCoroutine(waity());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waity()
    {
        foreach (chat q in Emotion)
        {
            emotionText.text = q.C1 + "," + q.C3;
            yield return new WaitForSeconds(delayBeforeNext);
        }
    }
}
