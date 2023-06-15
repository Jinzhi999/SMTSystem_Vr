using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AmazingAssets.WireframeShader.Example
{
    [ExecuteAlways]
    public class UpdateGlobalIllumination : MonoBehaviour
    {
        new public Renderer renderer;
        public Material wireframeMaterial;


        public void OnUIColorChange(float value)
        {
            if (wireframeMaterial != null)
                wireframeMaterial.SetColor("_Wireframe_Color", Color.HSVToRGB(value, 1, 1));

            if (renderer != null)
                RendererExtensions.UpdateGIMaterials(renderer);
        }
    }
}