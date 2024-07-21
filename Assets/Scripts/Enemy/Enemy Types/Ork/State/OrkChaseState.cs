using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkChaseState : EnemyChaseState
{
    private Ork ork;

    public OrkChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.ork = ork;
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
        if (isPlayerInRange)
        {
            stateMachine.ChangeState(ork.MeleeAttackState);
        }else if(canDash && !isPlayerInRange)
        {
            stateMachine.ChangeState(ork.DashAttackState);
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
