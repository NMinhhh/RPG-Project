using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName) : base(player, stateMachine, data, isAnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Player Hurt"), player.transform.position);
        player.weaponsController.EndDealDamageAll();
        player.Anim.SetLayerWeight(1, 0);
        player.thirdPersonAim.NotAim();
        player.weaponsController.EndArrowShoot();
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
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
