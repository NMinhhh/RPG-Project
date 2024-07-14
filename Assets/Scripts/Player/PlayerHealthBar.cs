using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Image easeHealthImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private float decreaseTime;
    [SerializeField] private float delay = .1f;
    private float target;
    void Start()
    {
        target = 1;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
    }

    private void OnEnable()
    {
        Player.UpdateHealthBar += UpdateHealthBar;
    }

    private void OnDisable()
    {
        Player.UpdateHealthBar -= UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        float newTarget = PlayerStats.Instance.currentHealth / PlayerStats.Instance.maxHealth;
        if(newTarget <= target)
        {
            target = newTarget;
            StartCoroutine(UpdateBar(target));
        }
        else
        {
            target = newTarget;
            StartCoroutine(Healing(target));
        }
    }

    IEnumerator UpdateBar(float target)
    {
        healthImage.fillAmount = target;
        easeHealthImage.color = damageColor;
        yield return new WaitForSeconds(delay);
        easeHealthImage.color = normalColor;
        float timer = 0;
        while(timer < decreaseTime)
        {
            timer += Time.deltaTime;
            easeHealthImage.fillAmount = Mathf.Lerp(easeHealthImage.fillAmount, target, timer / decreaseTime);
            yield return null;
        } 
    }

    IEnumerator Healing(float target)
    {
        float time = 0;
        while (time < decreaseTime)
        {
            time += Time.deltaTime;
            healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, target, time / decreaseTime);
            easeHealthImage.fillAmount = Mathf.Lerp(easeHealthImage.fillAmount, target, time / decreaseTime);
            yield return null;
        }
    }
    
}
