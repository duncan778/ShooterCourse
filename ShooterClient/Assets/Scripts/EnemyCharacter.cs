using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform head;
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    public float TargetRotationY { get; private set; } = 0;
    public float TargetRotationX { get; private set; } = 0;

    private float velocityMagnitude = 0;

    private void Start()
    {
        TargetPosition = transform.position;
        TargetRotationY = transform.eulerAngles.y;
        TargetRotationX = head.localEulerAngles.x;
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

        if (Mathf.Abs(RotationVelocityY) > 0.01f)
            transform.localEulerAngles = Quaternion.Lerp(transform.localRotation, 
                                                        Quaternion.Euler(new(0, TargetRotationY, 0)), 
                                                        Time.deltaTime * Mathf.Abs(RotationVelocityY)).eulerAngles;
        else
            transform.localEulerAngles = new(0, TargetRotationY, 0);

        if (Mathf.Abs(RotationVelocityX) > 0.01f)
            head.localEulerAngles = Quaternion.Lerp(head.localRotation, 
                                                    Quaternion.Euler(new(TargetRotationX, 0, 0)), 
                                                    Time.deltaTime * Mathf.Abs(RotationVelocityX)).eulerAngles;
        else
            head.localEulerAngles = new(TargetRotationX, 0, 0);
    }

    public void SetSpeed(float value) => Speed = value;

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + (velocity * averageInterval);
        velocityMagnitude = velocity.magnitude;
        
        Velocity = velocity;
    }

    public void SetRotateY(float rotationY, float rotationVelocityY, float averageInterval)
    {
        TargetRotationY = rotationY + (rotationVelocityY * Time.deltaTime * averageInterval);
        
        RotationY = rotationY;
        RotationVelocityY = rotationVelocityY;
    }

    public void SetRotateX(float rotationX, float rotationVelocityX, float averageInterval)
    {
        TargetRotationX = rotationX + (-rotationVelocityX * Time.deltaTime * averageInterval);

        RotationX = rotationX;
        RotationVelocityX = rotationVelocityX;
    }
}
