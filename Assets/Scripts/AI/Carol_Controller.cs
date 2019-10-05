using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carol_Controller : AI_Controller
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        { 
            case AI_STATES.Work:
                Work();

                if (pd.pee >=75)
                {
                    currentState = AI_STATES.Pee;
                }
                else if (pd.eat >= 75)
                {
                    currentState = AI_STATES.Eat;
                }
                else if (pd.social >= 75)
                {
                    currentState = AI_STATES.Socialize;
                }
                break;
            case AI_STATES.Pee:
                Pee();

                if (pd.eat >= 75)
                {
                    currentState = AI_STATES.Eat;
                }
                else if (pd.social >= 75)
                {
                    currentState = AI_STATES.Socialize;
                }

                if (pd.eat <= 25 || pd.pee <= 25 || pd.social <= 25)
                {
                    currentState = AI_STATES.Work;
                }
                break;
            case AI_STATES.Eat:
                Eat();

                if (pd.pee >= 75)
                {
                    currentState = AI_STATES.Pee;
                }
                else if (pd.social >= 75)
                {
                    currentState = AI_STATES.Socialize;
                }
                if (pd.eat <= 25 || pd.pee <= 25 || pd.social <= 25)
                {
                    currentState = AI_STATES.Work;
                }
                break;
            case AI_STATES.Socialize:
                Social();

                if (pd.pee >= 75)
                {
                    currentState = AI_STATES.Pee;
                }
                else if (pd.eat >= 75)
                {
                    currentState = AI_STATES.Eat;
                }
                if (pd.eat <= 25 || pd.pee <= 25 || pd.social <= 25)
                {
                    currentState = AI_STATES.Work;
                }
                break;
        }
    }
}
