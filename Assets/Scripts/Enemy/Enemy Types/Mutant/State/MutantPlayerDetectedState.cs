using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantPlayerDetectedState : EnemyPlayerDetectedState
{
    private Mutant mutant;

    public MutantPlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyPlayerDetectedData data, Mutant mutant) : base(enemy, stateMachine, isAnimationName, data)
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
        if(isDetectedOver && isPlayerToShieldAttack && (enemy.currentCombo == 3 || (enemy.currentCombo == 1 && !mutant.isEquip)))
        {
            enemy.ResetCombo();
            stateMachine.ChangeState(mutant.SwingState);
        }
        else if(isDetectedOver && isPlayerToAttack)
        {
            stateMachine.ChangeState(mutant.MeleeattackState);
        }
        else if((isPlayerDetected && !isPlayerToAttack) || !isPlayerDetected)
        {
            stateMachine.ChangeState(mutant.ChaseState);
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
