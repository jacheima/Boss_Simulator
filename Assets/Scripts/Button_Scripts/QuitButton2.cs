using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton2 : MonoBehaviour
{
    /*
    When the player click on the quit button the second time it is asked,
    this will take the player back to the main menu where they can exit.
    */

    public void QuitButton2_Click()
    {
        GameManager.instance.SetGameState((int)GameManager.GameState.Start);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
