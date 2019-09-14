using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Master : MonoBehaviour
{
    public delegate void ColorSystem(Globals.EMOTIONS emotion);

    public event ColorSystem ColorChange;

    public void EventColorChange(Globals.EMOTIONS e)
    {
        ColorChange?.Invoke(e);
    }
}
