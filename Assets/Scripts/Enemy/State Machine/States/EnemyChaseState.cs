using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected EnemyChaseData data;
    protected bool isPlayerInRangeToAttack;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetSpeed(data.chaseSpeed);
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
        enemy.Move(enemy.PlayerPos.position);
        enemy.transform.LookAt(enemy.PlayerPos.position);
        isPlayerInRangeToAttack = enemy.CheckDistance(enemy.transform.position, enemy.PlayerPos.position) <= data.checkPlayerInRangeToAttack ? true : false;
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
