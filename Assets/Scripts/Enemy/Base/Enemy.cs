using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagaeble
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

    #endregion

    #region Variable

    public Transform PlayerPos { get; private set; }

    //Health
    protected float maxHelth;

    protected float currentHealth;

    protected bool isHurt;
    protected bool isDie;

    #endregion

    #region Unity Function

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        Anim = alive.GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        WeaponsController = GetComponent<EnemyWeaponController>();
        maxHelth = data.MaxHealthData;
        currentHealth = maxHelth;
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
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHelth);
        if (currentHealth > 0)
        {
            isHurt = true;
        }
        else
        {
            isDie = true;
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
        return Vector3.Distance(transform.position, PlayerPos.position) <= data.radiusCheckToChase;
    }

    public bool CheckPlayerInRange()
    {
        return Vector3.Distance(transform.position, PlayerPos.position) <= data.radiusCheckToAttack;
    }

    #endregion


    #region Others Function
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
