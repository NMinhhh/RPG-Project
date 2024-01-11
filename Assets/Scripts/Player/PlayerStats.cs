using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [Header("Health")]
    private float health;
    [SerializeField] private float maxHealth = 100f;
    [Space]
    [Header("Hunger")]
    private float hunger;
    [SerializeField] private float maxHunger = 100f;
    [Space]
    [Header("Thirst")]
    private float thirst;
    [SerializeField] private float maxThirst = 100f;

    [Header("Stats Deleption")]
    [SerializeField] private float hungerDeleption = 0.5f;
    [SerializeField] private float thirstDeleption = 0.75f;

    [Header("Stats Damages")]
    [SerializeField] private float hungerDamage = 1.5f;
    [SerializeField] private float thirstDamage = 2.2f;

    [Header("UI")]
    [SerializeField] private StatsBar healthBar;
    [SerializeField] private StatsBar hungerBar;
    [SerializeField] private StatsBar thirstBar;

    private void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    private void Update()
    {
        UpdateStats();
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.numberText.text = health.ToString("f0");
        healthBar.bar.fillAmount = health / maxHealth;

        hungerBar.numberText.text = hunger.ToString("f0");
        hungerBar.bar.fillAmount = hunger / maxHunger;

        thirstBar.numberText.text = thirst.ToString("f0");
        thirstBar.bar.fillAmount = thirst / maxThirst;
    }

    private void UpdateStats()
    {
        if(health <= 0)
            health = 0;
        if(health >= maxHealth)
            health = maxHealth;

        if (hunger <= 0)
            hunger = 0;
        if(hunger >= maxHunger)
            hunger = maxHunger;

        if(thirst <= 0) 
            thirst = 0;
        if (thirst >= maxThirst)
            thirst = maxThirst;

        //Damages
        if (hunger <= 0)
            health -= hungerDamage * Time.deltaTime;

        if(thirst <= 0)
            health -= thirstDamage * Time.deltaTime;

        //Deleption
        if(hunger > 0) 
            hunger -= hungerDeleption * Time.deltaTime;

        if(thirst > 0)
            thirst -= thirstDeleption * Time.deltaTime;
    }
}
