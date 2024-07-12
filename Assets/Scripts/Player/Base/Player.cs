using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagaeble
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

    #endregion

    #region Component Variable

    public GameObject Alive { get; private set; }

    public Animator Anim { get; private set; }

    public ThirdPersonCamera thirdPersonCamera { get; private set; }

    public WeaponsController WeaponsController { get; private set; }

    public CharacterController character { get; private set; }

    #endregion

    #region Check Transform

    [SerializeField] private Transform groundCheck;

    #endregion

    #region Others Variable

    //Health
    private float currentHealth;

    //Velocity
    [HideInInspector]
    public Vector2 velocity;

    //Attack
    public int maxComboAttack;
    private int currentComboAttack;
    private bool isAttack;

    //Action
    public static event Action UpdateHealthBar;


    #endregion


    //---------------------------Function-----------------------------------------

    #region Unity Function

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, "InAir");
        LandingState = new PlayerLandingState(this, StateMachine, "Landing");
        AttackState = new PlayerAttackState(this, StateMachine, "Attack");
        HurtState = new PlayerHurtState(this, StateMachine, "Hurt");
    }

    // Start is called before the first frame update
    void Start()
    {
        Alive = transform.Find("Alive").gameObject;
        Anim = Alive.GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        WeaponsController = GetComponent<WeaponsController>();
        currentHealth = data.maxHealth;
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        EquipSystem.Instance.usePotion += UpdateHealth;
        StateMachine.Intialize(IdleState);
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

    public void UpdateHealth(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, data.maxHealth);
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        if (UpdateHealthBar != null)
            UpdateHealthBar();
    }

    public void Damage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, data.maxHealth);
        PlayerStats.Instance.SetHealth(currentHealth, data.maxHealth);
        if(UpdateHealthBar != null)
            UpdateHealthBar();
        if(currentHealth > 0 && StateMachine.currentState != HurtState)
        {
            StateMachine.ChangeState(HurtState);
        }
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    #endregion


    #region Move Function

    public void Move(Vector3 direction)
    {
        Vector3 motion = thirdPersonCamera.MoveRotation(direction) * (InputManager.Instance.xInput == 1 && InputManager.Instance.xInput == 1 ? .7f : 1);
        character.Move(motion * data.speed * Time.deltaTime);
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

    public void SetAmoutOfCombo(int combo)
    {
        maxComboAttack = combo;
        currentComboAttack = 0;
        if (StateMachine != null && StateMachine.currentState != IdleState && Anim != null)
        {
            StateMachine.ChangeState(IdleState);
        }
    }

    public void IsAttack(bool isAttack)
    {
        this.isAttack = isAttack;
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


    #region Others Function

    public void TrtiggerAnimation() => StateMachine.currentState.TriggerAnimation();

    public void FinishAnimation() => StateMachine.currentState.FinishAnimation();


    private void OnDrawGizmos()
    {
        if(groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, data.radius);
    }

    #endregion

}
