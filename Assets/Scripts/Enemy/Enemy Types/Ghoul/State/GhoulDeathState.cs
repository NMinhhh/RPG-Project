using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDeathState : EnemyDeathState
{
    private Ghoul ghoul;

    public GhoulDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Ghoul ghoul) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.ghoul = ghoul;
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
