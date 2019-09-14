using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic script to animate material float property value
    /// </summary>
    public class FBasic_MaterialFloatAnimate : FBasic_MaterialScriptBase
    {
        [Tooltip("Texture identificator in shader")]
        public string TextureProperty = "_BumpScale";

        public float PropertyInitValue = 1f;
        public float RangeValue = 0.3f;
        public float AnimateSpeed = 1f;

        float time;
        float randomValue;

        void Start()
        {
            GetRendererMaterial();

            time = Random.Range(-Mathf.PI, Mathf.PI);
            randomValue = Random.Range(-Mathf.PI, Mathf.PI);
        }

        void Update()
        {
            time += Time.deltaTime * AnimateSpeed;

            float value = Mathf.Sin(time) * RangeValue + Mathf.Cos(randomValue + time * 0.7f) * RangeValue;

            RendererMaterial.SetFloat(TextureProperty, value + PropertyInitValue);
        }
    }

}