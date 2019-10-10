using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;

    void Start()
    {
        time = GetComponent<Text>();
    }

    void Update()
    {
        time.text = UnityEngine.Time.time.ToString("0.0");
    }
}
