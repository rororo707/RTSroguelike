using Movement;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private IInputHandler inputHandler;
    private SelectableObject selectableObject;

    private MovementState movementState;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectableObject = GetComponent<SelectableObject>();
        inputHandler = new InputHandler();
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
    }
    public void HandleMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.point); // Move to clicked position
        }
    }
    public void StopMove()
    {
        agent.isStopped = true;
    }

}
