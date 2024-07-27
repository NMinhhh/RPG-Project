using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDeathState : EnemyDeathState
{
    private Dummy dummy;

    public DummyDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Dummy dummy) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.dummy = dummy;
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
