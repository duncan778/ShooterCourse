using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private Transform headshotPoint;
    [SerializeField] private GameObject headShotVFX;
    [SerializeField] private MeshRenderer[] headMeshes;

    public void Explosion(Quaternion bulletRotation)
    {
        HideHead(true);
        var vfx = Instantiate(headShotVFX, headshotPoint.position, bulletRotation);
        ParticleSystem.MainModule main = vfx.GetComponent<ParticleSystem>().main;
        main.startColor = headMeshes[0].material.color;
    }

    public void HideHead(bool active)
    {
        foreach (var mesh in headMeshes)
            mesh.enabled = !active;
    }
}
