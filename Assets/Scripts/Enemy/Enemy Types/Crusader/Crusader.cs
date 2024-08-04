using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusader : Enemy
{
    #region State

    public CrusaderPlayerDetectedState PlayerDetectedState { get; private set; }

    public CrusaderChaseState ChaseState { get; private set; }

    public CrusaderMeleeAttackState MeleeAttackState { get; private set; }

    public CrusaderHurtState HurtState { get; private set; }

    public CrusaderDeathState DeathState { get; private set; }

    public CrusaderShieldAttackState ShieldAttackState {  get; private set; } 

    #endregion


    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;
    [SerializeField] private EnemyShieldAttackData shieldAttackData;

    #endregion


    protected override void Start()
    {
        base.Start();
        PlayerDetectedState = new CrusaderPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        ChaseState = new CrusaderChaseState(this, StateMachine, "Chase", chaseData, this);
        MeleeAttackState = new CrusaderMeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackData, this);
        HurtState = new CrusaderHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new CrusaderDeathState(this, StateMachine, "Death", deathData, this);
        ShieldAttackState = new CrusaderShieldAttackState(this, StateMachine, "ShieldAttack", shieldAttackData, this);
        StateMachine.Intialize(PlayerDetectedState);
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (isDie)
        {
            StateMachine.ChangeState(DeathState);
        }
        if(isHurt && StateMachine.CurrentEnemyState != HurtState)
        {
            StateMachine.ChangeState(HurtState);
        }
    }
}
