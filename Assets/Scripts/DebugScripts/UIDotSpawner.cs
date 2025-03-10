using UnityEngine;
using UnityEngine.EventSystems;

public class UIDotSpawner : MonoBehaviour
{
    public GameObject dotPrefab; // Assign a UI Image prefab
    public Transform dotsContainer; // Parent container inside the Canvas

    void Update()
    {
        // Check if the left mouse button is clicked and not over UI elements
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            SpawnDotAtMousePosition();
        }
    }

    void SpawnDotAtMousePosition()
    {
        // Get mouse position in screen space
        Vector2 screenPosition = Input.mousePosition;

        // Convert screen position to UI canvas position
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dotsContainer as RectTransform,
            screenPosition,
            null,
            out anchoredPosition
        );

        // Instantiate the dot inside the UI Canvas
        GameObject newDot = Instantiate(dotPrefab, dotsContainer);
        newDot.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
}
