using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [Header("Task Notice UI")]
    [SerializeField] public TaskNoticeUI taskNotice;

    [Header("Task List")]
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
            GameManager.Instance.ShowCredit();
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

    public TaskNoticeUI GetTaskNoticeUI() => taskNotice;
}

[System.Serializable]
public class MainTask
{
    public string mainTextName;

    public Task[] taskSteps;

    public int currentTaskStep = 0;

    public bool isFinished;

    public void ResetMainTask()
    {
        foreach(var task in taskSteps)
        {
            task.RestTask();
        }
        isFinished = false;
    }

    public void InitializeTaskStep(int id)
    {
        currentTaskStep = id;
        GetTaskStep().TaskActive();
        SoundFXManager.Instance.PlayMusic(SoundFXManager.Instance.GetMusic(mainTextName));
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
    public string taskInfo;
    public bool isNeedTrigger;
    public bool isFinished;
    public bool isNoticed;
    public void RestTask()
    {
        isFinished = false;
        taskObj.SetActive(false);
        IResetable resetable = taskObj.GetComponent<IResetable>();
        if (resetable != null)
        {
            resetable.ResetBaseState();
        }
    }

    public void FinishTask()
    {
        isFinished = true;
    }
   
    public void TriggerTask()
    {
        taskObj.GetComponent<TaskStep>().StartTask();
        TaskNotice();
    }

    public void TaskActive()
    {
        taskObj.SetActive(true);
        if (isNeedTrigger)
        {
            triggerObj.SetActive(true);
            return;
        }
        taskObj.GetComponent<TaskStep>().StartTask();
        TaskNotice();
    }

    void TaskNotice()
    {
        if (isNoticed)
        {
            CanvasManager.Instance.OpenUI(UIObject.UIName.TaskNotice);
            TaskManager.Instance.GetTaskNoticeUI().SetText(taskInfo);
            SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Notice"), TaskManager.Instance.gameObject.transform.position);
        }
    }
}