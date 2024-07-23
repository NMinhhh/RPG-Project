using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulHurtState : EnemyHurtState
{
    private Ghoul ghoul;
    public GhoulHurtState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyHurtData data, Ghoul ghoul) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isHurtFinish)
        {
            stateMachine.ChangeState(ghoul.PlayerDetectedState);
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
