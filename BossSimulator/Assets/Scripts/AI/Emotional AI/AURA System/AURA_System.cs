using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AURA_System : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Distance(cube1.transform.position, cube2.transform.position);
    }
}


//function will require an empty game object variable
//global or fixed variable with set fixed-emotional-transfer(Fixed emotional transfer is a constant that should not change and be universal for all game objects)
//create variable of type integer Variable name: Aura Layer
//create variable of type float Variable Name: Emotional_Transferance_Rate(Value of the Algorithm)
//create variable of type float current_emotional_value(This will be the current highest value and any effects)
//create a variable of Maximum_Emotional_Value (100)



// rage meter emotional function variable range 0- negative 100
    
    //Emotional State Annoyance between 0 - 50
        //
    //if between 50- 100 AI is angry

    // if 100 switch to rage state
     //rage state lasts 10 seconds, reduces rage by 75% over ten seconds

    

    //Detect any object within a radius of 5 meters

    //Verify current internal emotional state

    //transmit my emotion transference rate

    //receive other objects emotional transference rate

    //add or subtract the emotional transference rate
