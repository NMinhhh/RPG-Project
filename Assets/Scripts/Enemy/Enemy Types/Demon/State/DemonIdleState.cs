using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonIdleState : EnemyIdleState
{
    private Demon _demon;
   
    public DemonIdleState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyIdleData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
    {
        _demon = demon;
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
        if (isPlayerDetected)
        {
            stateMachine.ChangeState(_demon.PlayerDetectedState);
        }
        else if (isIdleFinish)
        {
            stateMachine.ChangeState(_demon.MoveState);
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
