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

    #endregion

    #region Data

    [SerializeField] private EnemyIdleData _idleData;

    [SerializeField] private EnemyHurtData _hurtData;

    [SerializeField] private EnemyMoveData _moveData;

    [SerializeField] private EnemyChaseData _chaseData;

    [SerializeField] private EnemyMeleeAttackData _meleeAttackData;
    #endregion

    protected override void Start()
    {
        base.Start();
        IdleState = new DemonIdleState(this, StateMachine, "Idle", _idleData, this);
        HurtState = new DemonHurtState(this, StateMachine, "Hurt", _hurtData, this);
        MoveState = new DemonMoveState(this, StateMachine, "Move", _moveData, this);
        ChaseState = new DemonChaseState(this, StateMachine, "Chase", _chaseData, this);
        MeleeAttackState = new DemonMeleeAttackState(this, StateMachine, "MeleeAttack", _meleeAttackData, this);
        StateMachine.Intialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (isHurt && StateMachine.CurrentEnemyState != HurtState && !isDie && StateMachine.CurrentEnemyState != MeleeAttackState)
        {
            StateMachine.ChangeState(HurtState);
        }
        if (isDie)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, _idleData.checkDistanceRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _meleeAttackData.inRangeAttack);
    }
}
