using UnityEngine;

public class SelectionPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject planePrefab; // Assign a GameObject with MeshFilter in the Inspector
    private GameObject planeInstance;

    public void ModifyPlaneMesh(Vector3[] newVertices, bool invertTriangleOrientation)
    {
        if (newVertices == null || newVertices.Length != 4)
        {
            Debug.LogError("Invalid vertices.");
            return;
        }
        if (planePrefab == null)
        {
            Debug.LogError("Plane prefab is not assigned.");
            return;
        }
        if (planeInstance == null)
        {
            planeInstance = Instantiate(planePrefab, Vector3.zero, Quaternion.identity);
        }


        MeshFilter meshFilter = planeInstance.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component is missing on the plane.");
            return;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = newVertices;

        if (invertTriangleOrientation)
        {
            // Define triangles (two triangles forming a quad)
            mesh.triangles = new int[]
            {
            0, 1, 2,  // First triangle
            2, 1, 3,    // Second triangle
            };
        }
        else
        {
            mesh.triangles = new int[]
            {
                0, 2, 1,  // First triangle
                2, 3, 1   // Second triangle
            };
        }






        // Define UV coordinates for textures
        /*mesh.uv = new Vector2[]
        {
            new Vector2(0, 0), // Bottom-left
            new Vector2(1, 0), // Bottom-right
            new Vector2(0, 1), // Top-left
            new Vector2(1, 1)  // Top-right
        };*/

        //mesh.RecalculateNormals(); // Fixes lighting
        meshFilter.mesh = mesh; // Apply new mesh to object
    }
    public void DestroyPlane()
    {
        if (planeInstance != null)
        {
            Destroy(planeInstance);
            planeInstance = null;
        }
    }
}