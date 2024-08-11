using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransisionManager : Singleton<TransisionManager>
{
    [SerializeField] private float loadTime;
    [SerializeField] private float transisionTime;
    [SerializeField] private TransitionEffect transitionEffect;

    // Start is called before the first frame update
    void Start()
    {
        transitionEffect.StopTransition();
    }

    public void LoadScene(Action action, bool isTaskInit)
    {
        Time.timeScale = 1;
        StartCoroutine(Load(action, isTaskInit));
    }

    IEnumerator Load(Action action, bool isTaskInit)
    {
        InputManager.Instance.CanNotGetUIInput();
        transitionEffect.StartTransition();
        yield return new WaitForSeconds(loadTime);
        action?.Invoke();
        yield return new WaitForSeconds(transisionTime);
        transitionEffect.StopTransition();
        if(MenuGameManager.Instance.isPlaying && !IntroManager.Instance.isPlayIntro)
            InputManager.Instance.CanGetUIInput();
        if (isTaskInit)
        {
            TaskManager.Instance.GetMainTask().InitializeTaskStep(0);
        }
    }
    


}
