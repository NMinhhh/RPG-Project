using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderHurtState : EnemyHurtState
{
    private Crusader crusader;

    public CrusaderHurtState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyHurtData data, Crusader crusader) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isHurtFinish)
        {
            stateMachine.ChangeState(crusader.PlayerDetectedState);
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
