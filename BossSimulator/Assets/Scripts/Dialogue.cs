using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.VR;

[System.Serializable]

public class Dialogue
{
    public string name;

    public TextAsset textDialogueFile;
    public TextAsset textOptionsFile;

    public string[] sentences;

}



