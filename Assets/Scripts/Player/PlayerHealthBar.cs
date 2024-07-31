using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Image easeHealthImage;
    [SerializeField] private float lerpSpeed = .05f;
    private float target;
    void Start()
    {
        target = 1;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
    }

    private void LateUpdate()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        target = PlayerStats.Instance.currentHealth / PlayerStats.Instance.maxHealth;
        if(healthImage.fillAmount != target)
        {
            healthImage.fillAmount = target;
        }
        if(easeHealthImage.fillAmount != target)
        {
            easeHealthImage.fillAmount = Mathf.Lerp(easeHealthImage.fillAmount, target, lerpSpeed);
        }
    }
    
}
