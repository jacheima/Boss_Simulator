using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    /*
    When the player clicks on the start button, it will take them
    to the level.
    */

    public void StartButton_Click()
    {
        GameManager.instance.SetGameState((int)GameManager.GameState.Level);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
