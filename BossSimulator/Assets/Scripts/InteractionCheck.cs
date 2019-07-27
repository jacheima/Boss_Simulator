using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCheck : MonoBehaviour
{
    public Camera camera;

    public int talkDistance;

    public LayerMask targetMask;

    public Interactable interactable;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            Debug.Log(objectHit);

            if (Vector3.Distance(transform.position, objectHit.position) <= talkDistance)
            {
                Debug.Log(Vector3.Distance(transform.position, objectHit.position));
                interactable.StartInteraction();
            }
        }
    }
}
