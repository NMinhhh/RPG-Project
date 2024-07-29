using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image staminaImage;
    [SerializeField] private float decreaseTime;
    private float target;

    void Start()
    {
        target = 1;
        staminaImage.fillAmount = 1;
    }

    private void OnEnable()
    {
        PlayerStats.UpdateStamina += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        PlayerStats.UpdateStamina -= UpdateStaminaBar;
    }

    public void UpdateStaminaBar()
    {
        target = PlayerStats.Instance.currentStamina / PlayerStats.Instance.maxStamina;
        StartCoroutine(UpdateBar(target));

    }

    IEnumerator UpdateBar(float target)
    {
        float timer = 0;
        while (timer < decreaseTime)
        {
            timer += Time.deltaTime;
            staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, target, timer / decreaseTime);
            yield return null;
        }
    }
}
