using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected EnemyChaseData data;
    protected bool isPlayerToAttack;
    protected bool isPlayerInRangeToThrow;
    protected bool isPlayerInRangeToSpawn;
    protected bool isPlayerToShieldAttack;
    protected bool canDash;
    protected bool canThrow;
    protected bool canSpawn;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerToAttack = enemy.CheckPlayerToMeleeAttack();
        isPlayerInRangeToThrow = enemy.CheckPlayerInRangeToThrow();
        isPlayerInRangeToSpawn = enemy.CheckPlayerInRangeToSpawn();
        isPlayerToShieldAttack = enemy.CheckPlayerToShieldAttack();
        canDash = enemy.CanDash();
        canThrow = enemy.CanThrow();
        canSpawn = enemy.CanSpawn();
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
        enemy.Move(enemy.playerPos.position);
        enemy.transform.LookAt(new Vector3(enemy.playerPos.position.x,enemy.transform.position.y,enemy.playerPos.position.z));

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
