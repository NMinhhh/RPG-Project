using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Image easeHealthImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private float decreaseTime;
    [SerializeField] private float delay = 0.05f;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
    }

    private void Update()
    {
        transform.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float target = currentHealth / maxHealth;
        StartCoroutine(UpdateBar(target));
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
