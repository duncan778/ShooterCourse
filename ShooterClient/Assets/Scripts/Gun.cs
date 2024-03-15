using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] private GameObject[] gunModels;
    public Transform BulletPoint { get => currentGunModel.transform.GetChild(0); }

    public Action OneShot;

    public int CurrentGunIndex { get; private set; } = 0;
    private GameObject currentGunModel;

    private void Start()
    {
        SetModel();
    }

    private void SetModel()
    {
        currentGunModel?.SetActive(false);
        currentGunModel = gunModels[CurrentGunIndex];
        currentGunModel.SetActive(true);
    }

    private void NextGunIndex()
    {
        CurrentGunIndex++;
        if (CurrentGunIndex >= gunModels.Length)
            CurrentGunIndex = 0;
            }

    private void PrevGunIndex()
    {
        CurrentGunIndex--;
        if (CurrentGunIndex <= 0)
            CurrentGunIndex = gunModels.Length - 1;
    }

    public void ChangeGun(float mouseWheelDirection)
    {
        if (mouseWheelDirection > 0) NextGunIndex();
        else PrevGunIndex();
        
        SetModel();
        SendGunModel();
    }

    private void SendGunModel()
    {
        var data = new Dictionary<string, object>()
        {
            { "id", CurrentGunIndex },
        };
        MultiplayerManager.Instance.SendMessage("gun", data);
    }

    public void ChangeGun(int gunIndex)
    {
        CurrentGunIndex = gunIndex;
        SetModel();
    }
}
