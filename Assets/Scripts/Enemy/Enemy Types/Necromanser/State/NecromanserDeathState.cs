using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserDeathState : EnemyDeathState
{
    private Necromanser necromanser;

    public NecromanserDeathState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyDeathData data, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data)
    {
        this.necromanser = necromanser;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.GetComponent<CapsuleCollider>().enabled = false;    
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
        if (isFinishAnimation)
        {
            isFinishAnimation = false;
            enemy.Die();
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
