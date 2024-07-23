using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EnemyState
{
    protected EnemyRangeAttackData data;
    protected Transform attackPoint;
   
    public EnemyRangeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyRangeAttackData data, Transform attackPoint) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
        this.attackPoint = attackPoint;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
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
