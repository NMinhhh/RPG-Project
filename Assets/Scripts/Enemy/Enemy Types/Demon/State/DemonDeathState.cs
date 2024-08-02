using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDeathState : EnemyDeathState
{
    private Demon _demon;
    public DemonDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isFinishAnimation)
        {
            isFinishAnimation = false;
            enemy.Die();
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
