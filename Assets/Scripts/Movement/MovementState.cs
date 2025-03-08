using UnityEngine;

namespace Movement
{
    public class MovementState
    {
        public Vector3 TargetPosition { get; set; } = Vector3.zero;
        public Vector3 CurrentVelocity { get; set; } = Vector3.zero;

    }
}