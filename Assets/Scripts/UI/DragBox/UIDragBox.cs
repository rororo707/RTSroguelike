using DragBox;
using UnityEngine;

public class UIDragBox : MonoBehaviour
{

    private Canvas canvas;
    private DragBoxBehavior dragBoxBehavior;
    private GameObject gameManager;

    private IInputHandler inputHandler;
    private ShapeDrawer shapeDrawer;
    private DrawingState drawingState;

    private Plane drawingPlane;

    [SerializeField]
    private float minimumSelectRadius = 0.5f;
    [SerializeField]
    private Transform lineContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        dragBoxBehavior = gameManager.GetComponent<DragBoxBehavior>();

        shapeDrawer = new ShapeDrawer(GetComponent<LineRenderer>());

        inputHandler = new InputHandler();
        drawingState = new DrawingState();

        drawingPlane = new Plane(Vector3.forward.normalized,
            (Vector3.forward)
            );

        // Find the Canvas
        canvas = GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas not found in the scene.");
        }
    }
    void Update()
    {
        if (inputHandler.IsMouseButtonHeldDown(0))
        {
            HandleDrawing();
        }
    }
    void HandleDrawing()
    {
        // Get mouse position in screen space
        /*Vector2 screenPosition = inputHandler.GetMousePosition();

        // Convert screen position to UI canvas position
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            lineContainer as RectTransform,
            screenPosition,
            null,
            out anchoredPosition
        );*/
        Vector3 anchoredPosition = Camera.main.ScreenToWorldPoint(inputHandler.GetMousePosition());
        anchoredPosition.z = 0;
        if (drawingState.InitialPosition == Vector3.zero)
        {
            drawingState.InitialPosition = anchoredPosition;
            return;
        }
        drawingState.CurrentPosition = anchoredPosition;
        /*drawingState.Corner2 = new Vector3(drawingState.InitialPosition.x, planeHeight, drawingState.CurrentPosition.z);
        drawingState.Corner4 = new Vector3(drawingState.CurrentPosition.x, planeHeight, drawingState.InitialPosition.z);*/

        Vector3[] positions = new Vector3[]
        {
                    drawingState.InitialPosition,
                    //drawingState.Corner2,
                    drawingState.CurrentPosition,
                    //drawingState.Corner4,
                    //drawingState.InitialPosition
        };

        shapeDrawer.DrawLine(positions);
    }
}
