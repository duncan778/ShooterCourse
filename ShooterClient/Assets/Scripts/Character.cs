using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; protected set; } = 2;
    public Vector3 Velocity { get; protected set; }
}
