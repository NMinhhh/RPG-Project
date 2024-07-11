using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected string isAnimationName;
    protected float startTime;
    protected bool isFinishAnimation;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.isAnimationName = isAnimationName;
    }

    public virtual void DoCheck() { }
    public virtual void Enter()
    {
        startTime = Time.time;
        isFinishAnimation = false;
        enemy.Anim.SetBool(isAnimationName, true);
        DoCheck();
    }

    public virtual void Exit() => enemy.Anim.SetBool(isAnimationName,false);

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() => DoCheck();

    public virtual void TriggerAnimation() { }

    public virtual void FinishAnimation() => isFinishAnimation = true;
}
