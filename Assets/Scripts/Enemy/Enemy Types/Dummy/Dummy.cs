using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    #region State

    public DummyIdleState IdleState {  get; private set; }

    public DummyHurtState HurtState { get; private set; }

    public DummyDeathState DeathState { get; private set; }

    #endregion

    #region Data

    [SerializeField] private EnemyIdleData idleData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;  

    #endregion

    protected override void Start()
    {
        base.Start();
        IdleState = new DummyIdleState(this, StateMachine, "Idle", idleData, this);
        HurtState = new DummyHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new DummyDeathState(this, StateMachine, "Death", deathData, this);
        StateMachine.Intialize(IdleState);
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (isDie)
        {
            StateMachine.ChangeState(DeathState);
            return;
        }
        if (isHurt)
        {
            StateMachine.ChangeState(HurtState);
        }
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        if (StateMachine == null) return;
        StateMachine.ChangeState(IdleState);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if (StateMachine == null) return;
        StateMachine.ChangeState(IdleState);
    }

}
