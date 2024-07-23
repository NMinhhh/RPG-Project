using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyRangeAttackData", menuName = "Enemy Data/Enemy Range Attack Data")]
public class EnemyRangeAttackData : ScriptableObject
{
    public float damage = 30f;
    public float offsetY = .5f;
}
