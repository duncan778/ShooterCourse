using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; protected set; } = 2;
    public Vector3 Velocity { get; protected set; }

    public float RotationY { get; protected set; }
    public float RotationX { get; protected set; }
    public float RotationVelocityY { get; protected set; }
    public float RotationVelocityX { get; protected set; }

}
