using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHurtState : EnemyHurtState
{
    private Mutant mutant;

    public MutantHurtState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyHurtData data, Mutant mutant) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.mutant = mutant;
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
            stateMachine.ChangeState(mutant.PlayerDetectedState);
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
