using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ghoul : Enemy
{
    #region State

    public GhoulPlayerDetectedState PlayerDetectedState { get; private set; }

    public GhoulRangeAttackState RangeAttackState { get; private set; }

    public GhoulHurtState HurtState { get; private set; }

    public GhoulDeathState DeathState { get; private set; }

    #endregion

    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyRangeAttackData rangeAttackData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;

    #endregion

    #region Transform

    [SerializeField] private Transform attackPoint;

    #endregion

    protected override void Start()
    {
        base.Start();
        PlayerDetectedState = new GhoulPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        RangeAttackState = new GhoulRangeAttackState(this, StateMachine, "RangeAttack", rangeAttackData, attackPoint, this);
        HurtState = new GhoulHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new GhoulDeathState(this, StateMachine, "Death", deathData, this);
        StateMachine.Intialize(PlayerDetectedState);
    }

    protected override void Update()
    {
        base.Update();
    }


    public override void Damage(float damage)
    {
        base.Damage(damage);
        if(isHurt && StateMachine.CurrentEnemyState != HurtState)
        {
            StateMachine.ChangeState(HurtState);
        }
        if (isDie)
        {
            StateMachine.ChangeState(DeathState);
        }
    }

    public override void Die()
    {
        base.Die();
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        if (StateMachine == null) return;
        StateMachine.ChangeState(PlayerDetectedState);
    }


    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if (StateMachine == null) return;
        StateMachine.ChangeState(PlayerDetectedState);
    }

}
