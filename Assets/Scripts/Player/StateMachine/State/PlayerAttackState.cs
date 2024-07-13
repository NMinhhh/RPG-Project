using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private Vector3 direction;
    private bool isMove;
    private float moveTime;
    private bool isAttack;
    private int comboAttack;
    private bool isStrongAttack;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isMove = false;
        moveTime = data.attackMoveTime;
        isAttack = false;
        isStrongAttack = false;
        player.IsAttack(true);
        player.Anim.SetInteger("Combo", player.GetComboAttack());
    }

    public override void Exit()
    {
        base.Exit();
        player.IsAttack(false);
        
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
            if (isStrongAttack)
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
