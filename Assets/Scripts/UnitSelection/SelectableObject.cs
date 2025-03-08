using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private Renderer renderer;
    private Material baseMaterial;
    [SerializeField]
    private Material highlightMaterial;

    public bool IsSelected { get; set; } = false;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseMaterial = renderer.material;
    }

    public void SetOutline(bool enabled)
    {
        renderer.material = enabled ? highlightMaterial : baseMaterial;
        IsSelected = enabled;
    }
}