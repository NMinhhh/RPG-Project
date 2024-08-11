using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    #region State

    public DemonIdleState IdleState { get; private set; }

    public DemonHurtState HurtState { get; private set; }

    public DemonMoveState MoveState { get; private set; }

    public DemonChaseState ChaseState { get; private set; }

    public DemonMeleeAttackState MeleeAttackState { get; private set; }

    public DemonPlayerDetectedState PlayerDetectedState { get; private set; }

    public DemonDeathState DeathState { get; private set; }

    #endregion

    #region Data

    [SerializeField] private EnemyIdleData _idleData;

    [SerializeField] private EnemyHurtData _hurtData;

    [SerializeField] private EnemyMoveData _moveData;

    [SerializeField] private EnemyChaseData _chaseData;

    [SerializeField] private EnemyMeleeAttackData _meleeAttackData;

    [SerializeField] private EnemyPlayerDetectedData _playerDetectedData;

    [SerializeField] private EnemyDeathData _deathData;
    #endregion


    protected override void Start()
    {
        base.Start();
        IdleState = new DemonIdleState(this, StateMachine, "Idle", _idleData, this);
        HurtState = new DemonHurtState(this, StateMachine, "Hurt", _hurtData, this);
        MoveState = new DemonMoveState(this, StateMachine, "Move", _moveData, destinations, this);
        ChaseState = new DemonChaseState(this, StateMachine, "Chase", _chaseData, this);
        MeleeAttackState = new DemonMeleeAttackState(this, StateMachine, "MeleeAttack", _meleeAttackData, this);
        PlayerDetectedState = new DemonPlayerDetectedState(this, StateMachine, "PlayerDetected", _playerDetectedData, this);
        DeathState = new DemonDeathState(this, StateMachine, "Death", _deathData, this);
        StateMachine.Intialize(IdleState);

    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (isDie)
        {
            StateMachine.ChangeState(DeathState);
            return;
        }
        if (isHurt && StateMachine.CurrentEnemyState != HurtState && !isDie)
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

    public override void Die()
    {
        base.Die();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
