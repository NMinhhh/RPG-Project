using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTaskStep : TaskStep, IResetable
{
    [SerializeField] private Intro.IntroName introName;

    public override void StartTask()
    {
        InputManager.Instance.CanNotGetInput();
        IntroManager.Instance.PlayCutScene(introName);
    }

    public void FinishCutscene()
    {
        IntroManager.Instance.FinishIntro();
        InputManager.Instance.CanGetInput();
        FinishTaskStep();
    }

    public void ResetBaseState()
    {
       
    }
}
