using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Event_Master : object
{
    // this is the master event system that you will create the events from...
    // think of this "EventSystem" as the event type...
    // so we will create events into the EventSystem and each event has its own name...
    public delegate void EventSystem();

    // this is an event it belongs to the EventSystem delegate that we just created...
    // these events CAN have parameters passed through them, and will be defined later...
    public static event EventSystem DoSomething;


    //this is the event that we will call, and anything subscribed to it will subsequentually be called...
    //this is essentually everything needed to start the event system...
    public static void EventDoSomething()
    {
        //this is the call to the event named DoSomething;
        DoSomething();
    }
}
