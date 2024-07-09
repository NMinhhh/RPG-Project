using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagaeble, IPlayerMoveable, ICheckable, IJumpable, IAttackable
{
    [field: SerializeField] public CharacterController character { get; set; }

    [field: SerializeField] public float MaxHealth { get; set; }

    public float CurrentHealth { get; set; }

    public bool isGround { get; set; }

    [field: SerializeField] public float JumpHeight { get; set; }

    [field: SerializeField] public int CombonAtttack { get; set; }

    public int CurrentComboAttack { get; set; }

    [field: SerializeField] public float ResetComboTime { get; set; }

    public bool isAttack { get; set; }

    #region State Machine Variable

    public PlayerStateMachine StateMachine { get; private set; }


    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }
    public PlayerJumpState PlayerJumpState { get; private set; }

    public PlayerInAirState PlayerInAirState { get; private set; }

    public PlayerLandingState PlayerLandingState { get; private set; }

    public PlayerAttackState PlayerAttackState { get; private set; }

    #endregion


    #region Component Variable

    public GameObject Alive { get; private set; }

    public Animator Anim { get; private set; }

    public ThirdPersonCamera thirdPersonCamera { get; private set; }

    public WeaponsController WeaponsController { get; private set; }

    #endregion



    #region Idle Variable

    [SerializeField] private float _speed;

    #endregion


    #region Jump Variable

    [SerializeField] private float _gravity = -9.81f * 2;

    [HideInInspector]
    public Vector2 velocity;

    #endregion


    #region Attack

    public enum CombatType
    {
        Normal,
        Sword,
        SwordAndShield,
        HeavySword
    }

    public CombatType currentCombatType { get; private set; }

    #endregion

    //---------------------------Function-----------------------------------------

    #region Unity Function

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(this, StateMachine, "Idle");
        PlayerMoveState = new PlayerMoveState(this, StateMachine, "Move");
        PlayerJumpState = new PlayerJumpState(this, StateMachine, "Jump");
        PlayerInAirState = new PlayerInAirState(this, StateMachine, "InAir");
        PlayerLandingState = new PlayerLandingState(this, StateMachine, "Landing");
        PlayerAttackState = new PlayerAttackState(this, StateMachine, "Attack");
    }

    // Start is called before the first frame update
    void Start()
    {
        Alive = transform.Find("Alive").gameObject;
        Anim = Alive.GetComponent<Animator>();
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        WeaponsController = GetComponent<WeaponsController>();
        CurrentComboAttack = CombonAtttack;

        StateMachine.Intialize(PlayerIdleState);
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

    public void Damage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        if (CurrentHealth < 0)
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
        Vector3 motion = thirdPersonCamera.MoveRotation(direction) * (InputManager.Instance.XInput == 1 && InputManager.Instance.XInput == 1 ? .7f : 1);
        character.Move(motion * _speed * Time.deltaTime);
    }

    #endregion


    #region Check Fuction

    public void CheckGround(bool isGround)
    {
        this.isGround = isGround;
    }


    #endregion


    #region Jump Fuction

    void WorldGravity()
    {
        if (isGround && velocity.y <= 0)
        {
            velocity.y = -2;
        }
        velocity.y += _gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);


    }

    public void SetJumpHeigth(float jumpheight)
    {
        velocity.y = Mathf.Sqrt(jumpheight * -2 * _gravity);
    }


    #endregion


    #region Attack Function

    public void SetAmoutOfCombo(int combo)
    {
        CombonAtttack = combo;
        CurrentComboAttack = 0;
        if(StateMachine != null || StateMachine.currentState != PlayerIdleState)
        {
            StateMachine.ChangeState(PlayerIdleState);
        }
    }

    public void IsAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }

    public void SetComboAttack()
    {
        CurrentComboAttack++;
        if(CurrentComboAttack > CombonAtttack)
        {
            CurrentComboAttack = 1;
        }
    }

    public void ResetComboAttack()
    {
        CurrentComboAttack = 0;
    }

    #endregion


    #region Others Function

    public void TrtiggerAnimation() => StateMachine.currentState.TriggerAnimation();

    public void FinishAnimation() => StateMachine.currentState.FinishAnimation();


    #endregion

}
