using UnityEngine;

public class ClickToSpawnDot : MonoBehaviour
{
    public GameObject dotPrefab; // Assign a prefab (like a small sphere) in the Inspector
    public float dotSize = 0.1f; // Size of the dot

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            SpawnDotAtMousePosition();
        }
    }

    void SpawnDotAtMousePosition()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // If we hit something with a collider
        {
            // Spawn a dot at the hit point
            GameObject newDot = Instantiate(dotPrefab, hit.point, Quaternion.identity);

            // Resize the dot if needed
            newDot.transform.localScale = Vector3.one * dotSize;
        }
    }
}
