using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedWeapons : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private BoxCollider box;

    private bool isAttack;

    void Start()
    {
        enemy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttack)
        {
            RaycastHit[] hit = Physics.BoxCastAll(box.bounds.center, box.bounds.extents, transform.up, box.transform.rotation, 0, whatIsEnemy);
            foreach (RaycastHit hit2 in hit)
            {
                if (!enemy.Contains(hit2.collider.gameObject))
                {
                    IDamagaeble damagaeble = hit2.collider.gameObject.GetComponent<IDamagaeble>();
                    if (damagaeble != null)
                    {
                        damagaeble.Damage(damage);
                    }
                    enemy.Add(hit2.collider.gameObject);
                }
            }
        }
      
    }

    public void StartDealDamage()
    {
        isAttack = true;
        enemy.Clear();
    }

    public void EndDealDamage()
    {
        isAttack = false;
    }

}