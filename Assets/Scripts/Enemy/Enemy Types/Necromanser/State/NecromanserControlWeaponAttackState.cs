using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserControlWeaponAttackState : EnemyControlWeaponAttackState
{
    private Necromanser necromanser;

    public NecromanserControlWeaponAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyControlWeaponAttackData data, Transform attackPoint, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data, attackPoint)
    {
        this.necromanser = necromanser;
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
