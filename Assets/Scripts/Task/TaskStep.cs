using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    protected bool isFinished = false;

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            gameObject.SetActive(false);
            TaskManager.Instance.FinishTaskStep();
            isFinished = false;
        }
    }

    public abstract void StartTask();

}
