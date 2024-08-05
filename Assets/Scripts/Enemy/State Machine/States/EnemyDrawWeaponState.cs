using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrawWeaponState : EnemyState
{
    protected EnemyDrawWeaponData data;

    public EnemyDrawWeaponState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDrawWeaponData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
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
