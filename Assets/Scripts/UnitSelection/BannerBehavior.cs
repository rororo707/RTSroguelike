using DragBox;
using UnityEngine;
public class BannerBehavior : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private DrawingState drawingState;
    private Plane drawingPlane;
    private ShapeDrawer shapeDrawer;
    private IInputHandler inputHandler;
    private Vector3 currentTarget;

    [SerializeField]
    private GameObject bannerPrefab;
    private GameObject bannerInstance;

    [SerializeField]
    private Vector3 VerticalBuffer = Vector3.up;

    [SerializeField]
    public float BannerClearRadius = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        drawingState = new DrawingState();

        drawingPlane = new Plane(Vector3.up, Vector3.up * 1); // Plane at y = 1
        inputHandler = new InputHandler();
        shapeDrawer = new ShapeDrawer(lineRenderer);
    }

    public void DisplayBannerPath(Vector3 targetPosition)
    {
        currentTarget = targetPosition;
        DisplayBanner();
        DisplayPath();
    }
    public void DisplayBanner()
    {
        if (bannerInstance == null)
        {
            bannerInstance = Instantiate(bannerPrefab, currentTarget + VerticalBuffer, Quaternion.identity);
        }
        else
        {
            bannerInstance.transform.position = currentTarget + VerticalBuffer;
        }
    }
    public void DisplayPath()
    {
        shapeDrawer.ClearLine();
        drawingState.InitialPosition = new Vector3(transform.position.x, 0.1f, transform.position.z);
        drawingState.CurrentPosition = new Vector3(currentTarget.x, 0.1f, currentTarget.z);

        Vector3[] positions = new Vector3[]
        {
                    drawingState.InitialPosition,
                    drawingState.CurrentPosition
        };

        shapeDrawer.DrawLine(positions);

    }
    public void ClearBannerPath()
    {
        ClearBanner();
        ClearPath();
        currentTarget = Vector3.zero;
    }

    public void ClearBanner()
    {
        if (bannerInstance != null)
        {
            Destroy(bannerInstance);
            bannerInstance = null;
        }
    }
    public void ClearPath()
    {
        shapeDrawer.ClearLine();
    }
}
