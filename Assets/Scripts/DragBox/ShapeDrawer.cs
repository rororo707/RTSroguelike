using UnityEngine;

namespace DragBox
{
    // Interface for drawing lines
    public interface IShapeDrawer
    {
        void DrawLine(Vector3[] positions);
        void ClearLine();
    }
    // Concrete implementation of line drawer
    public class ShapeDrawer : IShapeDrawer
    {
        private readonly LineRenderer lineRenderer;

        public ShapeDrawer(LineRenderer lineRenderer)
        {
            this.lineRenderer = lineRenderer;
        }

        public void DrawLine(Vector3[] positions)
        {
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        public void ClearLine()
        {
            lineRenderer.positionCount = 0;
        }
    }
}
