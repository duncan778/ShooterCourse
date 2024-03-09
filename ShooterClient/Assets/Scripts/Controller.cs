using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] private float mouseSensetivity = 2f;
    
    private bool cursorActivated;

    private void Start()
    {
        ActivateCursor(true);
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

        player.SetInput(h, v, mouseX * mouseSensetivity);
        player.RotateX(-mouseY * mouseSensetivity);

        if (space) player.Jump();

        SendMove();
    }

    private void SendMove()
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
            { "rY", rotateY }
        };
        MultiplayerManager.Instance.SendMessage("move", data);
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
