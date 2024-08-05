using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantChaseState : EnemyChaseState
{
    private Mutant mutant;

    public MutantChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data, Mutant mutant) : base(enemy, stateMachine, isAnimationName, data)
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
        if(isPlayerToShieldAttack && (enemy.currentCombo == 3 || (enemy.currentCombo == 1 && !mutant.isEquip)))
        {
            enemy.ResetCombo();
            stateMachine.ChangeState(mutant.SwingState);
        }
        else if (isPlayerToAttack)
        {
            stateMachine.ChangeState(mutant.MeleeattackState);
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
