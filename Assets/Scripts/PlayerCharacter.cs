using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 2f;
    private float _inputH;
    private float _inputV;

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Vector3 direction = new Vector3(_inputH, 0, _inputV).normalized;
        // transform.position += direction * Time.deltaTime * _speed;

        Vector3 velocity = (Vector3.forward * _inputV + Vector3.right * _inputH).normalized * _speed;
        _rigidbody.velocity = velocity;
    }
    
    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
}
