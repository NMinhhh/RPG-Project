using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (InputManager.Instance.XInput != 0 || InputManager.Instance.ZInput != 0)
        {
            stateMachine.ChangeState(player.PlayerMoveState);
        }else if (InputManager.Instance.JumpInput && player.isGround)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
