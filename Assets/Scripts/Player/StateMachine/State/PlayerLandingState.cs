using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerState
{
    private Vector3 direction;
    public PlayerLandingState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
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

    public override void HandleInput()
    {
        base.HandleInput();
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Move(direction, data.speed);
        if (isFinishAnimtion)
        {
            if (direction.magnitude > .1f)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
