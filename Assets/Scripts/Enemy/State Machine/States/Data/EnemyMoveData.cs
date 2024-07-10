using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveData", menuName = "Enemy Data/ Enemy Move Data")]
public class EnemyMoveData : ScriptableObject
{
    public float Speed;

    public Vector2 XPosRandom;
    public Vector2 YPosRandom;
}
