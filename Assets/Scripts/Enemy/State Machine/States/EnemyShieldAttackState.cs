using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldAttackState : EnemyState
{
    protected EnemyShieldAttackData data;
    protected bool isPlayerDetected;
    public EnemyShieldAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyShieldAttackData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerDetected = enemy.CheckPlayerDetected();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.WeaponsController.SetDamageLeftWeapon(data.damageLeft);
        enemy.WeaponsController.SetDamageRightWeapon(data.damageRight);
        enemy.SetSpeed(0);
        enemy.Move(enemy.transform.position);
        enemy.transform.LookAt(new Vector3(enemy.playerPos.position.x, enemy.transform.position.y, enemy.playerPos.position.z));
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
