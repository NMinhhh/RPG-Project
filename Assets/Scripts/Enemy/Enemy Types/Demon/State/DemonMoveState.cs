using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMoveState : EnemyMoveState
{
    private Demon _demon;

    public DemonMoveState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMoveData data, Transform[] destination, Demon demon) : base(enemy, stateMachine, isAnimationName, data, destination)
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
        else if (isFinishMove)
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
