using System;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private Health health;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform head;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private float maxHeadAngle = 90;
    [SerializeField] private float minHeadAngle = -90;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private CheckFly checkFly;
    [SerializeField] private float jumpDelay = 0.2f;

    private float jumpTime;
    private float inputH;
    private float inputV;
    private float rotateY;
    private float currentRotateX;

    private void Start()
    {
        Transform camera = Camera.main.transform;
        camera.parent = cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;
        health.SetMax(MaxHealth);
        health.SetCurrent(MaxHealth);
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

        Vector3 velocity = (transform.forward * inputV + transform.right * inputH).normalized * Speed;
        velocity.y = playerRb.velocity.y;
        Velocity = velocity;
        playerRb.velocity = velocity;
    }
    
    public void GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY)
    {
        position = transform.position;
        velocity = playerRb.velocity;
        rotateX = head.localEulerAngles.x;
        rotateY = transform.eulerAngles.y;
    }

    public void Jump()
    {
        if (checkFly.IsFly) return;
        if (Time.time - jumpTime < jumpDelay) return;

        jumpTime = Time.time;
        playerRb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }

    internal void OnChange(List<DataChange> changes)
    {
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance.lossCounter.SetPlayerLoss((ushort)dataChange.Value);
                    break;
                case "currentHP":
                    health.SetCurrent((sbyte)dataChange.Value);
                    break;

                default:
                    //Debug.LogWarning("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }
        }
    }

}