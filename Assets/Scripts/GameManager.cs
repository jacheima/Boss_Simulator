using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    These are the GameObjects that are for the Canvas of the game.
    */
    public GameObject StartCanvas, 
                      LevelCanvas, 
                      MenuCanvas,
                      QuitCanvas;

    public static GameManager instance;

    /*
    This is the list of the game states that will happen throughout the game. 
    */
    public enum GameState
    {
        Start,
        Level,
        Menu,
        Quit,
        Cancel
    }

    public GameState gameState;

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Destroying Copycat");
            Destroy(this);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            MenuCanvas.SetActive(true);
        }
    }

    /*
    This module will run a switch function that will activate and deactivate
    specific canvas when a case is met.
    */
    void ActivateScreens()
    {
        switch (gameState)
        {
            case GameState.Start:
                StartCanvas.SetActive(true);
                LevelCanvas.SetActive(false);
                MenuCanvas.SetActive(false);
                QuitCanvas.SetActive(false);
                break;

            case GameState.Level:
                StartCanvas.SetActive(false);
                LevelCanvas.SetActive(true);
                break;

            case GameState.Menu:
                MenuCanvas.SetActive(true);
                break;

            case GameState.Quit:
                MenuCanvas.SetActive(false);
                QuitCanvas.SetActive(true);
                break;

            case GameState.Cancel:
                MenuCanvas.SetActive(false);
                QuitCanvas.SetActive(false);
                break;
        }
    }

    //This module prepares to activate and deactivate the appropiate
    //canvas.
    public void SetGameState(int state)
    {
        gameState = (GameState)state;
        ActivateScreens();
    }
}
