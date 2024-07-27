using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaskable
{
    public void StartTask();

    public void PerformTask();

    public void EndTask();
}
