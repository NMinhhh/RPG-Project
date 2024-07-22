using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskKillEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private GameObject[] doors;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        TaskManager.Instance.StartTask();
        OpenDoor();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CheckToChangeTask())
        {
            TaskManager.Instance.ChangeTask(Task.TaskName.KillOrk);
            CloseDoor();
            gameObject.SetActive(false);
        }
    }

    public virtual void OpenDoor()
    {
        foreach (GameObject go in doors)
        {
            go.SetActive(true);
        }
    }

    public virtual void CloseDoor()
    {
        foreach (GameObject go in doors)
        {
            go.SetActive(false);
        }
    }

    protected virtual bool CheckToChangeTask()
    {
        int count = 0;
        foreach (GameObject enemy in enemys)
        {
            if (!enemy.activeInHierarchy)
            {
                count++;
            }
        }
        return enemys.Length == count;
    }
}
