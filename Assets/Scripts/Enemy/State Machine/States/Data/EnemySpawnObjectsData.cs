using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpawnObjectsData", menuName = "Enemy Data/Enemy Spawn Objects Data")]
public class EnemySpawnObjectsData : ScriptableObject
{
    public int amountOfObject = 5;
    public float damage = 10f;
    public float speed = 15f;
    public float timeLife = 3f;
    public float spawnDelayTime = .5f;
    public float prepTime;
    public float offsetY = 1f;
    public string spawmObjectName;
    public GameObject spawnObject;
}
