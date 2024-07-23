using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulPlayerDetectedState : EnemyPlayerDetectedState
{
    private Ghoul ghoul;
    public GhoulPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Ghoul ghoul) : base(enemy, stateMachine, isAnimationName, data)
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
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (isDetectedOver)
        {
            stateMachine.ChangeState(ghoul.RangeAttackState);
        }
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }
}
