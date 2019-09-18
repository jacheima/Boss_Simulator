using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    /*
    When the player clicks on the start button, it will take them
    to the level.
    */

    public void StartButton_Click()
    {
        SceneManager.LoadScene(1);
    }
}
