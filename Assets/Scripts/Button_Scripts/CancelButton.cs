using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    /*
    When you click on cancel, it just closes the menu.
    */

    public void CancelButton_Click()
    {
        GameManager.instance.SetGameState((int)GameManager.GameState.Cancel);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
