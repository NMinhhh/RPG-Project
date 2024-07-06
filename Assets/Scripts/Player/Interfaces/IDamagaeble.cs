using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagaeble 
{
    float MaxHealth { get; set; }

    float CurrentHealth {  get; set; }

    void Damage(float damage);

    void Die();


}
