using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector3 direction;

    private bool isJump;

    private bool isAttack;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;
        if (InputManager.Instance.jumpInput && player.CheckGround())
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }

        if (InputManager.Instance.attackInput)
        {
            isAttack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Move(direction);

        if (isAttack)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if(!player.CheckGround() && player.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isJump)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(direction.magnitude < .1f)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
