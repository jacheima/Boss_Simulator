using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talking : MonoBehaviour
{
    private List<chat> Emotion = new List<chat>();
    public Text emotionText;
    public float delayBeforeNext;
    public Text AItext;
    public Text boss_Answer;
    public Text boss_Answer2;
    public Text boss_Answer3;
    public Text boss_Answer4;
    private List<choosingxaxis> lockanswer = new List<choosingxaxis>();
    public string sata;
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

        TextAsset LockonData = Resources.Load<TextAsset>("EmotionPlaytest");
        string[] beta = LockonData.text.Split(new char[] { ',' });

        for (int i = 1; i < beta.Length - 1; i++)
        {
            string[] Xx = beta[i].Split(new char[] { '\n' });
            if (Xx[1] != "")
            {
                choosingxaxis T = new choosingxaxis();
                int.TryParse(Xx[0], out T.IDX);
                T.X1 = Xx[1];
                T.X2 = Xx[2];
                T.X3 = Xx[3];
                T.X4 = Xx[4];
                T.X5 = Xx[5];



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
       if (Input.GetMouseButtonDown(0))
        {
            foreach (chat q in Emotion)
            {
                boss_Answer.text = q.C2;
            }
       }
    }

    IEnumerator waity()
    {
        foreach (chat q in Emotion)
        {
            emotionText.text = q.C1 + "," + q.C3;
            yield return new WaitForSeconds();
        }
    }
}
