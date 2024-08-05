using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed;
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
