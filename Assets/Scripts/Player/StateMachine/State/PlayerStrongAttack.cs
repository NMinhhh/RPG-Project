using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrongAttack : PlayerState
{
    private Vector3 direction;
    private float moveTime;
    private bool isMove;
    private bool isBlock;
    private bool isAim;
    public PlayerStrongAttack(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.weaponsController.StrongAttack();
        PlayerStats.Instance.UseStamina(data.strongAttackStamina);
        isMove = false;
        player.ResetComboAttack();
        moveTime = data.strongAttackMoveTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!player.lockOn)
            direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;
        else
        {
            direction = player.lockOnDirection;
            player.transform.forward = direction;
        }
        isBlock = InputManager.Instance.blockInput;
        isAim = InputManager.Instance.aimInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isMove)
        {
            if (Time.time <= startTimer + moveTime)
            {
                player.Move(direction, data.speed);
            }
        }
        if (isFinishAnimtion)
        {
            if (isAim && player.weaponsController.isEquippedBow)
            {
                stateMachine.ChangeState(player.AimState);
            }
            else if (isBlock)
            {

            }
            else if(direction.magnitude > .1f)
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

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
        isMove = true;
        startTimer = Time.time;
    }
}
