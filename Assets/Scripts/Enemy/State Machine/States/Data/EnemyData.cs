using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyData",menuName = "Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    public float MaxHealthData;

    public float radiusCheckToChase;

    public float radiusCheckToAttack;
}
