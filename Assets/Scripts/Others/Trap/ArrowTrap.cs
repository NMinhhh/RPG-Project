using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [Header("Shoot Pos")]
    [SerializeField] private Transform[] shootPos;
    [SerializeField] private Transform arrowHolder;
    [Header("Delay")]
    [SerializeField] protected float startTimeDelay;
    [SerializeField] private float delayTime;

    void Start()
    {
        Invoke(nameof(Shoot), startTimeDelay);
    }

    void Shoot()
    {
        foreach (Transform pos in shootPos)
        {
            ObjectPool.Instance.SpawnFromPool("Arrow(Trap)", pos.position, transform.rotation).transform.SetParent(arrowHolder);

        }
        Invoke(nameof(Shoot), delayTime);
    }
}
