using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkPlayerDetectedState : EnemyPlayerDetectedState
{
    private Ork ork;
    public OrkPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
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
        if(!isPlayerToAttack && isPlayerDetected && canDash)
        {
            stateMachine.ChangeState(ork.DashAttackState);
        }
        else if (isPlayerToAttack && isDetectedOver)
        {
            stateMachine.ChangeState(ork.MeleeAttackState);
        }
        else if ((isPlayerDetected && !isPlayerToAttack) || !isPlayerDetected)
        {
            stateMachine.ChangeState(ork.ChaseState);
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
