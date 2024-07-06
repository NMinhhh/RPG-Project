using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    Vector3 direction;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
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

        direction = new Vector3(InputManager.Instance.XInput, 0, InputManager.Instance.ZInput).normalized;
        player.Move(direction);

        if(!player.isGround && player.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.PlayerInAirState);
        }
        else if (InputManager.Instance.JumpInput)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
        else if(direction.magnitude < .1f)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
