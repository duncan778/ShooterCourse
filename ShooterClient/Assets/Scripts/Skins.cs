using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    public int Length { get {return materials.Length;} }

    public Material GetMaterial(int index)
    {
        if (index >= materials.Length) return materials[0];

        return materials[index];
    }
}
