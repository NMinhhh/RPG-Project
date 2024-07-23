using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskKillEnemy : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] protected Door door;

    [Header("Amount of enemy need kill")]
    [SerializeField] private GameObject[] enemys;
    
    [SerializeField] protected Intro.IntroType introType;
    [SerializeField] protected Task.TaskName taskNameNext;

    protected virtual void Start()
    {
        TaskManager.Instance.StartTask();
        door.CloseDoor();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CheckToChangeTask())
        {
            TaskManager.Instance.ChangeTask(taskNameNext);
            door.OpenDoor();
            gameObject.SetActive(false);
        }
    }

   

    protected virtual bool CheckToChangeTask()
    {
        int count = 0;
        foreach (GameObject enemy in enemys)
        {
            if (enemy.GetComponentInChildren<Enemy>().isDie)
            {
                count++;
            }
        }
        return enemys.Length == count;
    }
}
