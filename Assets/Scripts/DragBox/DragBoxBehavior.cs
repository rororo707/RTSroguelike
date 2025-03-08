using DragBox;
using System.Collections.Generic;
using UnityEngine;
// Main class to handle drawing logic
public class DragBoxBehavior : MonoBehaviour
{
    private IInputHandler inputHandler;
    private ShapeDrawer shapeDrawer;
    private DrawingState drawingState;
    private Plane drawingPlane;
    private List<GameObject> selectedUnits;

    [SerializeField]
    private float minimumSelectRadius = 0.5f;


    //TODO add ctrl click, shift click logic! (eventually lol)

    void Start()
    {
        // Initialize dependencies
        inputHandler = new InputHandler();
        shapeDrawer = new ShapeDrawer(GetComponent<LineRenderer>());
        drawingState = new DrawingState();
        drawingPlane = new Plane(Vector3.up, Vector3.up * 1); // Plane at y = 1

        selectedUnits = new List<GameObject>();
        // If the LineRenderer is not found, log an error
        if (shapeDrawer == null)
        {
            Debug.LogError("LineRenderer component is missing from the GameObject.");
        }
    }

    void Update()
    {
        if (inputHandler.IsMouseButtonDown(0))
        {
            HandleDrawing();
        }
        else if (inputHandler.IsMouseButtonUp(0))
        {
            HandleSelectUnits();
            shapeDrawer.ClearLine();
            drawingState.Reset();
        }
    }
    private void HandleSelectUnits()
    {

        // Get all selectable units in the scene
        GameObject[] selectableUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject unit in selectedUnits)
        {
            unit.GetComponent<SelectableObject>().SetOutline(false);
        }

        if (drawingState.GetMagnitude() < minimumSelectRadius)
        {
            Ray ray = Camera.main.ScreenPointToRay(inputHandler.GetMousePosition());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Player"))
                {
                    selectedUnits.Add(clickedObject);
                    clickedObject.GetComponent<SelectableObject>().SetOutline(true);
                    Debug.Log("Clicked on unit: " + clickedObject.name);
                }
            }
            return;
        }
        foreach (GameObject unit in selectableUnits)
        {
            Vector3 unitPosition = unit.transform.position;

            if (drawingState.IsWithinSelectionBounds(unitPosition))
            {
                selectedUnits.Add(unit);
                // You can add logic here to highlight or mark the selected units
                Debug.Log("Selected unit: " + unit.name);
                unit.GetComponent<SelectableObject>().SetOutline(true);
            }
        }

        // Handle the selected units as needed
    }


    private void HandleDrawing()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputHandler.GetMousePosition());
        if (drawingPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            if (drawingState.InitialPosition == Vector3.zero)
            {
                drawingState.InitialPosition = new Vector3(hitPoint.x, 1, hitPoint.z);
                return;
            }
            drawingState.CurrentPosition = new Vector3(hitPoint.x, 1, hitPoint.z);
            drawingState.Corner2 = new Vector3(drawingState.InitialPosition.x, 1, drawingState.CurrentPosition.z);
            drawingState.Corner4 = new Vector3(drawingState.CurrentPosition.x, 1, drawingState.InitialPosition.z);

            Vector3[] positions = new Vector3[]
            {
                    drawingState.InitialPosition,
                    drawingState.Corner2,
                    drawingState.CurrentPosition,
                    drawingState.Corner4,
                    drawingState.InitialPosition
            };

            if (drawingState.GetMagnitude() >= minimumSelectRadius)
            {
                shapeDrawer.DrawLine(positions);
            }

        }
    }
}
