using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter character;
    private List<float> receiveTimeInterval = new List<float> {0, 0, 0, 0, 0};
    private float lastReceiveTime = 0;
    private Player player;

    public void Init(Player player)
    {
        player.OnChange += OnChange;
        character.SetSpeed(player.speed);
        this.player = player;
    }

    public void Shoot(in ShootInfo info)
    {
        
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
