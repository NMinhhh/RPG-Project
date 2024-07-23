using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField] private List<Task> taskList;

    private Task currentTask;
    private int currentTaskId;

    private void Start()
    {
        Initialize(Task.TaskName.OpenChest);
    }

    public void Initialize(Task.TaskName taskName)
    {
        currentTask = GetTask(taskName);
    }

    public void StartTask()
    {
        currentTask.isCompleted = false;
        currentTask.taskObj.SetActive(true);
    }

    public void ChangeTask(Task.TaskName taskName)
    {
        currentTask.isCompleted = true;
        currentTask.taskObj.SetActive(false);
        currentTask = GetTask(taskName);
    }



    public Task GetTask(Task.TaskName taskName)
    {
        foreach (Task task in taskList)
        {
            if (task.taskName == taskName)
            {
                return task;
            }
        }
        return null;
    }

}

[System.Serializable]
public class Task
{
    public enum TaskName
    {
        OpenChest,
        KillEnemy,
        KillOrk,
        KillEnemySpawn,
    }

    public TaskName taskName;
    public GameObject taskObj;
    public bool isCompleted;
}