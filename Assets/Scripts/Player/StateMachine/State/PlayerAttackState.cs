using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private Vector3 direction;
    private bool isMove;
    private float moveTime;
    private bool isAttack;
    private bool isStrongAttack;
    private bool isBlock;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isBlock = false;
        isMove = false;
        moveTime = data.attackMoveTime;
        isAttack = false;
        isStrongAttack = false;
        player.Anim.SetInteger("Combo", player.GetComboAttack());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;

        if (InputManager.Instance.attackInput)
        {
            isAttack = true;
        }
        if(InputManager.Instance.strongAttackInput)
        {
            isAttack = false;
            isStrongAttack = true;
        }
        if (InputManager.Instance.blockInput)
        {
            isBlock = true;
            isAttack = false;
            isStrongAttack = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isMove)
        {
            if (Time.time <= startTimer + moveTime)
            {
                player.Move(direction, data.speed);
            }
        }
        if (isFinishAnimtion)
        {
            if (isBlock)
            {
                stateMachine.ChangeState(player.BlockState);
            }
            else if (isStrongAttack)
            {
                stateMachine.ChangeState(player.StrongAttack);
            }
            else if (isAttack)
            {
                stateMachine.ChangeState(player.AttackState);
            }
            else if (direction.magnitude > .1f)
            {
                stateMachine.ChangeState(player.MoveState);
            }else
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
        startTimer = Time.time;
        isMove = true;

    }
}
