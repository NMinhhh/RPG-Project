using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;

    protected PlayerStateMachine stateMachine;

    protected string isAnimationName;

    protected float startTimer;

    protected bool isFinishAnimtion;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string isAnimationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.isAnimationName = isAnimationName;
    }


    public virtual void Enter()
    {
        startTimer = Time.time;
        isFinishAnimtion = false;
        player.Anim.SetBool(isAnimationName, true);
    }

    public virtual void Exit() => player.Anim.SetBool(isAnimationName, false);

    public virtual void LogicUpdate() { }
    public virtual void PhysicUpdate() { }

    public virtual void TriggerAnimation() { }

    public virtual void FinishAnimation() => isFinishAnimtion = true;
}
