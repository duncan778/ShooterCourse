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

    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();

        if (Time.time - lastShootTime < shootDelay) return false;

        Vector3 position = bulletPoint.position;
        Vector3 direction = bulletPoint.forward;

        Instantiate(bulletPrefab, position, bulletPoint.rotation).Init(direction, bulletSpeed);
        lastShootTime = Time.time;   
        OneShot?.Invoke();     

        direction *= bulletSpeed;
        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;
        info.dX = direction.x;
        info.dY = direction.y;
        info.dZ = direction.z;

        return true;
    }
}
