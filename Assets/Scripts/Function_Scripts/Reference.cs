using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reference : MonoBehaviour
{

   // private Event_Master eventMaster;
    private int someRandomNumber;

    // this function will be utilized to get references to any objects that may need to be referenced...
    // set initial variables values and whatnot...
    private void Init()
    {
        someRandomNumber = GetRandomNumber(0, 99);
        //any other things you may need to initialize place it in here...
    }

    // when we are using the event system we need to use OnEnable to do all our initialization...
    private void OnEnable()
    {
        //call the Init script first in order to initialize anything that may be utilized in your functions/methods...
        Init();
        //this will take the eventmaster script, the do something event, and subscribe the function DoSomethingCool to it...
        //then when any script calls EventDoSomething(); it will call ANY and ALL functions subscribed to this event...
        Event_Master.DoSomething += DoSomethingCool;
    }

    // when we are using the event system we need to utilize OnDisable to perform our cleanup and unsubscribe events...
    // if we don't unsubscribe and our object goes out of scope, we will end up throwing errors and crashing...
    private void OnDisable()
    {
        //this will take the eventmaster script, the do something event, and unsubscribe the dosomethingcool function...
        Event_Master.DoSomething -= DoSomethingCool;
    }

    private void Update()
    {
        //simple way to test this all out by checking if someone pressed R and then calls the event.
        if (Input.GetKeyUp(KeyCode.R))
        {
            Event_Master.EventDoSomething();
        }
    }


    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    private void DoSomethingCool()
    {
        //here is an event that we will subscribe to the do something event in the...
        //master even handler...
        Debug.Log(someRandomNumber.ToString());
        someRandomNumber = GetRandomNumber(0, 99);
    }

}
