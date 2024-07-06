using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICheckable
{
    bool isGround { get; set; }

    void CheckGround(bool isGround);
}
