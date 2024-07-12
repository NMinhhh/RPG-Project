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
    [SerializeField] private float delay = 0.05f;

    void Start()
    {
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
        Player.UpdateHealthBar += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        float target = PlayerStats.Instance.currentHealth / PlayerStats.Instance.maxHealth;
        StartCoroutine(UpdateBar(target));
    }

    IEnumerator UpdateBar(float target)
    {
        easeHealthImage.color = damageColor;
        float time1 = 0;
        while(time1 < decreaseTime)
        {
            time1 += Time.deltaTime;
            healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, target, time1 / decreaseTime);
            yield return null;
        }
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
    
}
