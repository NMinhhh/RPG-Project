using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float _animClipLength;
    private float _animClipSpeed;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _animClipLength = player.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        _animClipSpeed = player.Anim.GetCurrentAnimatorStateInfo(0).speed;

        if (Time.time >= startTimer + _animClipLength / _animClipSpeed)
        {
            player.SetJumpHeigth(player.JumpHeight);
            stateMachine.ChangeState(player.PlayerInAirState);
        }
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
