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

    public static event Action UpdateStamina;

    [SerializeField] private float recoveryDelayTimer = 2;
    [SerializeField] private float recoveryAmount = 30f;
    private float currentDelayTimer;

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

    public bool EnoughStamina(float staminaAmount)
    {
        return currentStamina >= staminaAmount;
    }

    public void UseStamina(float staminaAmount)
    {
        isStaminaRecovery = false;
        currentDelayTimer = recoveryDelayTimer;
        currentStamina = Mathf.Clamp(currentStamina -= staminaAmount, 0, maxStamina);
        UpdateStamina?.Invoke();
    }

    public void StaminaRecovery()
    {
        if (!isStaminaRecovery && currentStamina < maxStamina)
        {
            currentDelayTimer -= Time.deltaTime;
            if (currentDelayTimer <= 0)
            {
                isStaminaRecovery = true;
            }
        }
        if (isStaminaRecovery && currentStamina < maxStamina)
        {
            currentStamina = Mathf.Clamp(currentStamina + recoveryAmount * Time.deltaTime, 0, maxStamina);
            UpdateStamina?.Invoke();
            isStaminaRecovery = (currentStamina == maxStamina);
        }
    }
}
