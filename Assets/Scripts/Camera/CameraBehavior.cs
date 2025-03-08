using DragBox;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera cam;
    private InputHandler inputHandler;
    private DrawingState drawingState;
    private Plane drawingPlane;

    [SerializeField]
    private int dragVelocityMultiplier = 1;
    [SerializeField]
    private float edgeThreshold = 15f; // Distance from the edge to start panning
    [SerializeField]
    private float edgePanningSpeed = 10f; // Speed of edge panning

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = GetComponent<Camera>();

        inputHandler = new InputHandler();
        drawingState = new DrawingState();

        drawingPlane = new Plane(Vector3.up, Vector3.up * 1); // Plane at y = 1
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.IsMouseButtonHeldDown(2))
        {
            Cursor.visible = false;
            HandleDragging();
        }
        else if (inputHandler.IsMouseButtonUp(2))
        {
            Cursor.visible = true;
            drawingState.Reset();
        }
        else
        {
            HandleKeyboardInput();
            HandleEdgePanning();
        }
    }
    private void HandleKeyboardInput()
    {
        if (inputHandler.IsKeyboardButtonHeldDown(KeyCode.UpArrow))
        {
            cam.transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
        }
        if (inputHandler.IsKeyboardButtonHeldDown(KeyCode.DownArrow))
        {
            cam.transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
        }
        if (inputHandler.IsKeyboardButtonHeldDown(KeyCode.LeftArrow))
        {
            cam.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
        }
        if (inputHandler.IsKeyboardButtonHeldDown(KeyCode.RightArrow))
        {
            cam.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        }
    }
    private void HandleEdgePanning()
    {
        Vector3 mousePosition = inputHandler.GetMousePosition();

        if (mousePosition.x <= edgeThreshold)
        {
            cam.transform.position += edgePanningSpeed * Time.deltaTime * new Vector3(-1, 0, 0);
        }
        if (mousePosition.x >= Screen.width - edgeThreshold)
        {
            cam.transform.position += edgePanningSpeed * Time.deltaTime * new Vector3(1, 0, 0);
        }
        if (mousePosition.y <= edgeThreshold)
        {
            cam.transform.position += edgePanningSpeed * Time.deltaTime * new Vector3(0, 0, -1);
        }
        if (mousePosition.y >= Screen.height - edgeThreshold)
        {
            cam.transform.position += edgePanningSpeed * Time.deltaTime * new Vector3(0, 0, 1);
        }
    }
    private void HandleDragging()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputHandler.GetMousePosition());
        if (drawingPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            if (drawingState.InitialPosition == Vector3.zero)
            {
                drawingState.InitialPosition = new Vector3(hitPoint.x, 1, hitPoint.z);
            }
            else
            {
                drawingState.CurrentPosition = new Vector3(hitPoint.x, 1, hitPoint.z);
            }
            Vector3 diff = drawingState.InitialPosition - drawingState.CurrentPosition;
            cam.transform.position += dragVelocityMultiplier * Time.deltaTime * diff;
        }
    }
}
