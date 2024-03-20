using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Health health;
    [SerializeField] private Transform head;
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    private float velocityMagnitude = 0;
    private string sessionID;

    private void Start()
    {
        TargetPosition = transform.position;
    }

    public void Init(string sessionID)
    {
        this.sessionID = sessionID;
    }

    private void Update()
    {
        if (velocityMagnitude > 0.1f)
        {
            float maxDistance = velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else
            transform.position = TargetPosition;
    }

    public void SetSpeed(float value) => Speed = value;
    public void SetMaxHP(int value)
    {
        MaxHealth = value;
        health.SetMax(value);
        health.SetCurrent(value);
    }

    public void RestoreHP(int newValue)
    {
        health.SetCurrent(newValue);
        head.GetComponent<EnemyHead>().HideHead(false);
    }

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + (velocity * averageInterval);
        velocityMagnitude = velocity.magnitude;
        
        Velocity = velocity;
    }

    public void ApplyDamage(int damage)
    {
        health.ApplyDamage(damage);
        Dictionary<string, object> data = new() 
        {
            {"id", sessionID},
            {"value", damage}
        };
        MultiplayerManager.Instance.SendMessage("damage", data);
        RestoreHP(MaxHealth);
    }

    public void ApplyFullDamage()
    {
        ApplyDamage(MaxHealth);
    }

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new(0, value, 0);
    }

    public void SetRotateX(float value)
    {
        head.localEulerAngles = new(value, 0, 0);
    }
}
