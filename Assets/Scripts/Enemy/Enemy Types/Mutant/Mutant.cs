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

    public MutantDrawWeaponState DrawWeaponState { get; private set; }

    public MutantRoarState RoarState { get; private set; }

    #endregion

    #region Data

    [SerializeField] private EnemyPlayerDetectedData playerDetectedData;
    [SerializeField] private EnemyChaseData chaseData;
    [SerializeField] private EnemyMeleeAttackData meleeAttackData;
    [SerializeField] private EnemyHurtData hurtData;
    [SerializeField] private EnemyDeathData deathData;
    [SerializeField] private EnemyShieldAttackData swingData;
    [SerializeField] private EnemyDrawWeaponData drawWeaponData;
    [SerializeField] private EnemyRoarData roarData;

    #endregion

    #region Other Variable

    [SerializeField] private GameObject sheath;
    [SerializeField] private EnemyWeapon currentRightWeapon;
    [SerializeField] private EnemyWeapon newRightWeapon;
    [SerializeField] private float newRadiusCheckToAttack;
    [SerializeField] private GameObject effectRage;
    public bool isEquip {  get; private set; }

    #endregion

    protected override void Start()
    {
        base.Start();
        newRightWeapon.gameObject.SetActive(false);
        PlayerDetectedState = new MutantPlayerDetectedState(this, StateMachine, "PlayerDetected", playerDetectedData, this);
        ChaseState = new MutantChaseState(this, StateMachine, "Chase", chaseData, this);
        MeleeattackState = new MutantMeleeattackState(this, StateMachine, "MeleeAttack", meleeAttackData, this);
        SwingState = new MutantSwingState(this, StateMachine, "Swing", swingData, this);
        HurtState = new MutantHurtState(this, StateMachine, "Hurt", hurtData, this);
        DeathState = new MutantDeathState(this, StateMachine, "Death", deathData, this);
        DrawWeaponState = new MutantDrawWeaponState(this, StateMachine, "DrawWeapon", drawWeaponData, this);
        RoarState = new MutantRoarState(this, StateMachine, "Roar", roarData, this);
        StateMachine.Intialize(PlayerDetectedState);
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if(currentHealth <= maxHelth / 2 && !isEquip)
        {
            isEquip = true;
            currentRightWeapon.gameObject.SetActive(false);
            StateMachine.ChangeState(DrawWeaponState);
            return;
        }
        if (isDie)
        {
            newRightWeapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            StateMachine.ChangeState(DeathState);
        }
        if(isHurt && StateMachine.CurrentEnemyState != HurtState && StateMachine.CurrentEnemyState != DrawWeaponState)
        {
            StateMachine.ChangeState(HurtState);
        }
    }

    public void MutantChange()
    {
        WeaponsController.ChangeRightWeapon(newRightWeapon);
        Anim.SetBool("IsEquip", true);
        sheath.SetActive(false);
        newRightWeapon.gameObject.SetActive(true);
        radiusCheckToAttack = newRadiusCheckToAttack;
        effectRage.SetActive(true);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if (StateMachine == null) return;
        WeaponsController.ChangeRightWeapon(currentRightWeapon);
        isEquip = false;
        Anim.SetBool("IsEquip", false);
        sheath.SetActive(true);
        newRightWeapon.gameObject.SetActive(false);
        radiusCheckToAttack = data.radiusCheckToAttack;
        StateMachine.ChangeState(PlayerDetectedState);
        effectRage.SetActive(false);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (isDrawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, newRadiusCheckToAttack);
        }
    }
}
