using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserPlayerDetectedState : EnemyPlayerDetectedState
{
    private Necromanser necromanser;

    public NecromanserPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data)
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
        if (canSpawn && isDetectedOver && isPlayerInRangeToSpawn)
        {
            stateMachine.ChangeState(necromanser.SpawnSkillState);
        }
        else if (canThrow && !isPlayerInRange && isPlayerInRangeToThrow)
        {
            stateMachine.ChangeState(necromanser.ThrowWeaponState);
        }
        else if (isPlayerInRange && isDetectedOver)
        {
            stateMachine.ChangeState(necromanser.MeleeAttackState);
        }
        else if ((isPlayerDetected && !isPlayerInRange) || !isPlayerDetected)
        {
            stateMachine.ChangeState(necromanser.ChaseState);
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
