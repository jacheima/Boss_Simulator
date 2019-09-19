using UnityEngine;
using UnityEngine.UI;

public class Aura_GUI : MonoBehaviour
{
    public Slider angerMeter;
    public Slider fearMeter;
    public Slider happyMeter;
    public Slider sadMeter;
    public Image currentEmotion;

    public Sprite happy;
    public Sprite sad;
    public Sprite angry;
    public Sprite fear;

    public Emotions emotions;

    public void SetMeters()
    {
        angerMeter.value = emotions.anger;
        fearMeter.value = emotions.fear;
        sadMeter.value = emotions.sadness;
        happyMeter.value = emotions.happiness;
    }

    public void SetCurrentEmotion(Globals.EMOTIONS e)
    {
        switch (e)
        {
            case Globals.EMOTIONS.ANGER:
                currentEmotion.sprite = angry;
                break;
            case Globals.EMOTIONS.FEAR:
                currentEmotion.sprite = fear;
                break;
            case Globals.EMOTIONS.SAD:
                currentEmotion.sprite = sad;
                break;
            case Globals.EMOTIONS.HAPPY:
                currentEmotion.sprite = happy;
                break;
        }
    }
}
