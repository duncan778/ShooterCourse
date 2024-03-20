using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRay : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform center;
    [SerializeField] private Transform point;
    [SerializeField] private float pointSize;
    private Transform cameraTr;

    private void Awake()
    {
        cameraTr = Camera.main.transform;
    }

    private void Update()
    {
        Ray ray = new Ray(center.position, center.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, layerMask, QueryTriggerInteraction.Ignore))
        {
            center.localScale = new Vector3(1,1, hit.distance);
            point.position = hit.point;
            float distance = Vector3.Distance(cameraTr.position, hit.point);
            point.localScale = Vector3.one * distance * pointSize;
        }
    }
}
