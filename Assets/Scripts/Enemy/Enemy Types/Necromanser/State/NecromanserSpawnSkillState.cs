using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserSpawnSkillState : EnemySpawnObjectsState
{
    private Necromanser necromanser;
    private GameObject go;
    private EnemyProjectile projectile;
    public NecromanserSpawnSkillState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemySpawnObjectsData data, Transform[] spawnPoints, Necromanser necromanser) : base(enemy, stateMachine, isAnimationName, data, spawnPoints)
    {
        this.necromanser = necromanser;
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
        if (isFinishPrep && !isFinishSpawn)
        {
            if(Time.time >= startTime + data.spawnDelayTime && amountOfObject > 0)
            {
                Vector3 point = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                Vector3 direction = (new Vector3(enemy.playerPos.position.x, enemy.playerPos.position.y + data.offsetY, enemy.playerPos.position.z) - point).normalized;
                go = GameObject.Instantiate(data.spawnObject, point, Quaternion.LookRotation(direction,Vector3.up));
                projectile = go.GetComponent<EnemyProjectile>();
                projectile.CreateProjectile(data.damage, data.speed, data.timeLife);
                amountOfObject--;
                startTime = Time.time;

            }
            if(amountOfObject <= 0 && !isFinishSpawn)
            {
                enemy.Anim.SetTrigger("isFinishSpawn");
                isFinishSpawn = true;
            }
        }
        if (isFinishAnimation)
        {
            enemy.SpawnCooldown();
            stateMachine.ChangeState(necromanser.PlayerDetectedState);
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
