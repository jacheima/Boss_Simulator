using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        //if the player presses left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //Create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                //Check if interactable exists
                if (interactable != null)
                {
                    interactable.StartInteraction();
                }
            }


        }
    }
}
