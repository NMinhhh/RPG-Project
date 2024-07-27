using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemySpawnTask : TaskStep
{
    [SerializeField] private SpawnEnemy spawnEnemy;
    [SerializeField] private bool isSpawned;
    // Start is called before the first frame update
    void Start()
    {
        if (!isSpawned)
        {
            spawnEnemy.StartSpawn();
            spawnEnemy.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemy.isFinishSpawn)
        {
            FinishTaskStep();
        }
    }
}
