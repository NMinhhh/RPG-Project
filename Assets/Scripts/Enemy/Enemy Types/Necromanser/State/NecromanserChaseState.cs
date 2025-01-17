using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserChaseState : EnemyChaseState
{
    private Necromanser necromanser;
    public NecromanserChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data)
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
        if(canSpawn && isPlayerInRangeToSpawn)
        {
            stateMachine.ChangeState(necromanser.SpawnSkillState);
        }
        else if(canThrow && isPlayerInRangeToThrow)
        {
            stateMachine.ChangeState(necromanser.ThrowWeaponState);
        }
        else if (isPlayerToAttack)
        {
            stateMachine.ChangeState(necromanser.MeleeAttackState);
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
