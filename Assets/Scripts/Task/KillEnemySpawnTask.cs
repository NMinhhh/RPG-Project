using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemySpawnTask : TaskStep, IResetable
{
    [SerializeField] private SpawnEnemy spawnEnemy;
    [SerializeField] private bool isSpawned;

    public void ResetBaseState()
    {
        spawnEnemy.ResetSpawn();
    }

    public override void StartTask()
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
