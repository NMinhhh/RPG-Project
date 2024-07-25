using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowWeaponState : EnemyState
{
    protected EnemyThrowWeaponData data;
    private GameObject weaponToThrow;
    private EnemyBomerang bomerang;
    private bool isFinishThrow;
    private bool isFinishAttack;
    protected bool isFinishCatch;

    public EnemyThrowWeaponState(Enemy enemy, EnemyStateMachine stateMachine, string isAnimationName, EnemyThrowWeaponData data, GameObject weaponToThrow) : base(enemy, stateMachine, isAnimationName)
    {
        this.data = data;
        this.weaponToThrow = weaponToThrow;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        isFinishThrow = false;
        isFinishAttack = false;
        isFinishCatch = false;
        enemy.SetSpeed(0);
        enemy.Move(enemy.transform.position);
        enemy.transform.LookAt(new Vector3(enemy.playerPos.position.x, enemy.transform.position.y, enemy.playerPos.position.z));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Check finish throw animation  
        if (Time.time > startTime + data.throwTime && !isFinishThrow) 
        {
            weaponToThrow.SetActive(true);
            enemy.WeaponsController.currentRightWeapon.gameObject.SetActive(false);
            enemy.Anim.SetTrigger("isFinishThrow");
            isFinishThrow = true;
            WeaponStartAttack();
            startTime = Time.time;
        }
        //Check finish attack
        if (isFinishThrow && !isFinishAttack)
        {
            isFinishAttack = bomerang.isFinsishMove;
            if (isFinishAttack)
            {
                weaponToThrow.SetActive(false);
                enemy.WeaponsController.currentRightWeapon.gameObject.SetActive(true);
                enemy.Anim.SetTrigger("isCatch");
                startTime = Time.time;
            }
        }
        //Check finish catch animation
        if(Time.time >= startTime + data.catchTime && isFinishAttack && !isFinishCatch)
        {
            enemy.ThrowCooldown();
            isFinishCatch = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public void WeaponStartAttack()
    {
        bomerang = weaponToThrow.GetComponent<EnemyBomerang>();
        Vector3 direction = (new Vector3(enemy.playerPos.position.x, enemy.playerPos.position.y + data.offsetY, enemy.playerPos.position.z) - weaponToThrow.transform.position).normalized;
        bomerang.SetBomerang(direction, data.damage, data.speed, data.moveTime, weaponToThrow.transform.position);
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }
}
