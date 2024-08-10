using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Door))]

public class OpenDoorTaskStep : TaskStep, IResetable
{
    [SerializeField] private Door doorAnim;
    [SerializeField] protected bool isOpen;

    private bool isFirstStart;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        doorAnim = GetComponent<Door>();
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public override void StartTask()
    {
        if (isOpen)
            doorAnim.OpenDoor();
        else
            doorAnim.CloseDoor();
        FinishTaskStep();
    }

    public void ResetBaseState()
    {
        doorAnim.ResetDoor();
    }
}
