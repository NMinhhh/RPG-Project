using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Enemy
{
    #region State

    public MutantPlayerDetectedState PlayerDetectedState {  get; private set; }

    public MutantChaseState ChaseState { get; private set; }

    public MutantMeleeattackState MeleeattackState { get; private set; }

    public MutantSwingState SwingState { get; private set; }

    public MutantHurtState HurtState { get; private set; }

    public MutantDeathState DeathState { get; private set; }

    #endregion

    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;
    [SerializeField] private EnemyShieldAttackData swingData;

    #endregion

    protected override void Start()
    {
        base.Start();
        PlayerDetectedState = new MutantPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        ChaseState = new MutantChaseState(this, StateMachine, "Chase", chaseData, this);
        MeleeattackState = new MutantMeleeattackState(this, StateMachine, "MeleeAttack", meleeAttackData, this);
        SwingState = new MutantSwingState(this, StateMachine, "Swing", swingData, this);
        HurtState = new MutantHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new MutantDeathState(this, StateMachine, "Death", deathData, this);
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
