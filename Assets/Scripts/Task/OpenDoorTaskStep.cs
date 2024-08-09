using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Door))]

public class OpenDoorTaskStep : TaskStep
{
    [SerializeField] private Door doorAnim;
    [SerializeField] protected bool isOpen;
    private void OnValidate()
    {
        #if UNITY_EDITOR
        doorAnim = GetComponent<Door>();
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    private void Start()
    {
        if (isOpen)
            doorAnim.OpenDoor();
        else
            doorAnim.CloseDoor();
        FinishTaskStep();
    }

   
}
