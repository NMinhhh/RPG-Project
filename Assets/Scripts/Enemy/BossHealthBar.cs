using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : EnemyHealthBar
{
    [SerializeField] private Text nameText;

    protected override void Update()
    {
        AppearDamageText();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
