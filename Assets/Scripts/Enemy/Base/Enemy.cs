using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IKnockBackable
{
    #region State Machine

    public EnemyStateMachine StateMachine { get; private set; }

    #endregion

    #region Data

    [SerializeField] protected EnemyData data;

    #endregion

    #region Component Variable

    public Animator Anim { get; private set; }

    protected GameObject alive;

    public NavMeshAgent Agent {  get; private set; }

    public EnemyWeaponController WeaponsController { get; private set; }

    public EnemyHealthBar HealthBar { get; private set; }

    #endregion

    #region Transform

    [SerializeField] protected Transform[] destinations; 

    #endregion

    #region Variable

    public Transform playerPos { get; private set; }

    //Health
    protected float maxHelth;

    protected float currentHealth;

    protected bool isHurt;

    public bool isDie {  get; private set; }

    public Vector3 damageDir {  get; private set; }

    protected int maxCombo;
    protected int currentCombo;

    //Amount of damage to change hurt state
    protected int currentAODToHurt;

    //Amount of attack to change state
    public int amountOfAttack {  get; private set; }

    //Check first damage to attack player
    protected bool isFirstDamage;

    //Check to dash
    protected float currentDashCooldown;
    protected bool canDash;

    #endregion

    #region Unity Function

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        Anim = alive.GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
        WeaponsController = GetComponent<EnemyWeaponController>();
        maxHelth = data.MaxHealthData;
        currentHealth = maxHelth;
        maxCombo = data.maxCombo;
        currentAODToHurt = data.amountOfDamageToHurt;
        currentDashCooldown = Random.Range(data.dashCooldownRan.x, data.dashCooldownRan.y);
        isFirstDamage = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        StateMachine.CurrentEnemyState.LogicUpdate();
        if (data.isDash)
        {
            CheckToDash();
        }
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicUpdate();
    }

    #endregion

    #region Move Fuction

    public void SetSpeed(float speed)
    {
        Agent.speed = speed;
    }

    public void Move(Vector3 target)
    {
        Agent.SetDestination(target);
    }

    public Vector3 GetRandomPos(Vector2 ranX, Vector2 ranY)
    {
        return transform.position + new Vector3(Random.Range(ranX.x, ranX.y), 0, Random.Range(ranY.x, ranY.y));
    }

    #endregion

    #region Health Function

    public virtual void Damage(float damage)
    {
        isHurt = false;
        currentAODToHurt--;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHelth);
        HealthBar.UpdateHealthBar(currentHealth, maxHelth);
        if (currentHealth > 0 && currentAODToHurt <= 0)
        {
            currentAODToHurt = data.amountOfDamageToHurt;
            isHurt = true;
        }
        else if(currentHealth <= 0)
        {
            isDie = true;
            HealthBar.gameObject.SetActive(false);
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    

    #endregion

    #region Check Function

    public float GetDistance(Vector3 orginal, Vector3 target)
    {
        return Vector3.Distance(orginal, target);
    }

    public bool CheckPlayerDetected()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.radiusCheckToChase;
    }

    public bool CheckPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.radiusCheckToAttack;
    }

    public bool CheckBlock()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);
        return Physics.Raycast(transform.position + Vector3.up, (playerPos.position - transform.position).normalized, distance, data.whatIsWall);
    }

    #endregion

    #region Knock Back Function

    public void DamageDiretion(Vector3 direction)
    {
        damageDir = direction;
    }

    public void KnockBack()
    {
        transform.position += damageDir * data.knockBackSpeed;
    }

    #endregion

    #region Attack Function

    public void IncreaseAmountOfAttack()
    {
        amountOfAttack++;
    }

    public void ResetAmountOfAttack()
    {
        amountOfAttack = 0;
    }
    public int GetCurrentCombo()
    {
        currentCombo++;
        if(currentCombo > maxCombo)
        {
            currentCombo = 1;
        }
        return currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }

    #endregion

    #region Is First Damage Function

    public bool IsFirstDamage()
    {
        return isFirstDamage;
    }

    public void SetIsFirstDamage(bool isFirstDamage)
    {
        this.isFirstDamage = isFirstDamage;
    }

    #endregion

    #region Dash Function

    public void Dash(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void CheckToDash()
    {
        if (!canDash)
        {
            currentDashCooldown -= Time.deltaTime;
            if (currentDashCooldown <= 0)
            {
                currentDashCooldown = Random.Range(data.dashCooldownRan.x, data.dashCooldownRan.y);
                canDash = true;
            }
        }
    }

    public void SetDashState(bool state)
    {
        canDash = state;
    }

    public bool CanDash()
    {
        return canDash;
    }
    

    #endregion

    #region Others Function

    public virtual Vector3 GetPlayerDirection()
    {
        return (playerPos.position - transform.position).normalized;
    }

    public virtual void TriggerAnimation() => StateMachine.CurrentEnemyState.TriggerAnimation();

    public virtual void FinishAnimation()
    {
       
        StateMachine.CurrentEnemyState.FinishAnimation();
    }

    protected virtual void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.DrawWireSphere(transform.position, data.radiusCheckToChase);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.radiusCheckToAttack);
        }
    }

    #endregion
}
