using UnityEngine;

public class Color_Change : MonoBehaviour
{
    //all the materials i created to show emotion transference...
    public Material mAnger, mFear, mHappy, mSad;
    public Color_Master colorMaster;

/// <summary>
    /// This function takes in a parameter of emotion
    /// and then depending on the emotion that is passed in
    /// changes the material of the object to the corresponding color...
    /// </summary>
    public void PerformColorChange(Globals.EMOTIONS emotion)
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            switch (emotion)
            {
                case Globals.EMOTIONS.ANGER:
                    rend.material = mAnger;
                    break;

                case Globals.EMOTIONS.FEAR:
                    rend.material = mFear;
                    break;

                case Globals.EMOTIONS.HAPPY:
                    rend.material = mHappy;
                    break;

                case Globals.EMOTIONS.SAD:
                    rend.material = mSad;
                    break;
            }
        }
    }
}
