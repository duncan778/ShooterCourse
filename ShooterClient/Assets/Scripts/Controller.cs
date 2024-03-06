using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] private float mouseSensetivity = 2f;

    void Update()
    {
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
        player.GetMoveInfo(out Vector3 position, out Vector3 velocity);
        var data = new Dictionary<string, object>()
        {
            { "pX", position.x},
            { "pY", position.y},
            { "pZ", position.x},
            { "vX", position.x},
            { "vY", position.y},
            { "vZ", position.z},
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
