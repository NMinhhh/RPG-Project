using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDashAttackData", menuName = "Enemy Data/Enemy Dash Attack Data")]
public class EnemyDashAttackData : ScriptableObject
{
    public float dashTime = 1f;
    public float leftDamage = 20f;
    public float rightDamage = 20f;
    public float dashSpeed = 10f;
    public float dashDelayTime = 0.5f;
}
