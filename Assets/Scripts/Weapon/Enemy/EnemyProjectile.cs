using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        currentTimeLife -= Time.deltaTime;
        if (currentTimeLife <= 0)
        {
            ObjectPool.Instance.AddInPool(Pool.Type.GhoulProjectile, this.gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            ObjectPool.Instance.AddInPool(Pool.Type.GhoulProjectile, this.gameObject);

        }
    }
}
