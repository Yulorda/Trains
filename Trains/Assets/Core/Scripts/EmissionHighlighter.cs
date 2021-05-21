using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EmissionHighlighter : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public Color color;

    public void Highlight(bool state)
    {
        if (state)
        {
            meshRenderer.material.EnableKeyword("_EMISSION");
            meshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.AnyEmissive;
            meshRenderer.material.SetColor("_EmissionColor", color);
        }
        else
        {
            meshRenderer.material.DisableKeyword("_EMISSION");
            meshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            meshRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
