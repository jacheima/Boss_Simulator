using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_UI : MonoBehaviour
{
    public Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = GameManager.instance.totalIncome.ToString("C0");
    }
}
