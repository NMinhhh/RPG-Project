using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaImage;
    [SerializeField] private Image easeStaminaImage;
    [SerializeField] private float lerpSpeed = .05f;
    private float target;

    void Start()
    {
        target = 1;
        staminaImage.fillAmount = 1;
        easeStaminaImage.fillAmount = 1;
    }

    private void LateUpdate()
    {
        UpdateStaminaBar();
    }

    public void UpdateStaminaBar()
    {
        target = PlayerStats.Instance.currentStamina / PlayerStats.Instance.maxStamina;
        if (staminaImage.fillAmount != target)
        {
            staminaImage.fillAmount = target;
        }
        if(easeStaminaImage.fillAmount != target)
        {
            easeStaminaImage.fillAmount = Mathf.Lerp(easeStaminaImage.fillAmount, target, lerpSpeed);
        }

    }

    
}
