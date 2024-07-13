using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected EnemyChaseData data;
    protected bool isPlayerInRange;
    protected bool isPlayerDetected;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyChaseData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerInRange = enemy.CheckPlayerInRange();
        isPlayerDetected = enemy.CheckPlayerDetected();
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
        enemy.transform.LookAt(new Vector3(enemy.PlayerPos.position.x,enemy.transform.position.y,enemy.PlayerPos.position.z));

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
