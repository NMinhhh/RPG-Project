using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameManager : Singleton<MenuGameManager>
{

    [SerializeField] private GameObject menuIdle;
    [SerializeField] private GameObject menuPlay;
    [SerializeField] private GameObject vcam;
    public bool isOpen {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        menuPlay.SetActive(false);
        isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.openMenu && !IntroManager.Instance.isPlayIntro)
        {
            if(!isOpen)
            {
                isOpen = true;
                CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGame);
                CanvasManager.Instance.CursorUnLock();
                InputManager.Instance.CanNotGetInput();
                Time.timeScale = 0;
            }
            else
            {
                isOpen = false;
                CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
                CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
                CanvasManager.Instance.CursorLock();
                InputManager.Instance.CanGetInput();
                Time.timeScale = 1;

            }
        }
    }

    public void PlayGame()
    {
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
        isOpen = false;
        vcam.SetActive(false);
        menuPlay.SetActive(true);
        menuIdle.SetActive(false);
        CanvasManager.Instance.LoadUIGamePlay();
        TaskManager.Instance.GetMainTask().InitializeTaskStep(0);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
        isOpen = false;
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
        CanvasManager.Instance.CursorLock();
        InputManager.Instance.CanGetInput();
    }

    public void OpenSettings()
    {
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Settings);
    }

    public void CloseSettings()
    {
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
        CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
    }

    public void ExitGameGamePlay()
    {
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
        menuPlay.SetActive(true);
        vcam.SetActive(true);
    }

    public void ExitGame()
    {
        SoundFXManager.instance.PlaySound(SoundFXManager.Instance.GetSound(Sound.SoundType.Click), transform.position, 0.5f);
    }
}
