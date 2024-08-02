using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    protected EnemyDeathData data;
    public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data) : base(enemy, stateMachine, isAnimationName)
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
        enemy.GetComponent<Collider>().enabled = false;
        enemy.WeaponsController.EndDealDamageAll();
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
