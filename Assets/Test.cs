using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed;
    [SerializeField] protected Transform[] posShoot;
    private void Start()
    {
        foreach (Transform pos in posShoot)
        {
            ObjectPool.Instance.SpawnFromPool("Arrow(Trap)", pos.position, pos.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IKnockBackable knockBackable = other.GetComponent<IKnockBackable>();
            if (knockBackable != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                knockBackable.DamageDiretion(direction);
                knockBackable.KnockBack(knockbackSpeed);
            }
        }
    }

}
