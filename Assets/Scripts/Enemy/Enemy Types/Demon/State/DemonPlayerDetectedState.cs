using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonPlayerDetectedState : EnemyPlayerDetectedState
{
    private Demon _demon;
    public DemonPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
    {
        _demon = demon;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
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
        if (isPlayerInRange && isDetectedOver)
        {
            stateMachine.ChangeState(_demon.MeleeAttackState);
        }
        else if (isPlayerDetected && !isPlayerInRange)
        {
            stateMachine.ChangeState(_demon.ChaseState);
        }
        else if(!isPlayerDetected)
        {
            stateMachine.ChangeState(_demon.IdleState);
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
