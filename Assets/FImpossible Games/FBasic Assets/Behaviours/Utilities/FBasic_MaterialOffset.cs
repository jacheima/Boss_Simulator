using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic script to animate material with simple offset and others
    /// </summary>
    public class FBasic_MaterialOffset : FBasic_MaterialScriptBase
    {
        [Tooltip("Texture identificator in shader")]
        public string TextureProperty = "_MainTex";

        [Tooltip("Offset speed for property texture, moving with deltaTime")]
        public Vector2 OffsetSpeed = Vector2.zero;

        void Start()
        {
            GetRendererMaterial();
        }

        void Update()
        {
            Vector2 newOffset = RendererMaterial.GetTextureOffset(TextureProperty) + OffsetSpeed * Time.deltaTime;

            RendererMaterial.SetTextureOffset(TextureProperty, newOffset);
        }
    }

}