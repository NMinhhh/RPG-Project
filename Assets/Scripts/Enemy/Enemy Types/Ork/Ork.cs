using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Ork : Enemy
{

    #region State

    public OrkIdleState IdleState {  get; private set; }

    public OrkMoveState MoveState { get; private set; }

    public OrkPlayerDetectedState PlayerDetectedState { get; private set; }

    public OrkChaseState ChaseState { get; private set; }

    public OrkMeleeAttackState MeleeAttackState { get; private set; }

    public OrkHurtState HurtState { get; private set; }

    public OrkDeathState DeathState { get; private set; }

    public OrkDashAttackState DashAttackState { get; private set; }

    public OrkRoarState RoarState { get; private set; }
    #endregion

    #region Data

    [SerializeField] private EnemyIdleData idleData;
    [SerializeField] private EnemyMoveData moveData;
    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;
    [SerializeField] private EnemyDashAttackData dashAttackData;
    [SerializeField] private EnemyRoarData roarData;
    #endregion



    protected override void Start()
    {
        base.Start();
        IdleState = new OrkIdleState(this, StateMachine, "Idle", idleData, this);
        MoveState = new OrkMoveState(this, StateMachine, "Move", moveData, destinations, this);
        PlayerDetectedState = new OrkPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        MeleeAttackState = new OrkMeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackData, this);
        ChaseState = new OrkChaseState(this, StateMachine, "Chase", chaseData, this);
        HurtState = new OrkHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new OrkDeathState(this, StateMachine, "Death", deathData, this);
        DashAttackState = new OrkDashAttackState(this, StateMachine, "DashAttack", dashAttackData, this);
        RoarState = new OrkRoarState(this, StateMachine, "Roar", roarData, this);
        StateMachine.Intialize(PlayerDetectedState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (isHurt && StateMachine.CurrentEnemyState != HurtState && !isDie && StateMachine.CurrentEnemyState != DashAttackState)
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

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if (StateMachine == null) return;
        StateMachine.ChangeState(PlayerDetectedState);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

   
}
