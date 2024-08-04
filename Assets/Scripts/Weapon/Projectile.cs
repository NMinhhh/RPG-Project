using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] protected float damage = 20f;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float timeLife = 5f;
    [SerializeField] protected List<string> hitEffect;
    [SerializeField] protected LayerMask layerEffectHit;
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
            ObjectPool.Instance.AddInPool("Arrow", this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SpawnHitEffect(other);
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            ObjectPool.Instance.AddInPool("Arrow", this.gameObject);

        }
    }

    void SpawnHitEffect(Collider collider)
    {
        CollisionType collisionType = collider.GetComponent<CollisionType>();
        string hitEffect = "";
        if (!collisionType)
        {
            Debug.Log("Don't have collision");
            return;
        }
        switch (collisionType.type)
        {
            default:
                break;
            case CollisionType.Type.Meat:
                hitEffect = this.hitEffect[0];
                break;
            case CollisionType.Type.Wood:
                hitEffect = this.hitEffect[1];
                break;

        }
        if(Physics.Raycast(transform.position, transform.forward,out RaycastHit hit, 10, layerEffectHit))
        {
            Vector3 direction = transform.position - hit.point;
            ObjectPool.Instance.SpawnFromPool(hitEffect, hit.point, Quaternion.LookRotation(direction.normalized, Vector3.up)).transform.SetParent(collider.transform);
        }
    }

    public virtual void CreateProjectile(float damage, float speed, float timeLife)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    public virtual void OnObjectSpawn()
    {
        currentTimeLife = timeLife;
    }
}
