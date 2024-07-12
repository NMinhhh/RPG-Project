using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{

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

        if (isFinishAnimtion)
        {
            player.Jump();
            stateMachine.ChangeState(player.InAirState);
        }
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
