using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : EnemyHealthBar
{
    [SerializeField] private Text nameText;

    protected override void Start()
    {
    }

    protected override void Update()
    {
        AppearDamageText();
    }

    public void SetName(string name)
    {
        target = 1;
        nameText.text = name;
        healthImage.fillAmount = 1;
        easeHealthImage.fillAmount = 1;
        damageText.gameObject.SetActive(false);
        healthBar.SetActive(true);
    }
}
