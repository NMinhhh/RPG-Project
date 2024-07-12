using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboAttack;
    private bool isAttack;
    private Vector3 _direction;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttack = false;
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

        _direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;

        if (InputManager.Instance.attackInput)
        {
            isAttack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isFinishAnimtion)
        {
            if (isAttack)
            {
                stateMachine.ChangeState(player.AttackState);
            }
            else if (_direction.magnitude > .1f)
            {
                player.ResetComboAttack();
                stateMachine.ChangeState(player.IdleState);
            }else
            {
                player.ResetComboAttack();
                stateMachine.ChangeState(player.IdleState);
            }
        }
       
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
