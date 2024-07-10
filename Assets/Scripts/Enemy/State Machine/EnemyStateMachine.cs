using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState CurrentEnemyState { get; private set; }   

    public void Intialize(EnemyState enemyState)
    {
        CurrentEnemyState = enemyState;
        enemyState.Enter();
    }

    public void ChangeState(EnemyState enemyState)
    {
        CurrentEnemyState.Exit();
        CurrentEnemyState = enemyState;
        CurrentEnemyState.Enter();

    }
}
