using UnityEngine;

public class FPD_MinMaxSliderAttribute : PropertyAttribute
{
    public float MinValue = -60;
    public float MaxValue = 60;

    public FPD_MinMaxSliderAttribute(int min, int max)
    {
        MinValue = min;
        MaxValue = max;
    }
}