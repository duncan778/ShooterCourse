using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private Transform headshotPoint;
    [SerializeField] private GameObject headShotVFX;
    [SerializeField] private MeshRenderer headMesh;

    public void Explosion(Quaternion bulletRotation)
    {
        HideHead(true);
        var vfx = Instantiate(headShotVFX, headshotPoint.position, bulletRotation);
        ParticleSystem.MainModule main = vfx.GetComponent<ParticleSystem>().main;
        main.startColor = headMesh.material.color;
    }

    public void HideHead(bool active)
    {
        var headMeshes = GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in headMeshes)
            mesh.enabled = !active;
    }
}
