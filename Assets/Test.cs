using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2f, 4f, 6f);
    public Vector3 boxCenter = Vector3.zero; 

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 

        Vector3 halfExtents = boxSize / 2;

        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize);

        Debug.Log($"Half Extents: {halfExtents}");
    }

}
