using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            gameObject.SetActive(false);
            TaskManager.Instance.FinishTaskStep();
        }
    }

}
