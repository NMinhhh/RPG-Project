using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform bowString;
    [SerializeField] private Transform bowStringIdle;
    [SerializeField] private Transform bowStringPull;

    public void BowStringPull()
    {
        bowString.position = bowStringPull.position;
        bowString.SetParent(bowStringPull);
    }

    public void BowStringNotPull()
    {
        bowString.position = bowStringIdle.position;
        bowString.SetParent(bowStringIdle);
    }
}
