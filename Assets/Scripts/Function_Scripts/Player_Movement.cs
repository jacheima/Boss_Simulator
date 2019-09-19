using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
    }
}
