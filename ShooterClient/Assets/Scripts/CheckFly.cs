using UnityEngine;

public class CheckFly : MonoBehaviour
{
    public bool IsFly { get; private set; }
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float coyoteTime = 0.15f;
    private float flyTimer = 0;

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, radius, layerMask))
        {
            IsFly = false;
            flyTimer = 0;
        }
        else
        {
            flyTimer += Time.deltaTime;
            if (flyTimer > coyoteTime)
                IsFly = true;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }    
#endif
}
