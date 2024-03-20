using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter character;
    [SerializeField] private EnemyGun gun;
    private List<float> receiveTimeInterval = new List<float> {0, 0, 0, 0, 0};
    private float lastReceiveTime = 0;
    private Player player;

    public void Init(string key, Player player)
    {
        character.Init(key);

        player.OnChange += OnChange;
        character.SetSpeed(player.speed);
        character.SetMaxHP(player.maxHP);
        this.player = player;
    }

    public void Shoot(in ShootInfo info)
    {
        Vector3 position = new (info.pX, info.pY, info.pZ);
        Vector3 velocity = new (info.dX, info.dY, info.dZ);

        gun.Shoot(position, velocity);
    }

    public void Destroy()
    {
        player.OnChange -= OnChange;
        Destroy(gameObject);
    }

    public float AverageInterval
    {
        get 
        {
            int receiveTimeIntervalCount = receiveTimeInterval.Count;
            float summ = 0;
            for (int i = 0; i < receiveTimeInterval.Count; i++)
                summ += receiveTimeInterval[i];

            return summ / receiveTimeIntervalCount;
        }
    }

    private void SaveReceiveTime()
    {
        float interval = Time.time - lastReceiveTime;
        lastReceiveTime = Time.time;

        receiveTimeInterval.Add(interval);
        receiveTimeInterval.RemoveAt(0);
    }

    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        Vector3 position = character.TargetPosition;
        Vector3 velocity = character.Velocity;
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance.LossCounter.SetEnemyLoss((ushort)dataChange.Value);
                    break;
                case "currentHP":
                    if ((sbyte)dataChange.Value > (sbyte)dataChange.PreviousValue)
                        character.RestoreHP((sbyte)dataChange.Value);
                    break;
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                case "rX":
                    character.SetRotateX((float)dataChange.Value);
                    break;
                case "rY":
                    character.SetRotateY((float)dataChange.Value);
                    break;

                default:
                    Debug.LogWarning("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }
        }
        character.SetMovement(position, velocity, AverageInterval);
    }
}
