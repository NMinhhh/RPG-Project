using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newData",menuName = "Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    public bool isEnemyPatrol;

    public float MaxHealthData;

    public float radiusCheckToChase;

    public float radiusCheckToAttack;

    public LayerMask whatIsWall;

    public int maxCombo = 1;

    public int amountOfDamageToHurt = 1;

    [Header("Dash Attack")]
    public bool isDash;
    public Vector2 dashCooldownRan;

    [Header("Throw Weapon")]
    public float radiusCheckThrow = 5f;
    public bool isThrowWeapon;
    public Vector2 throwWeaponCooldownRan;

}
