using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private Renderer renderer;
    private Material baseMaterial;
    private BannerBehavior bannerBehavior;

    [SerializeField]
    private Material highlightMaterial;

    public bool IsSelected { get; set; } = false;
    public Vector3? TargetPosition { get; set; } = null;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseMaterial = renderer.material;
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
        renderer.material = enabled ? highlightMaterial : baseMaterial;
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