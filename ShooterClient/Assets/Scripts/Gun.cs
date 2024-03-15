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

    private int currentGunIndex = 0;
    private GameObject currentGunModel;

    private void Start()
    {
        currentGunModel = gunModels[currentGunIndex];
    }

    private void SetModel()
    {
        currentGunModel.SetActive(false);
        currentGunModel = gunModels[currentGunIndex];
        currentGunModel.SetActive(true);
    }

    public void NextGun()
    {
        currentGunIndex++;
        if (currentGunIndex >= gunModels.Length)
            currentGunIndex = 0;
        
        SetModel();
    }

    public void PrevGun()
    {
        currentGunIndex--;
        if (currentGunIndex <= 0)
            currentGunIndex = gunModels.Length - 1;

        SetModel();
    }

}
