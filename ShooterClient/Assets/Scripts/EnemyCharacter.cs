using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform head;
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    private float velocityMagnitude = 0;

    private void Start()
    {
        TargetPosition = transform.position;
    }

    private void Update()
    {
        if (velocityMagnitude > 0.1f)
        {
            float maxDistance = velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else
            transform.position = TargetPosition;
    }

    public void SetSpeed(float value) => Speed = value;

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + (velocity * averageInterval);
        velocityMagnitude = velocity.magnitude;
        
        Velocity = velocity;
    }

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new(0, value, 0);
    }

    public void SetRotateX(float value)
    {
        head.localEulerAngles = new(value, 0, 0);
    }
}
