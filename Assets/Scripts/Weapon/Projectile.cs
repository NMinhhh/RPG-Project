using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] protected float damage = 20f;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float timeLife = 5f;
    protected float currentTimeLife;
    protected virtual void Start()
    {
        currentTimeLife = timeLife;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        currentTimeLife -= Time.deltaTime;
        if(currentTimeLife <= 0)
        {
            ObjectPool.Instance.AddInPool(Pool.Type.Arrow, this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            ObjectPool.Instance.AddInPool(Pool.Type.Arrow, this.gameObject);

        }
    }

    public virtual void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public virtual void OnObjectSpawn()
    {
        currentTimeLife = timeLife;
    }
}
