using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public float maxHealth {  get; private set; }

    public float currentHealth {  get; private set; }


    public float maxStamina {  get; private set; }

    public float currentStamina {  get; private set; }

    public bool isStaminaRecovery {  get; private set; }

    [SerializeField] private float recoverySpeed = 1f;
    [SerializeField] private float recoveryDelayTime = 2f;
    private float currentRecoveryDelayTime;


    private void Update()
    {
        StaminaRecovery();
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }

    public void SetStamina(float currentStanima, float maxStamina)
    {
        this.currentStamina = currentStanima;
        this.maxStamina = maxStamina;
    }

    public void UseStamina(float staminaAmount)
    {
        currentStamina -= staminaAmount;
        isStaminaRecovery = true;
        currentRecoveryDelayTime = recoveryDelayTime;
    }

    public bool EnoughStamina(float staminaAmount)
    {
        return currentStamina >= staminaAmount;
    }

    public void StaminaRecovery()
    {
        if (isStaminaRecovery && currentStamina < maxStamina)
        {
            currentRecoveryDelayTime -= Time.deltaTime;
            if(currentRecoveryDelayTime <= 0)
            {
                currentStamina += recoverySpeed * Time.deltaTime;
               
            }
        }
        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            isStaminaRecovery = false;
        }
    }
}
