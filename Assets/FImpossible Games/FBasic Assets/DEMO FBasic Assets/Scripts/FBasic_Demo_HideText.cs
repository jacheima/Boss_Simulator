using UnityEngine;
using UnityEngine.UI;

namespace FIMSpace.Basics
{
    public class FBasic_Demo_HideText : MonoBehaviour
    {
        private Text text;

        void Start()
        {
            text = GetComponent<Text>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H)) text.enabled = !text.enabled;
        }
    }
}
