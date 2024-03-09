using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float shootDelay = 0.2f;

    public Action OneShot;

    private float lastShootTime;

    public void Shoot()
    {
        if (Time.time - lastShootTime < shootDelay) return;

        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation).Init(bulletPoint.forward, bulletSpeed);
        lastShootTime = Time.time;   
        OneShot?.Invoke();     
    }
}
