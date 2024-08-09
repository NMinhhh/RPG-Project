using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIObject;

public class MenuGameManager : Singleton<MenuGameManager>
{

    [SerializeField] private GameObject menuIdle;
    [SerializeField] private GameObject menuPlay;
    [SerializeField] private GameObject vcam;

    [SerializeField] private List<GameObject> settingObj;

    public bool isOpen {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.LoadFile(SaveManager.Instance.GetFileName()) != null)
        {
            menuIdle.SetActive(false);
            menuPlay.SetActive(true);
        }
        else
        {
            menuPlay.SetActive(false);
            menuIdle.SetActive(true);
        }
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
                if (!InventorySystem.Instance.isOpen)
                {
                    CanvasManager.Instance.CursorLock();
                    InputManager.Instance.CanGetInput();
                }
                Time.timeScale = 1;

            }
        }
    }

    public void SetCameraHome(bool state)
    {
        vcam.SetActive(state);
    }

    public void OnButtonStartNewGame()
    {
        SceneManager.Instance.LoadScene(StartNewGame);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void StartNewGame()
    {
        isOpen = false;
        menuPlay.SetActive(true);
        menuIdle.SetActive(false);
        CanvasManager.Instance.LoadUIGamePlay();
        TaskManager.Instance.Initialize(0);
        TaskManager.Instance.GetMainTask().InitializeTaskStep(0);
    }

    public void OnButtonContinue()
    {
        SceneManager.Instance.LoadScene(Continue);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        isOpen = false;
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
        CanvasManager.Instance.CursorLock();
        InputManager.Instance.CanGetInput();
        CanvasManager.Instance.LoadUIGamePlay();
        TaskManager.Instance.GetMainTask().InitializeTaskStep(0);
    }

    public void OpenSettings()
    {
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Settings);
        Open("Audio");
    }

    public void CloseSettings()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
    } 
    
    public void Open(string name)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        foreach(GameObject obj in settingObj)
            obj.SetActive(false);
        switch (name)
        {
            case "Audio":
                settingObj[0].SetActive(true);
                break;
            case "Controls":
                settingObj[1].SetActive(true);
                break;
        }
    }

    public void ExitGameGamePlay()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        menuPlay.SetActive(true);
        vcam.SetActive(true);
    }

    public void ExitGame()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }
}