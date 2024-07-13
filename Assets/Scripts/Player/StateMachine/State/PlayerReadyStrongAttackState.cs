using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyStrongAttackState : PlayerState
{
    private float readyTime;
    private bool isStrongAttack;
    public PlayerReadyStrongAttackState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        readyTime = data.readyTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        isStrongAttack = InputManager.Instance.strongAttackInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > readyTime + startTimer && !isStrongAttack && isFinishAnimtion)
        {
            stateMachine.ChangeState(player.StrongAttack);
        }
        else if(!isStrongAttack)
        {
            stateMachine.ChangeState(player.IdleState);
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
