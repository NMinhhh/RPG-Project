using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    protected EnemyPlayerDetectedData data;
    protected bool isPlayerDetected;
    protected bool isPlayerInRange;
    protected bool isDetectedOver;
    public EnemyPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerDetected = enemy.CheckPlayerDetected();
        isPlayerInRange = enemy.CheckPlayerInRange();
    }

    public override void Enter()
    {
        base.Enter();
        isDetectedOver = false;
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
        if (Time.time >= startTime + data.detectedTime)
        {
            isDetectedOver = true;
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