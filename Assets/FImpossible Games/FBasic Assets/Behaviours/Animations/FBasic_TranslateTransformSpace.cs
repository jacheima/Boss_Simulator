using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Basic script to move object in object's transform orientation direction
    /// </summary>
    public class FBasic_TranslateTransformSpace : MonoBehaviour
    {
        public Vector3 TranslationAxis = new Vector3(0f, 0f, 1f);

        /// <summary> Multiplies deltaTime </summary>
        public float TranslationSpeed = 10f;

        /// <summary> If animator should go on for example during game pause (useful for UI) </summary>
        public bool UnscaledDeltaTime = false;

        protected virtual void Update()
        {
            float delta;
            if (UnscaledDeltaTime) delta = Time.unscaledDeltaTime; else delta = Time.deltaTime;

            transform.position += transform.TransformVector(TranslationAxis) * delta * TranslationSpeed;
        }
    }
}