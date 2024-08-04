using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderChaseState : EnemyChaseState
{
    private Crusader crusader;

    public CrusaderChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data, Crusader crusader) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.crusader = crusader;
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
        if (isPlayerToShieldAttack)
        {
            stateMachine.ChangeState(crusader.ShieldAttackState);
        }
        else if (isPlayerToAttack)
        {
            stateMachine.ChangeState(crusader.MeleeAttackState);
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
