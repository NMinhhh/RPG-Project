using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] private float loadTime;
    [SerializeField] private float transisionTime;
    [SerializeField] private TransitionEffect transitionEffect;

    // Start is called before the first frame update
    void Start()
    {
        transitionEffect.StopTransition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneRespawn(Action action)
    {
        StartCoroutine(Respawn(action));
    }
    
    IEnumerator Respawn(Action action)
    {
        InputManager.Instance.CanNotGetUIInput();
        transitionEffect.StartTransition();
        yield return new WaitForSeconds(transisionTime);
        action?.Invoke();
        yield return new WaitForSeconds(loadTime);
        transitionEffect.StopTransition();
        yield return new WaitForSeconds(transisionTime);
        InputManager.Instance.CanGetUIInput();
        GameManager.Instance.SetGameState(false);
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
