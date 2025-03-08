using UnityEngine;

// Class responsible for managing the drawing state
namespace DragBox
{
    public class DrawingState
    {
        public Vector3 InitialPosition { get; set; } = Vector3.zero;
        public Vector3 CurrentPosition { get; set; } = Vector3.zero;
        public Vector3 Corner2 { get; set; } = Vector3.zero;
        public Vector3 Corner4 { get; set; } = Vector3.zero;

        public void Reset()
        {
            InitialPosition = Vector3.zero;
            CurrentPosition = Vector3.zero;
            Corner2 = Vector3.zero;
            Corner4 = Vector3.zero;
        }
        public double GetMagnitude()
        {
            // Calculate the magnitude of the selection box
            return Vector3.Distance(InitialPosition, CurrentPosition);
        }
        public bool IsWithinSelectionBounds(Vector3 position)
        {
            // Check if the position is within the rectangle defined by the drawing state
            bool xMatch;
            bool zMatch;
            if (InitialPosition.x < CurrentPosition.x)
            {
                xMatch = position.x >= InitialPosition.x && position.x <= CurrentPosition.x;
            }
            else
            {
                xMatch = position.x <= InitialPosition.x && position.x >= CurrentPosition.x;
            }
            if (InitialPosition.z < CurrentPosition.z)
            {
                zMatch = position.z >= InitialPosition.z && position.z <= CurrentPosition.z;

            }
            else
            {
                zMatch = position.z <= InitialPosition.z && position.z >= CurrentPosition.z;
            }
            return xMatch && zMatch;

        }
    }
}
