using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Vector3 direction;
    private bool isAttack;
    private bool isJump;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttack = false;
        isJump = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        isAttack = InputManager.Instance.attackInput;

        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput);

        if (InputManager.Instance.jumpInput && player.CheckGround())
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAttack)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (direction.magnitude >= .1f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (isJump)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
