using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] meshRenderers;

    public void Set(Material material)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = material;
        }
    }
}
