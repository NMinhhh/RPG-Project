using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderMeleeAttackState : EnemyMeleeAttackState
{
    private Crusader crusader;

    public CrusaderMeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMeleeAttackData data, Crusader crusader) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isFinishAnimation)
        {
            if (isPlayerDetected)
            {
                stateMachine.ChangeState(crusader.PlayerDetectedState);

            }
            else
            {
                stateMachine.ChangeState(crusader.ChaseState);
            }
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
