using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionType : MonoBehaviour
{
    public enum Type
    {
        Default,
        Wood,
        Meat
    }

    public Type type;
}
