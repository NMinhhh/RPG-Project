using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    private bool isAim;
    private bool isShoot;
    public PlayerAimState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetLayerWeight(1, 1);
        player.weaponsController.StartArrowShoot();
        player.weaponsController.BowStringPull();

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
        isShoot = InputManager.Instance.attackInput;
        isAim = InputManager.Instance.aimInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isShoot)
        {
            stateMachine.ChangeState(player.ShootState);
        }
        else if (!isAim)
        {
            player.weaponsController.EndArrowShoot();
            player.weaponsController.BowStringNotPull();
            player.Anim.SetLayerWeight(1, 0);
            stateMachine.ChangeState(player.IdleState);
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
