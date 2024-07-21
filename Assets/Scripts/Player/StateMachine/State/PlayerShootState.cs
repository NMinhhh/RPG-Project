using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerState
{
    private Transform shootPoint;
    private bool isAim;

    public PlayerShootState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName, Transform shootPoint) : base(player, stateMachine, data, isAnimationName)
    {
        this.shootPoint = shootPoint;
    }

    public override void Enter()
    {
        base.Enter();
        player.thirdPersonAim.Aim();
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
        isAim = InputManager.Instance.aimInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.thirdPersonAim.Aim();
        if (isFinishAnimtion)
        {
            if (isAim)
            {
                stateMachine.ChangeState(player.AimState);
            }
            else
            {
                player.Anim.SetLayerWeight(1, 0);
                player.thirdPersonAim.NotAim();
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
        Vector3 direction = (player.thirdPersonAim.mousePos - shootPoint.position).normalized;
        //GameObject.Instantiate(data.arrow, shootPoint.position, Quaternion.LookRotation(direction, Vector3.up));
        ObjectPool.Instance.SpawnFromPool(Pool.Type.Arrow, shootPoint.position, Quaternion.LookRotation(direction, Vector3.up));
    }
}
