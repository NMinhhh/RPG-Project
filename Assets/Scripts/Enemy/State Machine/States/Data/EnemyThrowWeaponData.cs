using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newThrowWeaponData", menuName = "Enemy Data/Enemy Throw Weapon Data")]
public class EnemyThrowWeaponData : ScriptableObject
{
    public float speed = 5f;
    public float damage = 50f;
    public float moveTime = 4f;
    public float throwTime = .5f;
    public float catchTime = .5f;
    public float offsetY = 0.5f;
}
