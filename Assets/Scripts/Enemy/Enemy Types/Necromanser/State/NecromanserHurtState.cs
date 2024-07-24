using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserHurtState : EnemyHurtState
{
    private Necromanser necromanser;

    public NecromanserHurtState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyHurtData data, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isHurtFinish)
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
