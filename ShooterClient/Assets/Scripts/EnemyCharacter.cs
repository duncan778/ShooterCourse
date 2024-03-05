using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
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

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + (velocity * averageInterval);
        velocityMagnitude = velocity.magnitude;
    }
}
