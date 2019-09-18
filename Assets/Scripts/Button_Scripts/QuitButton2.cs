using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton2 : MonoBehaviour
{
    /*
    When the player click on the quit button the second time it is asked,
    this will take the player back to the main menu where they can exit.
    */

    public GameManager GM;

    void QuitButton2_Click()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
