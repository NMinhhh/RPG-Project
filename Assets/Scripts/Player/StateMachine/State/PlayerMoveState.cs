using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector3 direction;

    private bool isAttack;

    private bool isStrongAttack;

    private bool isJump;

    private bool isBlock;

    private bool isAim;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        isAttack = false;
        player.ResetComboAttack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;
        isBlock = InputManager.Instance.blockInput;
        isJump = InputManager.Instance.jumpInput;
        isStrongAttack = InputManager.Instance.strongAttackInput;
        isAttack = InputManager.Instance.attackInput;
        isAim = InputManager.Instance.aimInput;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Move(direction, data.speed);

        if (isJump)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isAim && player.weaponsController.isEquippedBow)
        {
            stateMachine.ChangeState(player.AimState);
        }
        else if (isBlock && player.weaponsController.isEquippedWeapon)
        {
            stateMachine.ChangeState(player.BlockState);
        }
        else if (isStrongAttack && player.weaponsController.isEquippedWeapon && PlayerStats.Instance.EnoughStamina(data.strongAttackStamina))
        {
            stateMachine.ChangeState(player.StrongAttack);
        }
        else if (isAttack && player.weaponsController.isEquippedWeapon && PlayerStats.Instance.EnoughStamina(data.attackStamina))
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (direction.magnitude < .1f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
