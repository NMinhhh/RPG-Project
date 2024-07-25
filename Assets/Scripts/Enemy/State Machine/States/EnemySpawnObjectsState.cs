using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnObjectsState : EnemyState
{
    protected EnemySpawnObjectsData data;
    protected Transform[] spawnPoints;
    protected bool isFinishPrep;
    protected bool isFinishSpawn;
    protected int amountOfObject;

    public EnemySpawnObjectsState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemySpawnObjectsData data, Transform[] spawnPoints) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
        this.spawnPoints = spawnPoints;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
    
    public override void Enter()
    {
        base.Enter();
        amountOfObject = data.amountOfObject;
        enemy.Move(enemy.transform.position);
        enemy.SetSpeed(0);
        isFinishPrep = false;
        isFinishSpawn = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.transform.LookAt(new Vector3(enemy.playerPos.position.x, enemy.transform.position.y, enemy.playerPos.position.z));
        if (Time.time >= startTime + data.prepTime && !isFinishPrep)
        {
            enemy.Anim.SetTrigger("isFinishPrepSpawn");
            isFinishPrep = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }
}
