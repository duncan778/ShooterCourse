using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRb;

    public void Init(Vector3 direction, float speed)
    {
        bulletRb.velocity = direction * speed;
    }

}
