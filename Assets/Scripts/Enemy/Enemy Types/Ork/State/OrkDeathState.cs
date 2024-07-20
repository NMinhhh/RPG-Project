using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDeathState : EnemyDeathState
{
    private Ork ork;
    public OrkDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Ork ork) : base(enemy, stateMachine, isAnimationName, data)
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
        enemy.GetComponent<CapsuleCollider>().enabled = false;
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
            ork.Die();
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
