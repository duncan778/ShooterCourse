using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] private PlayerGun gun;
    [SerializeField] private float mouseSensetivity = 2f;

    private MultiplayerManager multiplayerManager;
    
    private bool cursorActivated;

    private void Start()
    {
        ActivateCursor(true);
        multiplayerManager = MultiplayerManager.Instance;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) ActivateCursor(false);
        else if (!cursorActivated && Input.GetMouseButtonDown(0)) ActivateCursor(true);
        
        if (cursorActivated == false) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool space = Input.GetKeyDown(KeyCode.Space);

        bool isShoot = Input.GetMouseButton(0);

        float rotationVelocityY = mouseX * mouseSensetivity;
        player.SetInput(h, v, rotationVelocityY);
        rotationVelocityY /= Time.deltaTime;

        float rotationVelocityX = -mouseY * mouseSensetivity;
        player.RotateX(rotationVelocityX);
        rotationVelocityX /= Time.deltaTime;
        
        if (space) player.Jump();
        if (isShoot && gun.TryShoot(out ShootInfo shootInfo)) SendShoot(ref shootInfo);
        
        SendMove(rotationVelocityX, rotationVelocityY);
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = multiplayerManager.GetSessionId();
        string json = JsonUtility.ToJson(shootInfo);
        multiplayerManager.SendMessage("shoot", json);
    }

    private void SendMove(float rotationVelocityX, float rotationVelocityY)
    {
        player.GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY);
        var data = new Dictionary<string, object>()
        {
            { "pX", position.x},
            { "pY", position.y},
            { "pZ", position.z},
            { "vX", velocity.x},
            { "vY", velocity.y},
            { "vZ", velocity.z},
            { "rX", rotateX },
            { "rY", rotateY },
            { "rvX", rotationVelocityX },
            { "rvY", rotationVelocityY },
        };
        multiplayerManager.SendMessage("move", data);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
            ActivateCursor(true);
        else    
            ActivateCursor(false);
    }

    private void OnDisable()
    {
        ActivateCursor(false);
    }

    private void ActivateCursor(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorActivated = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            cursorActivated = false;
        }
    }

}

[System.Serializable]
public struct ShootInfo
{
    public string key;
    public float pX;
    public float pY;
    public float pZ;
    public float dX;
    public float dY;
    public float dZ;
    
}
