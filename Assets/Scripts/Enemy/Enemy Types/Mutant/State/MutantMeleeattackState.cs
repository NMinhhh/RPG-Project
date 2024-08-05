using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMeleeattackState : EnemyMeleeAttackState
{
    private Mutant mutant;
    public MutantMeleeattackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMeleeAttackData data, Mutant mutant) : base(enemy, stateMachine, isAnimationName, data)
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
        if (isFinishAnimation)
        {
            if (mutant.isEquip)
            {
                if (enemy.currentCombo == 3 || !isPlayerToAttack)
                {
                    stateMachine.ChangeState(mutant.PlayerDetectedState);
                }
                else if (isPlayerToAttack)
                {
                    stateMachine.ChangeState(mutant.MeleeattackState);
                }
            }
            else
            {
                stateMachine.ChangeState(mutant.PlayerDetectedState);
            }
            
          
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
