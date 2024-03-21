using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    public int Length { get { return points.Length; } }

    public void GetPoint(int index, out Vector3 position, out Vector3 rotation)
    {
        if (index >= points.Length)
        {
            position = Vector3.zero;
            rotation = Vector3.zero;
            return;
        }

        position = points[index].position;
        rotation = points[index].eulerAngles;
    }
}
