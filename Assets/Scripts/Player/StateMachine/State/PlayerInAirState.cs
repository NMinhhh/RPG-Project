using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    Vector3 direction;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
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

        player.Move(direction);

        if (player.CheckGround() && player.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.LandingState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
