using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IKnockBackable
{

    #region Data

    [SerializeField] private PlayerData data;

    #endregion

    #region State Machine Variable

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerInAirState InAirState { get; private set; }

    public PlayerLandingState LandingState { get; private set; }

    public PlayerAttackState AttackState { get; private set; }

    public PlayerHurtState HurtState { get; private set; }

    public PlayerStrongAttack StrongAttack { get; private set; }

    public PlayerBlockState BlockState { get; private set; }

    public PlayerAimState AimState { get; private set; }

    public PlayerShootState ShootState { get; private set; }

    public PlayerDeathState DeathState {  get; private set; } 

    #endregion

    #region Component Variable

    public GameObject Alive { get; private set; }

    public Animator Anim { get; private set; }

    public ThirdPersonCamera thirdPersonCamera { get; private set; }

    public WeaponsController weaponsController { get; private set; }

    public CharacterController character { get; private set; }

    public ThirdPersonAim thirdPersonAim {  get; private set; }


    #endregion

    #region Check Transform

    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform shootPoint;

    #endregion

    #region Particle Effect

    [SerializeField] private ParticleSystem healingParticle;

    #endregion

    #region Others Variable

    //Health
    private float currentHealth;
    private Vector3 damageDirection;
    private bool isHurt;
    private bool isDie;

    //Stamina
    private float currentStamina;

    //Velocity
    [HideInInspector]
    public Vector2 velocity;

    //Attack
    private int maxComboAttack;
    private int currentComboAttack;

    //Block/Parry
    private bool isParry;

    public bool lockOn;

    public Vector3 lockOnDirection;

    #endregion


    //---------------------------Function-----------------------------------------

    #region Unity Function

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, data, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, data, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, data, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, data, "InAir");
        LandingState = new PlayerLandingState(this, StateMachine, data,  "Landing");
        AttackState = new PlayerAttackState(this, StateMachine, data, "Attack");
        StrongAttack = new PlayerStrongAttack(this, StateMachine, data, "StrongAttack");
        BlockState = new PlayerBlockState(this, StateMachine, data, "Block");
        HurtState = new PlayerHurtState(this, StateMachine, data, "Hurt");
        AimState = new PlayerAimState(this, StateMachine, data, "Aim");
        ShootState = new PlayerShootState(this, StateMachine, data, "Shoot", shootPoint);
        DeathState = new PlayerDeathState(this, StateMachine, data, "Death");
    }

    // Start is called before the first frame update
    void Start()
    {
        Alive = transform.Find("Alive").gameObject;
        Anim = Alive.GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        weaponsController = GetComponent<WeaponsController>();
        thirdPersonAim = GetComponent<ThirdPersonAim>();
        currentHealth = data.maxHealth;
        currentStamina = data.maxStamina;
        PlayerStats.Instance.SetStamina(currentStamina,currentStamina);
        maxComboAttack = data.combo;
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        EquipSystem.Instance.usePotion += Healing;
        StateMachine.Intialize(IdleState);
        isParry = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        StateMachine.currentState.LogicUpdate();
        WorldGravity();
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicUpdate();
    }

    #endregion


    #region Health / Die Function

    public void Healing(float health)
    {
        healingParticle.Play();
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Healing"), transform.position);
        currentHealth = Mathf.Clamp(currentHealth + health, 0, data.maxHealth);
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
    }

    public void Damage(float damage)
    {
        if (isParry || isDie) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, data.maxHealth);
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        if (currentHealth > 0)
        {
            isHurt = true;
        }
        else
        {
            isDie = true;
        }
        if (isDie)
        {
            StateMachine.ChangeState(DeathState);
        }
        if(isHurt && StateMachine.currentState != HurtState && StateMachine.currentState != StrongAttack && StateMachine.currentState != ShootState)
        {
            StateMachine.ChangeState(HurtState);

        }
        
    }

    public void Die()
    {
        SceneManager.Instance.LoadSceneRespawn(Respawn);
    }

    public void Respawn()
    {
        GameManager.Instance.RespawnPlayer(this);
        currentHealth = data.maxHealth;
        currentComboAttack = 0;
        isDie = false;
        isHurt = false;
        PlayerStats.Instance.SetStamina(currentStamina, currentStamina);
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        if (StateMachine != null)
            StateMachine.ChangeState(IdleState);
    }

    #endregion


    #region Move Function

    public void Move(Vector3 direction, float speed)
    {
        Vector3 motion;
        if (!lockOn || StateMachine.currentState == MoveState 
            || StateMachine.currentState == JumpState 
            || StateMachine.currentState == InAirState 
            || StateMachine.currentState == LandingState 
            || StateMachine.currentState == AimState)
        {
            motion = thirdPersonCamera.MoveRotation(direction) * (InputManager.Instance.xInput == 1 && InputManager.Instance.zInput == 1 ? .7f : 1);
        }
        else
        {
            motion = (direction) * (InputManager.Instance.xInput == 1 && InputManager.Instance.zInput == 1 ? .7f : 1);
        }
        character.Move(motion * speed * Time.deltaTime);
    }

    #endregion


    #region Check Fuction

    public bool CheckGround()
    {
        return Physics.CheckSphere(groundCheck.position, data.radius, data.whatIsGround);
    }


    #endregion


    #region Jump Fuction

    void WorldGravity()
    {
        if (CheckGround() && velocity.y <= 0)
        {
            velocity.y = -2;
        }
        velocity.y += data.gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);


    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(data.jumpHeight * -2 * data.gravity);
    }

    #endregion


    #region Attack Function

    public void ChangeWeapon()
    {
        ResetComboAttack();
        if(StateMachine != null && Anim != null)
        {
            StateMachine.ChangeState(IdleState);
        }
    }

    public int GetComboAttack()
    {
        currentComboAttack++;
        if(currentComboAttack > maxComboAttack)
        {
            currentComboAttack = 1;
        }
        return currentComboAttack;
    }

    public void ResetComboAttack()
    {
        currentComboAttack = 0;
    }

    #endregion

    #region Block/Parry Function

    public void IsParry(bool state)
    {
        isParry = state;
    }

    #endregion


    #region Lock On Target

    public void SetLockOnDirectio(Vector3 direction)
    {
        lockOnDirection = direction;
    }

    #endregion


    #region Others Function

    public void TrtiggerAnimation() => StateMachine.currentState.TriggerAnimation();

    public void FinishAnimation() => StateMachine.currentState.FinishAnimation();


    private void OnDrawGizmos()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, data.radius);
    }

    public void DamageDiretion(Vector3 direction)
    {
        damageDirection = direction;
    }

    public void KnockBack(float knockBackSpeed)
    {
        character.Move(damageDirection * knockBackSpeed * Time.deltaTime);
    }

    #endregion

}
