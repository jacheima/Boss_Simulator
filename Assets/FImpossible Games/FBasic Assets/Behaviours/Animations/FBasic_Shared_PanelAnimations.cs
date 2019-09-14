using System.Collections;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class to animate pressing button on panel
    /// </summary>
    public class FBasic_Shared_PanelAnimations : MonoBehaviour
    {
        [Tooltip("How long in seconds should be played animation of button going down")]
        public float AnimationTime = 1f;
        [Tooltip("Extra value for easing functions to change a little their elasticity or other beahaviour")]
        public float EaseExtraValue = 0.85f;

        private Transform buttonTransform;
        private Vector3 buttonInitPosition;

        protected virtual void Start()
        {
            buttonTransform = transform.Find("Button");
            buttonInitPosition = buttonTransform.localPosition;
        }


        public virtual void Click()
        {
            StopAllCoroutines();
            StartCoroutine(ClickAniamtion());
        }

        /// <summary>
        /// Sometimes courutine is better than Update() because update is running during whole time
        /// but courutine can be used only when is needed, 
        /// notice that about 1000 behaviours with empty Update() can do some overload on CPU
        /// </summary>
        IEnumerator ClickAniamtion()
        {
            buttonTransform.localPosition = buttonInitPosition;
            float time = 0f;

            while(time < AnimationTime * 0.6f)
            {
                time += Time.deltaTime;

                float progress = time / AnimationTime;

                buttonTransform.localPosition = Vector3.LerpUnclamped(buttonInitPosition, buttonInitPosition - Vector3.up * 0.05f, FEasing.EaseOutElastic(0f, 1f, progress, EaseExtraValue) );

                yield return null;
            }

            time = 0f;

            Vector3 currentPos = buttonTransform.localPosition;

            while (time < AnimationTime / 4f)
            {
                time += Time.deltaTime;

                float progress = time / (AnimationTime / 4f);

                buttonTransform.localPosition = Vector3.LerpUnclamped(currentPos, buttonInitPosition, FEasing.EaseInOutCubic(0f, 1f, progress));

                yield return null;
            }

            yield break;
        }
    }
}
