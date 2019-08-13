using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    //holds the name of the person the player is talking to
    public string name;

    //holds all of the dialogue for the interaction
    public TextAsset textFile;

    public String[] sentences;

    public int lineNumber;

}

