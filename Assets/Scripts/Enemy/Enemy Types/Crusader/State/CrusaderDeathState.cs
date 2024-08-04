using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderDeathState : EnemyDeathState
{
    private Crusader crusader;

    public CrusaderDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Crusader crusader) : base(enemy, stateMachine, isAnimationName, data)
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
            isFinishAnimation = false;
            crusader.Die();
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
