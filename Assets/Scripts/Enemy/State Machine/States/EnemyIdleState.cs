using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected EnemyIdleData data;
    protected bool isIdleFinish;
    protected bool isPlayerDetected;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyIdleData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(enemy.CheckPlayerDetected() && !enemy.CheckBlock())
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }

    }

    public override void Enter()
    {
        base.Enter();
        enemy.Move(enemy.transform.position);
        isIdleFinish = false;
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
        if(Time.time >= startTime + data.idleTime)
        {
            isIdleFinish = true;
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
