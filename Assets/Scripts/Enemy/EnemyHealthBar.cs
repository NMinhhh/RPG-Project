using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] protected GameObject healthBar;
    [SerializeField] protected Image healthImage;
    [SerializeField] protected Image easeHealthImage;
    [SerializeField] private float lerpSpeed;

    [Header("Damage Text")]
    [SerializeField] protected Text damageText;
    [SerializeField] private float disappearTime;
    private float currentDisappearTime;
    private float amountOfDamage;
    private Camera cam;


    protected float target = 1;

    void Start()
    {
        healthBar.SetActive(false);
        cam = Camera.main;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
    }

    protected virtual void Update()
    {
        transform.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        
    }

    private void LateUpdate()
    {
        UpdateBar();
        AppearDamageText();
    }

    protected virtual void AppearDamageText()
    {
        if(currentDisappearTime <= 0)
        {
            amountOfDamage = 0;
            damageText.gameObject.SetActive(false);
        }
        else
        {
            currentDisappearTime -= Time.deltaTime;
        }
    }

    public void SetDamageText(float damage)
    {
        amountOfDamage += damage;
        currentDisappearTime = disappearTime;
        damageText.text = amountOfDamage.ToString();
        damageText.gameObject.SetActive(true);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.SetActive(true);
        target = currentHealth / maxHealth;
    }

    public void UpdateBar()
    {
        if(healthImage.fillAmount != target)
        {
            healthImage.fillAmount = target;
        }
        if (easeHealthImage.fillAmount != target)
        {
            easeHealthImage.fillAmount = Mathf.Lerp(easeHealthImage.fillAmount, target, lerpSpeed);

        }
    }

    public void ResetHealthBar()
    {
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
        damageText.gameObject.SetActive(false);
        amountOfDamage = 0;
        healthBar.SetActive(false);
    }
}
