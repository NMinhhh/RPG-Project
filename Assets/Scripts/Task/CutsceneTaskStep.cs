using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTaskStep : TaskStep
{
    [SerializeField] private Intro.IntroName introName;
    private void Start()
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
}
