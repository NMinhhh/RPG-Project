using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkPlayerDetectedState : EnemyPlayerDetectedState
{
    private Ork ork;
    private bool isDashAttack;
    public OrkPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.ork = ork;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isDashAttack = enemy.amountOfAttack == 5;
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
        if (!isPlayerInRange && isPlayerDetected && isDashAttack)
        {
            stateMachine.ChangeState(ork.DashAttackState);
        }
        else if (isPlayerInRange && isDetectedOver)
        {
            stateMachine.ChangeState(ork.MeleeAttackState);
        }
        else if ((isPlayerDetected && !isPlayerInRange) || !isPlayerDetected)
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