using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTr;

    void Start()
    {
        cameraTr = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cameraTr);
    }
}
