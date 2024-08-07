using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    Vector3 direction;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.character.center += data.centerOffset;
    }

    public override void Exit()
    {
        base.Exit();
        player.character.center -= data.centerOffset;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Move(direction, data.jumpSpeed);

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
