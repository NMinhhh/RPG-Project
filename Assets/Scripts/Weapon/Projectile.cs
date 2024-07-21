using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float timeLife = 5f;
    private float currentTimeLife;
    void Start()
    {
        currentTimeLife = timeLife;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        currentTimeLife -= Time.deltaTime;
        if(currentTimeLife <= 0)
        {
            ObjectPool.Instance.AddInPool(Pool.Type.Arrow, this.gameObject);
        }
    }

    private void OnDisable()
    {
        currentTimeLife = timeLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(20);
            }
            ObjectPool.Instance.AddInPool(Pool.Type.Arrow, this.gameObject);

        }
    }
}
