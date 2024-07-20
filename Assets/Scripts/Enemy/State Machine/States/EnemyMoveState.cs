using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected EnemyMoveData data;
    protected Vector3 randomPos;
    protected bool isFinishMove;
    protected bool isPlayerDetected;
    protected Transform[] destination;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyMoveData data, Transform[] destination) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
        this.destination = destination;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(enemy.CheckPlayerDetected() && !enemy.CheckBlock())
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }
        if (enemy.GetDistance(enemy.transform.position, randomPos) <= 0.1f)
        {
            isFinishMove = true;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isFinishMove = false;
        randomPos = destination[Random.Range(0,destination.Length)].position;
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
