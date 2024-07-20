using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkIdleState : EnemyIdleState
{
    private Ork ork;

    public OrkIdleState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyIdleData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isPlayerDetected)
        {
            stateMachine.ChangeState(ork.PlayerDetectedState);
        }
        else if (isIdleFinish)
        {
            stateMachine.ChangeState(ork.MoveState);
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
