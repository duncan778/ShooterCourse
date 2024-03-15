using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float restartDelay = 3f;
    [SerializeField] PlayerCharacter player;
    [SerializeField] private PlayerGun gun;
    [SerializeField] private float mouseSensetivity = 2f;

    private MultiplayerManager multiplayerManager;
    private bool hold = false;
    private bool cursorActivated;

    private void Start()
    {
        ActivateCursor(true);
        multiplayerManager = MultiplayerManager.Instance;
    }


    void Update()
    {
        if (hold) return;

        if (Input.GetKey(KeyCode.Escape)) ActivateCursor(false);
        else if (!cursorActivated && Input.GetMouseButtonDown(0)) ActivateCursor(true);
        
        if (cursorActivated == false) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool space = Input.GetKeyDown(KeyCode.Space);
        bool isShoot = Input.GetMouseButton(0);
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
    

        player.SetInput(h, v, mouseX * mouseSensetivity);
        player.RotateX(-mouseY * mouseSensetivity);
        if (space) player.Jump();
        if (isShoot && gun.TryShoot(out ShootInfo shootInfo)) SendShoot(ref shootInfo);
        if (mouseWheel != 0) gun.ChangeGun(mouseWheel);
        SendMove();
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = multiplayerManager.GetSessionId();
        string json = JsonUtility.ToJson(shootInfo);
        multiplayerManager.SendMessage("shoot", json);
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

    public void Restart(string jsonRestartInfo)
    {
        RestartInfo info = JsonUtility.FromJson<RestartInfo>(jsonRestartInfo);
        StartCoroutine(Hold());

        player.transform.position = new(info.x, 0, info.z);
        player.SetInput(0, 0, 0);
        
        var data = new Dictionary<string, object>()
        {
            { "pX", info.x },
            { "pY", 0 },
            { "pZ", info.z },
            { "vX", 0 },
            { "vY", 0 },
            { "vZ", 0 },
            { "rX", 0 },
            { "rY", 0 }
        };
        multiplayerManager.SendMessage("move", data);
    }

    private IEnumerator Hold()
    {
        hold = true;
        yield return new WaitForSecondsRealtime(restartDelay);
        hold = false;
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

[System.Serializable]
public struct RestartInfo
{
    public float x;
    public float z;
}
