using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float inputH;
    private float inputV;

    public void SetInput(float h, float v)
    {
        inputH = h;
        inputV = v;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        transform.position += direction * Time.deltaTime * speed;
    }
    
    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
}
