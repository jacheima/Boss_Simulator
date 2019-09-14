using System.Collections;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Example of using FBasic_Pullable to create 3D knob
    /// </summary>
    public class FBasic_PullableKnob : FBasic_Pullable
    {
        public float KnobValue { get; private set; }

        private Transform body;
        private Material markerMaterial;
        private Material valueMaterial;

        protected override void Start()
        {
            base.Start();
            KnobValue = YValue / 100f;
            body = transform.Find("Body");
            markerMaterial = body.Find("Marker").GetComponent<Renderer>().material;
            valueMaterial = transform.Find("Value").GetComponent<Renderer>().material;
            UpdatePullableOrientation();
        }

        protected override void UpdatePullableOrientation()
        {
            body.localRotation = Quaternion.Euler(0f, Mathf.Lerp(-115f, 115f, KnobValue), 0f);
            valueMaterial.SetTextureOffset("_MainTex", new Vector2(Mathf.Lerp(0f, 0.5f, KnobValue), 0f));
        }

        protected override void UpdateIn()
        {
            base.UpdateIn();

            if ( Holding )
            {
                canvasGroup.alpha = 0f;

                KnobValue = Mathf.Clamp(YValue, 0f, 100f);
                KnobValue /= 100f;

                UpdatePullableOrientation();
            }

            if (mouseEntered)
            {
                FColorMethods.LerpMaterialColor(markerMaterial, "_Color", Color.green);
            }
            else
            {
                FColorMethods.LerpMaterialColor(markerMaterial, "_Color", Color.black);
            }
        }

        protected override void OnExit()
        {
            markerMaterial.SetColor("_MainTex", Color.black);
            base.OnExit();
        }

    }

}