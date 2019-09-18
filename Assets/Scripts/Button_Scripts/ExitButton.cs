using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    /*
    When the player clicks on the exit button, it will close the game.
    */
    public void ExitButton_Click()
    {
        Debug.Log("Game Ended!");
        Application.Quit();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
