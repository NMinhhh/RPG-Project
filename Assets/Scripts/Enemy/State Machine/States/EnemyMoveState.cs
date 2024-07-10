using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected EnemyMoveData data;
    protected Vector3 randomPos;
    protected bool isFinishMove;
    protected bool isPlayerDetected;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMoveData data) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerDetected = false;
        isFinishMove = false;
        randomPos = enemy.GetRandomPos(data.XPosRandom, data.YPosRandom);
        enemy.SetSpeed(data.Speed);
        enemy.transform.LookAt(randomPos);
        enemy.Move(randomPos);
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
        if(enemy.CheckDistance(enemy.transform.position, randomPos) <= 0.5f)
        {
            isFinishMove = true;
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
