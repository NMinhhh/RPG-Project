using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDashAttackState : EnemyDashAttackState
{
    private Ork ork;
    public OrkDashAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDashAttackData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isFinishDash)
        {
            stateMachine.ChangeState(ork.PlayerDetectedState);
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
