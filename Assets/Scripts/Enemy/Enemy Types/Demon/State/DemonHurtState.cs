using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHurtState : EnemyHurtState
{
    private Demon _demon;
    public DemonHurtState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyHurtData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isHurtFinish)
        {
            stateMachine.ChangeState(_demon.PlayerDetectedState);
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
