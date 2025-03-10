using Movement;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private IInputHandler inputHandler;
    private SelectableUnit selectableUnit;

    private MovementState movementState;
    private BannerBehavior bannerBehavior;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectableUnit = GetComponent<SelectableUnit>();
        inputHandler = new InputHandler();
        bannerBehavior = GetComponent<BannerBehavior>();
    }

    void Update()
    {
        if (selectableUnit.IsSelected && inputHandler.IsMouseButtonDown(1)) // right click
        {
            HandleMove();
        }
        else if (selectableUnit.IsSelected && inputHandler.IsKeyboardButtonDown(KeyCode.S)) // s stop key
        {
            StopMove();
        }
        else if (agent.remainingDistance <= bannerBehavior.BannerClearRadius)
        {
            OnDestinationReached();
        }
    }
    public void HandleMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.point); // Move to clicked position
            selectableUnit.TargetPosition = hit.point;
            selectableUnit.DisplayBannerPath();
        }
    }
    public void StopMove()
    {
        agent.isStopped = true;
        selectableUnit.TargetPosition = null;
        bannerBehavior.ClearBannerPath();
    }

    private void OnDestinationReached()
    {
        selectableUnit.TargetPosition = null;
        bannerBehavior.ClearBannerPath();
    }

}
