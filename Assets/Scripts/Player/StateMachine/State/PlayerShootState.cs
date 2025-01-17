using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerState
{
    private Transform shootPoint;
    private bool isAim;
    private GameObject go;
    private Projectile projectile;
    private Vector3 direction;

    public PlayerShootState(Player player, PlayerStateMachine stateMachine, PlayerData data, string isAnimationName, Transform shootPoint) : base(player, stateMachine, data, isAnimationName)
    {
        this.shootPoint = shootPoint;
    }

    public override void Enter()
    {
        base.Enter();
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Shoot Arrow"), player.transform.position);
        player.thirdPersonAim.Aim();
        direction = (player.thirdPersonAim.mousePos - shootPoint.position).normalized;
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
        go = ObjectPool.Instance.SpawnFromPool("Arrow", shootPoint.position, Quaternion.LookRotation(direction, Vector3.up));
        projectile = go.GetComponent<Projectile>();
        projectile.CreateProjectile(data.arrowDamage, data.arrowSpeed, data.arrowTimeLife);
    }
}
