using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    public RectTransform helpUI;

    void Start()
    {
        helpUI = GetComponent<RectTransform>();
    }

    public void SwoopOutComplete()
    {
        helpUI.offsetMin = new Vector2(0,-482);
    }
}
