using UnityEngine;
using Colyseus;
using System;
using System.Collections.Generic;
using GameDevWare.Serialization;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private EnemyController enemy;

    private Dictionary<string, EnemyController> enemies = new();
    private ColyseusRoom<State> room;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        Dictionary<string, object> data = new()
        {
            {"speed", player.Speed},
            {"hp", player.MaxHealth},
        };

        room = await Instance.client.JoinOrCreate<State>("state_handler", data);

        room.OnStateChange += OnChange;
        room.OnMessage<string>("Shoot", ApplyShoot);
    }

    private void ApplyShoot(string jsonShootInfo)
    {
        ShootInfo shootInfo = JsonUtility.FromJson<ShootInfo>(jsonShootInfo);

        if (enemies.ContainsKey(shootInfo.key) == false)
        {
            Debug.Log("Error: No such shooting enemy");
            return;
        }
        
        enemies[shootInfo.key].Shoot(shootInfo);

    }

    private void OnChange(State state, bool isFirstState)
    {
        if (isFirstState == false) 
            return;

        state.players.ForEach((key, player) => {
            if (key == room.SessionId) CreatePlayer(player);
            else CreateEnemy(key, player);
        });

        room.State.players.OnAdd += CreateEnemy;
        room.State.players.OnRemove += RemoveEnemy;
    }

    private void CreatePlayer(Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);
        Instantiate(this.player, position, Quaternion.identity);
    }


    private void CreateEnemy(string key, Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);
        var enemy = Instantiate(this.enemy, position, Quaternion.identity); 
        enemy.Init(player);   
        enemies.Add(key, enemy);
    }

    private void RemoveEnemy(string key, Player player)
    {
        if (enemies.ContainsKey(key) == false) return;

        var enemy = enemies[key];
        enemy.Destroy();
        enemies.Remove(key);
    }

    public void SendMessage(string key, Dictionary<string, object> data)
    {
        room.Send(key, data);
    }

    public void SendMessage(string key, string data)
    {
        room.Send(key, data);
    }


    protected override void OnDestroy() 
    {
        base.OnDestroy();

        room.Leave();
    }

    public string GetSessionId() => room.SessionId;
}
