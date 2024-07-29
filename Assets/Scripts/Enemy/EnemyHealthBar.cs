using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image easeHealthImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private float decreaseTime;
    [SerializeField] private float delay = 0.05f;

    [Header("Damage Text")]
    [SerializeField] private Text damageText;
    [SerializeField] private float disappearTime;
    private float currentDisappearTime;
    private float amountOfDamage;
    private Camera cam;


    void Start()
    {
        cam = Camera.main;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
    }

    protected virtual void Update()
    {
        transform.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
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

    public void SetDamage(float damage)
    {
        amountOfDamage += damage;
        currentDisappearTime = disappearTime;
        damageText.text = amountOfDamage.ToString();
        damageText.gameObject.SetActive(true);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float target = currentHealth / maxHealth;
        StartCoroutine(UpdateBar(target));
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateBar(float target)
    {
        healthImage.fillAmount = target;
        easeHealthImage.color = damageColor;
        yield return new WaitForSeconds(delay);
        easeHealthImage.color = normalColor;
        float timer = 0;
        while (timer < decreaseTime)
        {
            timer += Time.deltaTime;
            easeHealthImage.fillAmount = Mathf.Lerp(easeHealthImage.fillAmount, target, timer / decreaseTime);
            yield return null;
        }
    }
}
