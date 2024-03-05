using UnityEngine;
using Colyseus;
using System;
using System.Collections.Generic;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyController enemy;

    private ColyseusRoom<State> room;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        room = await Instance.client.JoinOrCreate<State>("state_handler");

        room.OnStateChange += OnChange;
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
        player.OnChange += enemy.OnChange;
    }

    private void RemoveEnemy(string key, Player player)
    {

    }

    public void SendMessage(string key, Dictionary<string, object> data)
    {
        room.Send(key, data);
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();

        room.Leave();
    }
}
