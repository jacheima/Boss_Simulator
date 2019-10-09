using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public float totalIncome = 0f;
    public float winCondition = 10000f;
    public float incomeValue = 100f;

    public List<GameObject> employeeDesks;

    public GameObject employee;

    public static GameManager instance;
>>>>>>> MoneyFeature

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
<<<<<<< HEAD
            DontDestroyOnLoad(gameObject);
        }
        else
        {
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
                MenuCanvas.SetActive(false);
                QuitCanvas.SetActive(false);
                break;

            case GameState.Level:
                StartCanvas.SetActive(false);
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
=======
        }

        if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        employeeDesks.Add(GameObject.Find("Desk"));
    }
    void Update()
    {
        if (employeeDesks != null)
        {

        }

        //check to see if we've met our win condition
        if (totalIncome < winCondition && employee.GetComponent<AI_Controller>().isAtDesk)
        {
            GenerateIncome();
        }
        else
        {
            Debug.Log("You Have Maximized your profit margin, Congratulations!");
        }
    }
    public void GenerateIncome()
    {
      
            totalIncome += incomeValue * Time.deltaTime;
            Debug.Log("Current Profit: " + totalIncome);

>>>>>>> MoneyFeature
    }
}
