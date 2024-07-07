using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Vector3 _direction;
    private bool _isAttack;
    private bool _isJump;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isAttack = false;
        _isJump = false;
        player.Move(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        _isAttack = InputManager.Instance.AttackInput;

        _direction = new Vector3(InputManager.Instance.XInput, 0, InputManager.Instance.ZInput);

        if (InputManager.Instance.JumpInput && player.isGround)
        {
            _isJump = true;
        }
        else
        {
            _isJump = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAttack)
        {
            stateMachine.ChangeState(player.PlayerAttackState);
        }
        else if (_direction.magnitude >= .1f)
        {
            stateMachine.ChangeState(player.PlayerMoveState);
        }
        else if (_isJump)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
