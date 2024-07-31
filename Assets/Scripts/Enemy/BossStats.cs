using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : Singleton<BossStats>
{
    [SerializeField] private BossHealthBar healthBar;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Start()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        this.CurrentHealth = currentHealth;
        this.MaxHealth = maxHealth;
        CanvasManager.Instance.OpenUI(UIObject.UIName.BossHealthBar);
        
    }

    public void SetName(string name)
    {
        healthBar.SetName(name);
    }

    public void SetDamage(float damage)
    {
        healthBar.SetDamageText(damage);
    }

    public void UpdateHealthBar(float currentHealth)
    {
        healthBar.UpdateHealth(currentHealth, this.MaxHealth);
        if (currentHealth <= 0)
            healthBar.gameObject.SetActive(false);
    }
}
