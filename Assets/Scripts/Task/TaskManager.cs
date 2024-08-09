using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField] private List<MainTask> mainTaskList;

    [SerializeField] public int currentTask {  get; private set; }

    private bool isFinishAllTask;

    private void Start()
    {
        //GetMainTask().InitializeTaskStep(0);
    }

    public void Initialize(int currentId)
    {
        currentTask = currentId;
        SaveManager.Instance.SaveTask(currentTask);
    }

    public MainTask GetMainTask()
    {
        return mainTaskList[currentTask];
    }

    public void ChangeMainTask()
    {
        currentTask++;
        if (currentTask == mainTaskList.Count)
        {
            isFinishAllTask = true;
        }
        else
        {
            SaveManager.Instance.SaveTask(currentTask);
        }
    }

    public void FinishTaskStep()
    {
        GetMainTask().GetTaskStep().FinishTask();
        GetMainTask().ChangeTaskStep();
        if (GetMainTask().isFinished)
        {
            ChangeMainTask();
            if (isFinishAllTask)
                return;
            GetMainTask().InitializeTaskStep(0);
        }
    }

    public void TriggerTaskStep()
    {
        GetMainTask().GetTaskStep().TriggerTask();
    }
}

[System.Serializable]
public class MainTask
{
    public string mainTextName;

    public Task[] taskSteps;

    public int currentTaskStep = 0;

    public bool isFinished;

    public void InitializeTaskStep(int id)
    {
        currentTaskStep = id;
        GetTaskStep().TaskActive();
    }

    public Task GetTaskStep()
    {
        return taskSteps[currentTaskStep];
    }

    public void ChangeTaskStep()
    {
        currentTaskStep++;
        if (currentTaskStep == taskSteps.Length)
        {
            isFinished = true;
            return;
        }

        GetTaskStep().TaskActive();
    }
}

[System.Serializable]
public class Task
{
    public GameObject taskObj;
    public GameObject triggerObj;
    public bool isNeedTrigger;
    public bool isFinished;

    public void FinishTask()
    {
        isFinished = true;
    }
   
    public void TriggerTask()
    {
        taskObj.SetActive(true);
    }

    public void TaskActive()
    {
        if (isNeedTrigger)
        {
            triggerObj.SetActive(true);
            return;
        }
        taskObj.SetActive(true);
    }
}