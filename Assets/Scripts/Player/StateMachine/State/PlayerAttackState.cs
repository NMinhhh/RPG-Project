using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int _comboAttack;
    private bool isAttack;
    private Vector3 _direction;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttack = false;
        player.SetComboAttack();
        player.IsAttack(true);
        _comboAttack = player.CurrentComboAttack;
        player.Anim.SetInteger("Combo", _comboAttack);
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
                stateMachine.ChangeState(player.PlayerAttackState);
            }
            else if (_direction.magnitude > .1f)
            {
                player.ResetComboAttack();
                stateMachine.ChangeState(player.PlayerIdleState);
            }else
            {
                player.ResetComboAttack();
                stateMachine.ChangeState(player.PlayerIdleState);
            }
        }
       
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
