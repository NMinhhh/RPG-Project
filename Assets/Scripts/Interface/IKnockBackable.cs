using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockBackable 
{
    void DamageDiretion(Vector3 direction);
    void KnockBack(float knockBackSpeed);
}
