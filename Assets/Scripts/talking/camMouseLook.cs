using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 SmoothV;
    public float sensitivity = 5.0f;
        public float smothing = 2.0f;

    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smothing, sensitivity * smothing));

        SmoothV.x = Mathf.Lerp(SmoothV.x, md.x, 1f / smothing);
        SmoothV.y = Mathf.Lerp(SmoothV.y, md.y, 1f / smothing);
        mouseLook += SmoothV;
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
