using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private Rigidbody bulletRb;
    private int damage;

    public void Init(Vector3 velocity, int damage = 0)
    {
        this.damage = damage;
        bulletRb.velocity = velocity;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out EnemyCharacter enemy))
            enemy.ApplyDamage(damage);
        else if (other.collider.TryGetComponent(out EnemyHead enemyHead))
        {
            enemyHead.Explosion(transform.rotation);
            enemyHead.GetComponentInParent<EnemyCharacter>().ApplyFullDamage();
        }

        Destroy();
    }

}
