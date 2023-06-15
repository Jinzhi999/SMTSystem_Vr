using UnityEngine;


namespace AmazingAssets.WireframeShader
{
    [ExecuteAlways]
    public class WireframeMaskController : MonoBehaviour
    {
        public enum MaskType { Plane, Sphere, Box }


        public MaskType maskType;
        public Transform maskObject;
        public Material[] materials;


        void Update()
        {
            if (maskObject != null && materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                        continue;


                    switch (maskType)
                    {
                        case MaskType.Plane:
                            materials[i].SetVector("_WireframeShaderMaskPlanePosition", maskObject.position);
                            materials[i].SetVector("_WireframeShaderMaskPlaneNormal", maskObject.up);
                            break;

                        case MaskType.Sphere:
                            materials[i].SetVector("_WireframeShaderMaskSpherePosition", maskObject.position);

                            float radius = Mathf.Abs(maskObject.localScale.x) * 0.5f;
                            materials[i].SetFloat("_WireframeShaderMaskSphereRadius", radius);
                            break;

                        case MaskType.Box:
                            Matrix4x4 matrixRTS = Matrix4x4.TRS(maskObject.position, maskObject.rotation, Vector3.one).inverse;
                            materials[i].SetMatrix("_WireframeShaderMaskBoxMatrixTRS", matrixRTS);

                            Vector3 boxBoundMin = new Vector3(-0.5f, -0.5f, -0.5f);
                            Vector3 localScale = maskObject.localScale;

                            //Keep all values positive
                            localScale.x *= localScale.x > 0 ? 1 : -1;
                            localScale.y *= localScale.y > 0 ? 1 : -1;
                            localScale.z *= localScale.z > 0 ? 1 : -1;


                            materials[i].SetVector("_WireframeShaderMaskBoxBoundingBox", Vector3.Scale(boxBoundMin, localScale));
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}