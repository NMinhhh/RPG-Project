using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyState
{
    protected EnemyMeleeAttackData data;
    protected bool isPlayerInRange;
    public EnemyMeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMeleeAttackData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInRange = false;
        enemy.SetSpeed(0);
        enemy.Move(enemy.transform.position);
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
        isPlayerInRange = enemy.CheckDistance(enemy.transform.position, enemy.PlayerPos.position) <= data.inRangeAttack ? true : false;
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
