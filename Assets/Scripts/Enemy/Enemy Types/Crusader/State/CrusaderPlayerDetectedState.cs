using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderPlayerDetectedState : EnemyPlayerDetectedState
{
    private Crusader crusader;

    public CrusaderPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Crusader crusader) : base(enemy, stateMachine, isAnimationName, data)
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
        if(isPlayerToShieldAttack && isDetectedOver)
        {
            stateMachine.ChangeState(crusader.ShieldAttackState);
        }
        else if(isDetectedOver && isPlayerToAttack)
        {
            stateMachine.ChangeState(crusader.MeleeAttackState);
        }
        else if ((isPlayerDetected && !isPlayerToAttack) || !isPlayerDetected)
        {
            stateMachine.ChangeState(crusader.ChaseState);
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
