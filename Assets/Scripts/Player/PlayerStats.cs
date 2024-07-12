using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public float maxHealth {  get; private set; }

    public float currentHealth {  get; private set; }


    public float maxEnergy {  get; private set; }

    public float currentEnergy {  get; private set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
