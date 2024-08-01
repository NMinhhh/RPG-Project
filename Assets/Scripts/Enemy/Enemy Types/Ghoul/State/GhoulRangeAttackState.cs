using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulRangeAttackState : EnemyRangeAttackState
{
    private Ghoul ghoul;
    private GameObject go;
    private EnemyProjectile projectile;

    public GhoulRangeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyRangeAttackData data, Transform attackPoint, Ghoul ghoul) : base(enemy, stateMachine, isAnimationName, data, attackPoint)
    {
        this.ghoul = ghoul;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isFinishAnimation)
        {
            stateMachine.ChangeState(ghoul.PlayerDetectedState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
        Vector3 direction = (new Vector3(enemy.playerPos.position.x, enemy.playerPos.position.y + data.offsetY, enemy.playerPos.position.z) - attackPoint.position).normalized;
        go = ObjectPool.Instance.SpawnFromPool("GhoulProjectile", attackPoint.position, Quaternion.LookRotation(direction, Vector3.up));
        projectile = go.GetComponent<EnemyProjectile>();
        projectile.CreateProjectile(data.damage, data.speed, data.timeLife);
    }
}
