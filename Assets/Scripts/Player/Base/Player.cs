using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagaeble, IPlayerMoveable, ICheckable, IJumpable
{
    [field: SerializeField] public CharacterController character { get; set; }

    [field: SerializeField] public float MaxHealth { get; set; }

    public float CurrentHealth { get; set; }

    public bool isGround { get; set; }

    [field: SerializeField] public float JumpHeight { get; set; }

    #region State Machine Variable

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }
    public PlayerJumpState PlayerJumpState { get; private set; }

    public PlayerInAirState PlayerInAirState { get; private set; }

    public PlayerLandingState PlayerLandingState { get; private set; }

    #endregion


    #region Component Variable

    public GameObject Alive {  get; private set; }

    public Animator Anim {  get; private set; }

    public ThirdPersonCamera thirdPersonCamera { get; private set; }

    #endregion


    #region Idle Variable

    [SerializeField] private float _speed;

    #endregion


    #region Jump Variable

    public float JumpPrepAnimTime { get; private set; }
    public float JumpLandingAnimTime { get; private set; }

    [SerializeField] private float gravity = -9.81f * 2;

    [HideInInspector]
    public Vector2 velocity;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        Alive = transform.Find("Alive").gameObject;
        Anim = Alive.GetComponent<Animator>();
        GetAnimationTime();
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        StateMachine.Intialize(PlayerIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        WorldGravity();
        StateMachine.currentState.PhysicUpdate();
    }

    #endregion


    #region Health / Die Function

    public void Damage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        if(CurrentHealth < 0)
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
        character.Move(thirdPersonCamera.MoveRotation(direction) * _speed * Time.deltaTime);
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
        if(isGround && velocity.y <= 0)
        {
            velocity.y = -2;
        }
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);


    }

    public void SetJumpHeigth(float jumpheight)
    {
        velocity.y = Mathf.Sqrt(jumpheight * -2 * gravity);
    }


    #endregion

    
    #region Others Function

    public void GetAnimationTime()
    {
        AnimationClip[] clips = Anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "Jump_Prep":
                    JumpPrepAnimTime = clip.length;
                    break;
                case "Jump_Landing":
                    JumpLandingAnimTime = clip.length;
                    break;

            }
        }
    }
    public void TrtiggerAnimation() => StateMachine.currentState.TriggerAnimation();

    public void FinishAnimatio() => StateMachine.currentState.FinishAnimation();


    #endregion

}
