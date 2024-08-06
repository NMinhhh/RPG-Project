using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [Header("Shoot Pos")]
    [SerializeField] private Transform[] shootPos;
    [Header("Delay")]
    [SerializeField] protected float startTimeDelay;
    [SerializeField] private float delayTime;
    [Header("Target Direction")]
    [SerializeField] protected Transform target;

    void Start()
    {
        Invoke(nameof(Shoot), startTimeDelay);
    }

    void Shoot()
    {
        foreach (Transform pos in shootPos)
        {
            Vector3 direction = (new Vector3(target.position.x,pos.position.y,pos.position.z) - pos.position).normalized;
            ObjectPool.Instance.SpawnFromPool("Arrow(Trap)", pos.position, Quaternion.LookRotation(direction, Vector3.up));
        }
        Invoke(nameof(Shoot), delayTime);
    }
}
