using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int _comboAttack;
    private float _animClipLength;
    private float _animClipSpeed;
    private bool isAttack;
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
        if (InputManager.Instance.AttackInput)
        {
            isAttack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        _animClipLength = player.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        _animClipSpeed = player.Anim.GetCurrentAnimatorStateInfo(0).speed;

        if (isAttack && Time.time >= startTimer + _animClipLength / _animClipSpeed)
        {
            stateMachine.ChangeState(player.PlayerAttackState);
        }
        else if (Time.time >= startTimer + _animClipLength / _animClipSpeed)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
