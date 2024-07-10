using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHurtData", menuName = "Enemy Data/ Enemy Hurt Data")]
public class EnemyHurtData : ScriptableObject
{
    public float hurtTime;
    public float knockback;
}
