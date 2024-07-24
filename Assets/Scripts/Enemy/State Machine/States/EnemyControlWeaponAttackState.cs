using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlWeaponAttackState : EnemyState
{
    protected EnemyControlWeaponAttackData data;
    protected Transform attackPoint;

    public EnemyControlWeaponAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyControlWeaponAttackData data, Transform attackPoint) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
        this.attackPoint = attackPoint;
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
