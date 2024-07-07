using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    int CombonAtttack {  get; set; }

    int CurrentComboAttack {  get; set; }

    float ResetComboTime {  get; set; }

    bool isAttack {  get; set; }

    void SetComboAttack();

    void ResetComboAttack();

    void IsAttack(bool isAttack);
}
