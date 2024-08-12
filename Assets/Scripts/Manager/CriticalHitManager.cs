using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHitManager : Singleton<CriticalHitManager>
{
    [SerializeField] private float criticalMutiplier;

    public float GetFinalDamage(float baseDamage, float criticalRate)
    {
        bool isCritical = Random.Range(0,100) <= criticalRate;
        if (isCritical)
        {
            return Mathf.FloorToInt(baseDamage * criticalMutiplier);
        }
        else
        {
            return Mathf.FloorToInt(baseDamage);
        }
    }
}
