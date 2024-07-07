using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerState
{
    private float _animClipLength;
    private float _animClipSpeed;

    public PlayerLandingState(Player player, PlayerStateMachine stateMachine, string isAnimationName) : base(player, stateMachine, isAnimationName)
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
            stateMachine.ChangeState(player.PlayerIdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
