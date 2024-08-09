using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyTaskStep : TaskStep
{
    [Header("Amount of enemy need kill")]
    [SerializeField] private GameObject[] enemys;


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

  
}
