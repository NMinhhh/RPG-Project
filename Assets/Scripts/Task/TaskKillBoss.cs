using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskKillBoss : TaskKillEnemy
{
    protected override void Start()
    {
        base.Start();
        IntroManager.Instance.PlayCutSceen(Intro.IntroType.OrkIntro);
    }

    protected override void Update()
    {
        if (CheckToChangeTask())
        {
            CloseDoor();
            gameObject.SetActive(false);
            
        }
    }
}
