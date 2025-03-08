using Movement;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private IInputHandler inputHandler;
    private SelectableObject selectableObject;

    private MovementState movementState;
    private BannerBehavior bannerBehavior;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectableObject = GetComponent<SelectableObject>();
        inputHandler = new InputHandler();
        bannerBehavior = GetComponent<BannerBehavior>();
    }

    void Update()
    {
        if (selectableObject.IsSelected && inputHandler.IsMouseButtonDown(1)) // right click
        {
            HandleMove();
        }
        else if (selectableObject.IsSelected && inputHandler.IsKeyboardButtonDown(KeyCode.S)) // s stop key
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
            selectableObject.TargetPosition = hit.point;
            selectableObject.SetOutline(true); //runs banner logic
        }
    }
    public void StopMove()
    {
        agent.isStopped = true;
        selectableObject.TargetPosition = null;
        bannerBehavior.ClearBannerPath();
    }

    private void OnDestinationReached()
    {
        selectableObject.TargetPosition = null;
        bannerBehavior.ClearBannerPath();
    }

}
