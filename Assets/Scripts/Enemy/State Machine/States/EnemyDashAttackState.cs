using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashAttackState : EnemyState
{
    protected EnemyDashAttackData data;
    protected Vector3 direction;
    protected bool isFinishDash;
    public EnemyDashAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDashAttackData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ResetAmountOfAttack();
        enemy.SetSpeed(0);
        enemy.Move(enemy.transform.position);
        isFinishDash = false;
        enemy.WeaponsController.SetDamageRightWeapon(data.rightDamage);
        enemy.WeaponsController.SetDamageLeftWeapon(data.leftDamage);
        direction = enemy.GetPlayerDirection();
        
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
        enemy.transform.position += direction * data.dashSpeed * Time.deltaTime;
        if (Time.time >= startTime + data.dashTime)
        {
            isFinishDash = true;
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