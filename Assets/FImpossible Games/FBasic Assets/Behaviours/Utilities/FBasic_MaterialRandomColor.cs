using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic script to change color of material property at start
    /// </summary>
    public class FBasic_MaterialRandomColor : FBasic_MaterialScriptBase
    {
        [Tooltip("Texture identificator in shader")]
        public string TextureProperty = "_Color";

        public Vector2 HueRange = new Vector2(0f, 1f);
        public Vector2 SaturationRange = new Vector2(0.9f, 1f);
        public Vector2 ValueRange = new Vector2(0.9f, 1f);
        public Vector2 AlphaRange = new Vector2(1f, 1f);

        void Start()
        {
            GetRendererMaterial();

            Color newColor = Color.HSVToRGB(Random.Range(HueRange.x, HueRange.y), Random.Range(SaturationRange.x, SaturationRange.y), Random.Range(ValueRange.x, ValueRange.y));
            newColor.a = Random.Range(AlphaRange.x, AlphaRange.y);

            RendererMaterial.SetColor(TextureProperty, newColor);
        }
    }

}