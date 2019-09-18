using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton1 : MonoBehaviour
{
    /*
    The first time you click on the quit button, it takes
    you to the next canvas, which asks the player if they
    are sure that they want to exit the game.
    */

    public void QuitButton1_Click()
    {
        GameManager.instance.SetGameState((int)GameManager.GameState.Quit);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
