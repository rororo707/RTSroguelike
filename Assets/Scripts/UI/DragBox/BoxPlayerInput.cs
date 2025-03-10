using UnityEngine;

public class BoxPlayerInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers;
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]
    private float DragDelay = 0.1f;

    private Vector2 StartMousePosition;
    private float MouseDownTime;
    private IInputHandler inputHandler;
    void Start()
    {
        inputHandler = new InputHandler();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSelectionInputs();
    }
    private void HandleSelectionInputs()
    {
        if (inputHandler.IsMouseButtonDown(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = inputHandler.GetMousePosition();
            MouseDownTime = Time.time;
        }
        else if (inputHandler.IsMouseButtonHeldDown(0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (inputHandler.IsMouseButtonUp(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            if (Physics.Raycast(Camera.ScreenPointToRay(inputHandler.GetMousePosition()), out RaycastHit hit, UnitLayers)
                && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {
                if (inputHandler.IsKeyboardButtonHeldDown(KeyCode.LeftShift) || inputHandler.IsKeyboardButtonHeldDown(KeyCode.RightShift))
                {
                    if (SelectionManager.Instance.IsSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    }
                    else
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else
                {
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }
            MouseDownTime = 0;
        }
    }
    private void ResizeSelectionBox()
    {
        float width = inputHandler.GetMousePosition().x - StartMousePosition.x;
        float height = inputHandler.GetMousePosition().y - StartMousePosition.y;

        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, height / 2);

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            SelectableUnit unit = SelectionManager.Instance.AvailableUnits[i];
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(unit.transform.position), bounds))
            {
                if (!SelectionManager.Instance.IsSelected(unit))
                {
                    SelectionManager.Instance.Select(unit);
                }
            }
            else
            {
                if (SelectionManager.Instance.IsSelected(unit))
                {
                    SelectionManager.Instance.Deselect(unit);
                }
            }
        }
    }
    private bool UnitIsInSelectionBox(Vector2 unitPosition, Bounds bounds)
    {
        return unitPosition.x > bounds.min.x && unitPosition.x < bounds.max.x &&
               unitPosition.y > bounds.min.y && unitPosition.y < bounds.max.y;
    }
}
