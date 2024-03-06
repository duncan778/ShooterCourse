using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform head;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private float maxHeadAngle = 90;
    [SerializeField] private float minHeadAngle = -90;
    [SerializeField] private float jumpForce = 50f;

    private float inputH;
    private float inputV;
    private float rotateY;
    private float currentRotateX;
    private bool isFly = true;

    private void Start()
    {
        Transform camera = Camera.main.transform;
        camera.parent = cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;
    }

    public void RotateX(float value)
    {
        currentRotateX = Mathf.Clamp(currentRotateX + value, minHeadAngle, maxHeadAngle);
        head.localEulerAngles = new (currentRotateX, 0, 0);
    }

    private void RotateY()
    {
        playerRb.angularVelocity = new Vector3(0, rotateY, 0);
        rotateY = 0;
    }

    public void SetInput(float h, float v, float rotateY)
    {
        inputH = h;
        inputV = v;
        this.rotateY += rotateY;
    }

    void FixedUpdate()
    {
        Move();
        RotateY();
    }

    void Move()
    {
        // Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        // transform.position += direction * Time.deltaTime * speed;

        Vector3 velocity = (transform.forward * inputV + transform.right * inputH).normalized * speed;
        velocity.y = playerRb.velocity.y;
        playerRb.velocity = velocity;
    }
    
    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = playerRb.velocity;
    }

    private void OnCollisionStay(Collision other)
    {
        var contactPoints = other.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (contactPoints[i].normal.y > 0.45f) isFly = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isFly = true;
    }

    public void Jump()
    {
        if (isFly) return;

        playerRb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }
}