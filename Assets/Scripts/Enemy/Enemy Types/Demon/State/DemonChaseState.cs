using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonChaseState : EnemyChaseState
{
    private Demon _demon;
    public DemonChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isPlayerInRange)
        {
            stateMachine.ChangeState(_demon.MeleeAttackState);
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
