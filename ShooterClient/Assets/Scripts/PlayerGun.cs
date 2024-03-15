using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    [SerializeField] private int damage;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float shootDelay = 0.2f;

    private float lastShootTime;

    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();

        if (Time.time - lastShootTime < shootDelay) return false;

        Vector3 position = BulletPoint.position;
        Vector3 velocity = BulletPoint.forward  * bulletSpeed;

        Instantiate(bulletPrefab, position, BulletPoint.rotation).Init(velocity, damage);
        lastShootTime = Time.time;   
        OneShot?.Invoke();     

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;
        info.dX = velocity.x;
        info.dY = velocity.y;
        info.dZ = velocity.z;

        return true;
    }

}
