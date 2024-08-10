using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] private float loadTime;
    [SerializeField] private float finishTransitionTime;
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
        yield return new WaitForSeconds(loadTime);
        action?.Invoke();
        yield return new WaitForSeconds(finishTransitionTime);
        transitionEffect.StopTransition();
        InputManager.Instance.CanGetUIInput();
        GameManager.Instance.SetGameState(false);
    }

    public void LoadScene(Action action)
    {
        StartCoroutine(Load(action));
    }

    IEnumerator Load(Action action)
    {
        InputManager.Instance.CanNotGetUIInput();
        transitionEffect.StartTransition();
        SaveManager.Instance.LoadGame();
        yield return new WaitForSeconds(loadTime);
        GameManager.Instance.RespawnPlayer();
        MenuGameManager.Instance.SetCameraHome(false);
        yield return new WaitForSeconds(finishTransitionTime);
        transitionEffect.StopTransition();
        action?.Invoke();
        InputManager.Instance.CanGetUIInput();
    }
}
