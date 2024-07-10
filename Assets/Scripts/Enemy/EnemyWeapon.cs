using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private float _damage;
    private List<GameObject> hit = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hit.Contains(other.gameObject))
        {
            IDamagaeble damagaeble = other.GetComponent<IDamagaeble>();
            if(damagaeble != null)
            {
                damagaeble.Damage(_damage);
                hit.Add(other.gameObject);
            }
        }
    }

    public void StartDealDamage()
    {
        hit.Clear();
        _collider.enabled = true;
    }

    public void EndDealDamage()
    {
        _collider.enabled = false;
    }
}
