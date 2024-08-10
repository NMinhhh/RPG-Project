using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyTaskStep : TaskStep, IResetable
{
    [Header("Amount of enemy need kill")]
    [SerializeField] private GameObject[] enemys;
  

    public override void StartTask()
    {
        foreach (var enemy in enemys)
        {
            enemy.SetActive(true);
        }
    }



    protected virtual void Update()
    {
        if (CheckToChangeTask())
        {
            FinishTaskStep();
        }
    }

    protected virtual bool CheckToChangeTask()
    {
        int count = 0;
        foreach (GameObject enemy in enemys)
        {
            if (enemy.GetComponentInChildren<Enemy>().isDie)
            {
                count++;
            }
        }
        return enemys.Length == count;
    }

    public void ResetBaseState()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            GameObject enemy = enemys[i];
            enemy.SetActive(false);
            Enemy enemyObj = enemy.GetComponentInChildren<Enemy>();
            enemyObj.ResetEnemy();
        }
    }
}
