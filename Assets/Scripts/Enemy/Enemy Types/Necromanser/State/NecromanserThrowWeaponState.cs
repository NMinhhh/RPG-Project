using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserThrowWeaponState : EnemyThrowWeaponState
{
    private Necromanser necromanser;

    public NecromanserThrowWeaponState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyThrowWeaponData data, GameObject weaponToThrow, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data, weaponToThrow)
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
        if (isFinishCatch)
        {
            stateMachine.ChangeState(necromanser.PlayerDetectedState);
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
