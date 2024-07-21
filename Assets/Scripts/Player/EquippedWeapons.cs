using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedWeapons : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private BoxCollider box;
    [SerializeField] private GameObject blood;
    private Transform damagePoint;
    private bool isAttack;

    Transform player;

    void Start()
    {
        enemy = new List<GameObject>();
        player = GameObject.FindWithTag("Player").transform;
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
                    damagePoint = hit2.collider.transform.Find("DamagePoint");
                    IDamageable damagaeble = hit2.collider.gameObject.GetComponent<IDamageable>();
                    Vector3 dir = (hit2.transform.position - player.position).normalized;
                    if (damagaeble != null)
                    {
                        IKnockBackable knockBackable = hit2.collider.GetComponent<IKnockBackable>();
                        if(knockBackable != null)
                        {
                            knockBackable.DamageDiretion(dir);
                        }
                        damagaeble.Damage(damage);
                        ObjectPool.Instance.SpawnFromPool(Pool.Type.BloodParticle,damagePoint.position, Quaternion.identity).transform.SetParent(hit2.transform);
                    }
                    enemy.Add(hit2.collider.gameObject);
                }
            }
        }
      
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void StartDealDamage() => isAttack = true;

    public void EndDealDamage()
    {
        isAttack = false;
        enemy.Clear();
    }

}
