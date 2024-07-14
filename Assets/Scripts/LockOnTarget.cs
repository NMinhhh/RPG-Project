using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private GameObject closesTarget;

    private Player player;

    float minAngle = 180;

    private bool lockOn;

    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        lockOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.lockOn && !lockOn)
        {
            lockOn = true;
            closesTarget = GetTarget();
            if(closesTarget == null)
                lockOn = false;
            player.lockOn = lockOn;
        }else if(InputManager.Instance.lockOn && lockOn)
        {
            if(closesTarget != null) 
                closesTarget = null;
            lockOn = false;
            player.lockOn = lockOn;
        }
        if (lockOn && player.StateMachine.currentState != player.MoveState)
        {
            if(closesTarget == null || closesTarget.GetComponent<Enemy>().isDie)
            {
                player.lockOn = false;
                lockOn = false;
                closesTarget = null;

            }if(closesTarget != null)
            {
                player.transform.LookAt(new Vector3(closesTarget.transform.position.x, player.transform.position.y, closesTarget.transform.position.z));
                direction = (closesTarget.transform.position - player.transform.position).normalized;
            }
          
        }
    }

    public GameObject GetTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, whatIsEnemy);
        if (cols.Length <= 0 || closesTarget != null) return null;
        GameObject target = null;
        foreach(var col in cols)
        {
            float agle = Vector3.Angle(transform.position, col.transform.position);
            if (agle < minAngle)
            {
                minAngle = agle;
                target = col.gameObject;
            }
        }
        return target;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
