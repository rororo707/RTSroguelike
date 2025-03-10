using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private Renderer materialRenderer;
    private Material baseMaterial;
    private BannerBehavior bannerBehavior;

    [SerializeField]
    private Material highlightMaterial;

    public bool IsSelected { get; set; } = false;
    public Vector3? TargetPosition { get; set; } = null;

    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        baseMaterial = materialRenderer.material;
        bannerBehavior = GetComponent<BannerBehavior>();
    }
    void Update()
    {
        if (IsSelected && TargetPosition != null)
        {
            bannerBehavior.DisplayPath();
        }
    }
    public void SetOutline(bool enabled)
    {
        materialRenderer.material = enabled ? highlightMaterial : baseMaterial;
        IsSelected = enabled;
        if (enabled && TargetPosition != null)
        {
            bannerBehavior.DisplayBannerPath((Vector3)TargetPosition);
        }
        else
        {
            bannerBehavior.ClearBannerPath();
        }
    }
}