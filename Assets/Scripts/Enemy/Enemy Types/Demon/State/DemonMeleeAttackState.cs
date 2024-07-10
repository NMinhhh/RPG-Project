using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMeleeAttackState : EnemyMeleeAttackState
{
    private Demon _demon;

    public DemonMeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMeleeAttackData data, Demon demon) : base(enemy, stateMachine, isAnimationName, data)
    {
        _demon = demon;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ChangeState");
       
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
            stateMachine.ChangeState(_demon.IdleState);
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
