using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagaeble
{
    #region State Machine

    public EnemyStateMachine StateMachine { get; private set; }

    #endregion


    #region Component Variable

    public Animator Anim { get; private set; }

    protected GameObject alive;

    public NavMeshAgent Agent {  get; private set; }

    #endregion

    #region Health Variable

    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    public Transform PlayerPos {  get; private set; }

    protected bool isHurt;
    protected bool isDie;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        Anim = alive.GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        CurrentHealth = MaxHealth;
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
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        Debug.Log(CurrentHealth);
        if (CurrentHealth > 0)
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
        Debug.Log("Die");
        Destroy(gameObject);
    }

    #endregion

    #region Check Function

    public float CheckDistance(Vector3 orginal, Vector3 target)
    {
        return Vector3.Distance(orginal, target);
    }

    #endregion
    
    public virtual void TriggerAnimation() => StateMachine.CurrentEnemyState.TriggerAnimation();

    public virtual void FinishAnimtion()
    {
       
        StateMachine.CurrentEnemyState.FinishAnimation();
    }

    protected virtual void OnDrawGizmos()
    {
        
    }
}
