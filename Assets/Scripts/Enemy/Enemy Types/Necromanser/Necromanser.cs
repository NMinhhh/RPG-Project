using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromanser : Enemy
{

    #region State

    public NecromanserPlayerDetectedState PlayerDetectedState { get; private set; }

    public NecromanserChaseState ChaseState { get; private set; }

    public NecromanserMeleeAttackState MeleeAttackState { get; private set; }

    public NecromanserThrowWeaponState ThrowWeaponState { get; private set; }

    public NecromanserHurtState HurtState { get; private set; } 

    public NecromanserDeathState DeathState { get; private set; }

    #endregion


    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyThrowWeaponData throwWeaponData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;

    #endregion

    #region Transform

    [SerializeField] private GameObject weaponToThrow;
    #endregion

    protected override void Start()
    {
        base.Start();
        PlayerDetectedState = new NecromanserPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        ChaseState = new NecromanserChaseState(this, StateMachine, "Chase", chaseData, this);
        MeleeAttackState = new NecromanserMeleeAttackState(this, StateMachine, "MeleeAttack", meleeAttackData, this);
        ThrowWeaponState = new NecromanserThrowWeaponState(this, StateMachine, "ControlWPAttack", throwWeaponData, weaponToThrow , this);
        HurtState = new NecromanserHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new NecromanserDeathState(this, StateMachine, "Death", deathData, this);
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

    
}
