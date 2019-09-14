using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TEST_Detection))]
public class AURA_Radius : Editor
{
    void OnSceneGUI()
    {
        TEST_Detection test = (TEST_Detection)target;

        //show the aura radius
        Handles.color = Color.yellow;
        Handles.DrawWireArc(test.transform.position, Vector3.up, Vector3.forward, 360, 5f);
    }
    
}
