using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IKnockBackable, IPooledObject
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

    public bool isDrawGizmos;

    public Transform playerPos { get; private set; }

    //Health
    protected float maxHelth;

    protected float currentHealth;

    protected bool isHurt;

    public bool isDie {  get; private set; }

    public Vector3 damageDir {  get; private set; }

    protected int maxCombo;
    public int currentCombo {  get; private set; }

    //Radius check attack
    protected float radiusCheckToAttack;

    //Amount of damage to change hurt state
    protected int currentAODToHurt;

    //Check first damage to attack player
    protected bool isFirstDamage;

    //Check to dash
    protected float currentDashCooldown;
    protected bool canDash;

    //Check to throw weapon
    protected float currentThrowWeaponCooldown;
    protected bool canThrow;

    //Check to spawn
    protected float currentSpawnObjectsCooldown;
    protected bool canSpawn;

    //Boss
    public bool isBoss {  get; private set; }
    //start pos
    private Vector3 startPos;
    private bool isFirstStart;
    private Vector3 startAngle;

    #endregion

    #region Unity Function

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isFirstStart = true;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        Anim = alive.GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        WeaponsController = GetComponent<EnemyWeaponController>();
        startPos = transform.localPosition;
        startAngle = alive.transform.eulerAngles;
        //Radius
        radiusCheckToAttack = data.radiusCheckToAttack;
        //Health
        maxHelth = data.MaxHealthData;
        currentHealth = maxHelth;
        //combo
        maxCombo = data.maxCombo;
        currentAODToHurt = data.amountOfDamageToHurt;
        isFirstDamage = false;
        isBoss = data.isBoss;
        if (!isBoss)
        {
            HealthBar = GetComponentInChildren<EnemyHealthBar>();
            HealthBar.ResetHealthBar();
        }
        else
        {
            BossStats.Instance.SetHealth(currentHealth, maxHelth);
            BossStats.Instance.SetName(data.enemyName);
        }
        if (data.isDash)
        {
            DashCooldown();
        }
        if (data.isThrowWeapon)
        {
            ThrowCooldown();
        }
        if (data.isSpawnObjects)
        {
            SpawnCooldown();
        }
    }

    private void OnEnable()
    {
        CancelInvoke();
        if(isBoss && isFirstStart)
        {
            BossStats.Instance.SetHealth(currentHealth, maxHelth);
            BossStats.Instance.SetName(data.enemyName);
        }
        if (data.isDash)
        {
            DashCooldown();
        }
        if (data.isThrowWeapon)
        {
            ThrowCooldown();
        }
        if (data.isSpawnObjects)
        {
            SpawnCooldown();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        StateMachine.CurrentEnemyState.LogicUpdate();
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
        if (this.enabled == false || isDie) return;
        isHurt = false;
        currentAODToHurt--;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHelth);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Damage Hit"), transform.position);
        if (isBoss)
        {
            BossStats.Instance.SetDamage(damage);
            BossStats.Instance.UpdateHealthBar(currentHealth);
        }
        else
        {
            HealthBar.SetDamageText(damage);
            HealthBar.UpdateHealth(currentHealth, maxHelth);
            if (currentHealth <= 0)
            {
                HealthBar.ResetHealthBar();
            }
        }
        if (currentHealth > 0 && currentAODToHurt <= 0)
        {
            currentAODToHurt = data.amountOfDamageToHurt;
            isHurt = true;
        }
        else if(currentHealth <= 0)
        {
            isDie = true;
        }
    }

    public virtual void Die()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        ObjectPool.Instance.AddInPool(data.enemyName, gameObject.transform.parent.gameObject);
    }
    

    #endregion

    #region Check Function

    public float GetDistance(Vector3 orginal, Vector3 target)
    {
        return Vector3.Distance(new Vector3(orginal.x, orginal.y, orginal.z), new Vector3(target.x, orginal.y, target.z));
    }

    public bool CheckPlayerDetected()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.radiusCheckToChase;
    }

    public bool CheckPlayerToMeleeAttack()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= radiusCheckToAttack;
    }

    public bool CheckPlayerToShieldAttack()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.radiusCheckToShieldAttack;
    }

    public bool CheckPlayerInRangeToThrow()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.radiusCheckThrow;
    }

    public bool CheckPlayerInRangeToSpawn()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= data.raidusCheckSpawn;
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

    public void KnockBack(float knockBackSpeed)
    {
        transform.position += damageDir * knockBackSpeed;
    }

    #endregion

    #region Attack Function


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

    public void DashCooldown()
    {
        canDash = false;
        currentDashCooldown = Random.Range(data.dashCooldownRan.x, data.dashCooldownRan.y);
        Invoke(nameof(ResetDash), currentDashCooldown);
    }

    private void ResetDash()
    {
        canDash = true;
    }

    public bool CanDash()
    {
        return canDash;
    }


    #endregion

    #region Throw Weapon

    public void ThrowCooldown()
    {
        canThrow = false;
        currentThrowWeaponCooldown = Random.Range(data.throwWeaponCooldownRan.x, data.throwWeaponCooldownRan.y);
        Invoke(nameof(ResetThrow), currentThrowWeaponCooldown);
    }


    public void ResetThrow()
    {
        canThrow = true;
    }

    public bool CanThrow()
    {
        return canThrow;
    }

    #endregion

    #region Spawn Objects
    public void SpawnCooldown()
    {
        canSpawn = false;
        currentSpawnObjectsCooldown = Random.Range(data.spawnObjectCooldown.x, data.spawnObjectCooldown.y);
        Invoke(nameof(ResetSpawn), currentSpawnObjectsCooldown);
    }

    private void ResetSpawn()
    {
        canSpawn = true;
    }

    public bool CanSpawn()
    {
        return canSpawn;
    }

    #endregion

    #region Object Pool
    public virtual void OnObjectSpawn()
    {
        transform.localPosition = Vector3.zero;
        maxHelth = data.MaxHealthData;
        currentHealth = maxHelth;
        maxCombo = data.maxCombo;
        currentAODToHurt = data.amountOfDamageToHurt;
        isFirstDamage = false;
        isBoss = data.isBoss;
        isHurt = false;
        isDie = false;
        if (!HealthBar)
        {
            if (!isBoss)
            {
                HealthBar = GetComponentInChildren<EnemyHealthBar>();
                HealthBar.ResetHealthBar();
            }
            else
            {
                BossStats.Instance.SetHealth(currentHealth, maxHelth);
                BossStats.Instance.SetName(this.name);
            }
        }

    }
    #endregion

    #region Reset

    public virtual void ResetEnemy()
    {
        isDie = false;
        maxHelth = data.MaxHealthData;
        currentHealth = maxHelth;
        if(HealthBar!= null)
        {
            if (!isBoss)
            {
                HealthBar.UpdateHealth(currentHealth, maxHelth);
                HealthBar.ResetHealthBar();
            }
            if (isBoss)
            {
                BossStats.Instance.UpdateHealthBar(0);
                CancelInvoke();
            }
        }
        currentAODToHurt = 0;
        currentCombo = 0;
        transform.localPosition = startPos;
        transform.localEulerAngles = startAngle;
        GetComponent<Collider>().enabled = true;
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

        if (data != null && isDrawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, data.radiusCheckToChase);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.radiusCheckToAttack);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, data.radiusCheckToShieldAttack);
            if (data.isThrowWeapon)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, data.radiusCheckThrow);
            }
            if (data.isSpawnObjects)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, data.raidusCheckSpawn);
            }
        }
    }



    #endregion
}
