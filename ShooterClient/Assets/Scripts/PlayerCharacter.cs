using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed = 2f;
    private float inputH;
    private float inputV;

    public void SetInput(float h, float v)
    {
        inputH = h;
        inputV = v;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        // transform.position += direction * Time.deltaTime * speed;

        Vector3 velocity = (Vector3.forward * inputV + Vector3.right * inputH).normalized * speed;
        rigidbody.velocity = velocity;
    }
    
    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = rigidbody.velocity;
    }
}
