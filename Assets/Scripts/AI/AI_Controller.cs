using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public bool isAtDesk = false;

    public float stateStartTime;

    public Pawn_Data pd;

    public enum AI_STATES
    {
        Work, Pee, Eat, Socialize
    }

    public AI_STATES currentState;

    void Start()
    {
        
    }

    public void Work()
    {
        GameManager.instance.GenerateIncome();
        Debug.Log("Work");
    }
    public void Eat()
    {
        Debug.Log("Eating");
    }
    public void Social()
    {
        Debug.Log("Socializing");
    }
    public void Pee()
    {
        Debug.Log("Peeing");
    }

}
