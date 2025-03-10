using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;

    private BannerBehavior bannerBehavior;
    public bool IsSelected { get; set; } = false;
    public Vector3? TargetPosition { get; set; } = null;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        bannerBehavior = GetComponent<BannerBehavior>();
    }
    void Update()
    {
        if (IsSelected && TargetPosition != null)
        {
            bannerBehavior.DisplayPath();
        }
    }
    public void MoveTo(Vector3 Position)
    {
        Agent.SetDestination(Position);
    }
    public void OnSelected()
    {
        IsSelected = true;
        SelectionSprite.gameObject.SetActive(true);
        DisplayBannerPath();
    }
    public void DisplayBannerPath()
    {
        if (TargetPosition != null)
        {
            bannerBehavior.DisplayBannerPath((Vector3)TargetPosition);
        }
        else
        {
            bannerBehavior.ClearBannerPath();
        }
    }
    public void OnDeselected()
    {
        IsSelected = false;
        SelectionSprite.gameObject.SetActive(false);
        bannerBehavior.ClearBannerPath();
    }
}
