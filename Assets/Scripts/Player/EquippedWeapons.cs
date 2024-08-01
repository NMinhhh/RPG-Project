using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedWeapons : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private BoxCollider box;
    [SerializeField] private float percentStrongDamage = 50f;
    [Header("Slash Effect")]
    [SerializeField] private string[] slashEffectName;

    private List<GameObject> enemy;

    private bool isAttack;
    private bool isStrongAttack;

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
            RaycastHit[] hits = Physics.BoxCastAll(box.bounds.center, box.bounds.extents, transform.up, box.transform.rotation, 0, whatIsEnemy);

            foreach (RaycastHit hit in hits)
            {
                if (!enemy.Contains(hit.collider.gameObject))
                {
                    IDamageable damagaeble = hit.collider.gameObject.GetComponent<IDamageable>();

                    Vector3 dir = (hit.transform.position - player.position).normalized;

                    if (damagaeble != null)
                    {
                        IKnockBackable knockBackable = hit.collider.GetComponent<IKnockBackable>();

                        if(knockBackable != null)
                        {
                            knockBackable.DamageDiretion(dir);
                        }

                        float currentDamage = (isStrongAttack ? damage + damage * percentStrongDamage / 100 : damage);

                        damagaeble.Damage(currentDamage);
                    }
                    //Spawn Blood
                    Vector3 spawnPos = hit.transform.position;
                    spawnPos.y = transform.position.y;
                    ObjectPool.Instance.SpawnFromPool("Blood Hit", spawnPos, Quaternion.identity).transform.SetParent(hit.transform);

                    enemy.Add(hit.collider.gameObject);
                }
            }
        }
      
    }

    public void StrongAttack()
    {
        isStrongAttack = true;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void StartDealDamage()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Swing), transform.position);
        isAttack = true;
    }  


    public void EndDealDamage()
    {
        isAttack = false;
        isStrongAttack = false;
        enemy.Clear();
    }

    public void TriggerEffect(int order,Vector3 pos, Quaternion rotaion)
    {
        ObjectPool.Instance.SpawnFromPool(slashEffectName[order], pos, rotaion);
    }
}


