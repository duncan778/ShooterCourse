using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] PlayerCharacter _player;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _player.SetInput(h, v);

        SendMove();
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity);
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
