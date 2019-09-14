using UnityEngine;

public class FPD_VerticalLimitCircleAttribute : PropertyAttribute
{
    public float MinValue = -180;
    public float MaxValue = 180;
    public bool symSlider = true;
    public bool drawHR = true;

    public FPD_VerticalLimitCircleAttribute(int min, int max, bool symetricalSlider = true, bool horLine = true)
    {
        MinValue = min;
        MaxValue = max;
        symSlider = symetricalSlider;
        drawHR = horLine;
    }
}

public class FPD_HorizontalLimitCircleAttribute : PropertyAttribute
{
    public float MinValue = -180;
    public float MaxValue = 180;
    public bool symSlider = true;
    public bool drawHR = true;

    public FPD_HorizontalLimitCircleAttribute(int min, int max, bool symetricalSlider, bool horLine = true)
    {
        MinValue = min;
        MaxValue = max;
        symSlider = symetricalSlider;
        drawHR = horLine;
    }
}