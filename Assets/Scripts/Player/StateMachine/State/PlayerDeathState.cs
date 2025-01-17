using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Player Hurt"), player.transform.position);
        player.weaponsController.EndDealDamageAll();
        player.Anim.SetLayerWeight(1, 0);
        player.thirdPersonAim.NotAim();
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isFinishAnimtion)
        {
            isFinishAnimtion = false;
            player.Die();
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
