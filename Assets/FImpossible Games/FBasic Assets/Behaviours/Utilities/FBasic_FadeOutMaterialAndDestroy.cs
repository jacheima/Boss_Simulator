using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// Changing alpha of material to 0 and destroying game object
    /// </summary>
    public class FBasic_FadeOutMaterialAndDestroy : FBasic_MaterialScriptBase
    {
        public string shaderParamName = "_TintColor";
        public float FadeSpeed = 1f;

        private void Start()
        {
            GetRendererMaterial();
        }

        private void Update()
        {
            Color newColor = RendererMaterial.GetColor(shaderParamName);
            newColor = FColorMethods.ChangeColorAlpha(newColor, newColor.a - Time.deltaTime * FadeSpeed);

            RendererMaterial.SetColor(shaderParamName, newColor);

            if (newColor.a <= 0f) GameObject.Destroy(gameObject);
        }
    }
}