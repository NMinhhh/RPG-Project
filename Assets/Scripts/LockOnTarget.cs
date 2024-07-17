using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private GameObject closesTarget;
    private float minDistance = 1000;
    private float currentMinDistance;


    private Player player;


    private bool lockOn;

    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        lockOn = false;
        currentMinDistance = minDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckTarget())
        {
            GetTargetInRange();
        }
        else
        {
            closesTarget = null;
            player.lockOn = false;
            currentMinDistance = minDistance;
        }
    }

    public bool CheckTarget()
    {
        return Physics.CheckSphere(transform.position, radius, whatIsEnemy);
    }

    public void GetTargetInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, whatIsEnemy);
        currentMinDistance = minDistance;
        if (cols.Length > 0)
        {
            foreach (var col in cols)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < currentMinDistance)
                {
                    currentMinDistance = distance;
                    closesTarget = col.gameObject;
                }
            }
        }

        if (closesTarget != null) 
        {
            if (closesTarget.GetComponent<Enemy>().isDie)
            {
                closesTarget = null;
                player.lockOn = false;
                currentMinDistance = minDistance;
            }
            else
            {
                direction = (closesTarget.transform.position - player.transform.position).normalized;
                player.SetLockOnDirectio(direction);
                player.lockOn = true;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
