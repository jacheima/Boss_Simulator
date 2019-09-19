using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office_Layout_Connector : MonoBehaviour
{

    public GameManager GM;

    public GameObject LevelCanvas,
                      MenuCanvas,
                      QuitCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Running Connector Start");
        GM = GameManager.instance;

        GM.LevelCanvas = LevelCanvas;
        GM.MenuCanvas = MenuCanvas;
        GM.QuitCanvas = QuitCanvas;
    }
}
