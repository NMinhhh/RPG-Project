using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Vector3 direction;
    private bool isAttack;
    private bool isStrongAttack;
    private bool isJump;
    private bool isBlock;
    private bool isAim;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
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
        isBlock = InputManager.Instance.blockInput;
        isAttack = InputManager.Instance.attackInput;
        isStrongAttack = InputManager.Instance.strongAttackInput;
        isJump = InputManager.Instance.jumpInput;
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput);
        isAim = InputManager.Instance.aimInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isJump) 
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isAim && player.weaponsController.isEquippedWeapon)
        {
            stateMachine.ChangeState(player.AimState);
        }
        else if (isBlock && player.weaponsController.isEquippedWeapon)
        {
            stateMachine.ChangeState(player.BlockState);
        }
        else if (isStrongAttack && player.weaponsController.isEquippedWeapon)
        {
            stateMachine.ChangeState(player.StrongAttack);
        }
        else if (isAttack && player.weaponsController.isEquippedWeapon)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (direction.magnitude >= .1f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
