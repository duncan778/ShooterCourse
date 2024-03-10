using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private const string Shooting = "Shoot";
    [SerializeField] private Gun gun;
    [SerializeField] private Animator animator;

    private void Start()
    {
        gun.OneShot += Shoot;
    }

    private void OnDestroy()
    {
        gun.OneShot -= Shoot;
    }

    private void Shoot()
    {
        animator.SetTrigger(Shooting);
    }
}
