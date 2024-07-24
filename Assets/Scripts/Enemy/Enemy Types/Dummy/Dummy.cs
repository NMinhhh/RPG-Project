using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] private int amountOfDamage;
    private Animator anim;
    private BoxCollider boxCollider;
    public bool isDie;
    public void Damage(float damage)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Hit), transform.position, .5f); ;
        amountOfDamage--;
        if(amountOfDamage == 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Damage");
        }
    }

    public void Die()
    {
        isDie = true;
        anim.SetTrigger("Death");
        boxCollider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
