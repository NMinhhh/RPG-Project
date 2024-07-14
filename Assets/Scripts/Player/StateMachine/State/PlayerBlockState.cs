using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    private float parryTime;
    private Vector3 direction;
    private bool isBlock;
    public PlayerBlockState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.IsParry(true);
        parryTime = data.parryTime;
        player.Move(Vector3.zero, 0);
    }

    public override void Exit()
    {
        base.Exit();
        player.IsParry(false);
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        direction = new Vector3(InputManager.Instance.xInput, 0, InputManager.Instance.zInput).normalized;
        isBlock = InputManager.Instance.blockInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTimer + parryTime)
        {
            player.IsParry(false);
        }
        if (!isBlock)
        {
            if(direction.magnitude > .1f)
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
    }
}
