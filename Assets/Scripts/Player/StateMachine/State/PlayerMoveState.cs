using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector3 _direction;

    private bool _isGrounded;

    private bool _isJump;

    private bool _isAttack;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        _direction = new Vector3(InputManager.Instance.XInput, 0, InputManager.Instance.ZInput).normalized;
        if (InputManager.Instance.JumpInput && player.isGround)
        {
            _isJump = true;
        }
        else
        {
            _isJump = false;
        }

        if (InputManager.Instance.AttackInput)
        {
            _isAttack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _isGrounded = player.isGround;

        player.Move(_direction);

        if (_isAttack)
        {
            stateMachine.ChangeState(player.PlayerAttackState);
        }
        else if(!_isGrounded && player.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.PlayerInAirState);
        }
        else if (_isJump)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
        else if(_direction.magnitude < .1f)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
