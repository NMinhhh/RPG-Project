using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskKillBoss : TaskKillEnemy
{

    protected override void Start()
    {
        IntroManager.Instance.PlayCutSceen(introType);
    }

    protected override void Update()
    {
        if (CheckToChangeTask())
        {
            TaskManager.Instance.ChangeTask(taskNameNext);
            door.OpenDoor();
            gameObject.SetActive(false);
            
        }
    }
}
