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

    public NecromanserSpawnSkillState SpawnSkillState { get; private set; }

    #endregion


    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyThrowWeaponData throwWeaponData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;
    [SerializeField] private EnemySpawnObjectsData spawnObjectsData;
    #endregion

    #region Transform

    [SerializeField] private GameObject weaponToThrow;
    [SerializeField] private Transform[] spawnPoints;
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
        SpawnSkillState = new NecromanserSpawnSkillState(this, StateMachine, "SpawnKill", spawnObjectsData, spawnPoints, this);
        StateMachine.Intialize(PlayerDetectedState);
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
        if (isHurt && StateMachine.CurrentEnemyState != HurtState && StateMachine.CurrentEnemyState != ThrowWeaponState && StateMachine.CurrentEnemyState != SpawnSkillState)
        {
            StateMachine.ChangeState(HurtState);
        }
        
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if (StateMachine == null) return;
        StateMachine.ChangeState(PlayerDetectedState);
    }

    public override void Die()
    {
        base.Die();
    }

    
}
