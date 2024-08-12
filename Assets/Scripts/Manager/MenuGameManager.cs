using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameManager : Singleton<MenuGameManager>
{
    [Header("Home")]
    [SerializeField] private GameObject menuIdle;
    [SerializeField] private GameObject menuPlay;
    [SerializeField] private GameObject vcam;
    [Header("Gameplay")]

    [SerializeField] private List<GameObject> settingObj;

    public bool isOpen {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.LoadFile(SaveManager.Instance.GetSaveName(SaveManager.SaveType.GameData)) != null)
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
                CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGamePlay);
                CanvasManager.Instance.CursorUnLock();
                InputManager.Instance.CanNotGetInput();
                Time.timeScale = 0;
            }
            else
            {
                isOpen = false;
                CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGamePlay);
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

    #region Home

    public void SetCameraHome(bool state)
    {
        vcam.SetActive(state);
    }

    public void OnButtonStartNewGame()
    {
        TransisionManager.Instance.LoadScene(StartNewGame, true);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.GamePlaying(true);
        isOpen = false;
        SetCameraHome(false);
        SaveManager.Instance.ResetGameData();
        SaveManager.Instance.LoadGame();
        GameManager.Instance.RespawnPlayer();
        CanvasManager.Instance.LoadUIGamePlay();
    }

    public void OnButtonContinueHome()
    {
        TransisionManager.Instance.LoadScene(ContinueHome, true);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void ContinueHome()
    {
        isOpen = false;
        GameManager.Instance.GamePlaying(true);
        Time.timeScale = 1;
        SetCameraHome(false);
        SaveManager.Instance.LoadGame();
        GameManager.Instance.RespawnPlayer();
        CanvasManager.Instance.LoadUIGamePlay();
    }

    public void ExitGame()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        Application.Quit();
    }

    #endregion


    #region Gameplay

    public void ContinueGamePlay()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        Time.timeScale = 1;
        isOpen = false;
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGamePlay);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
        if (!InventorySystem.Instance.isOpen)
        {
            CanvasManager.Instance.CursorLock();
            InputManager.Instance.CanGetInput();
        }
    }

    public void OnButtonExitGamePlay()
    {
        TransisionManager.Instance.LoadScene(ExitGamePlay, false);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    void ExitGamePlay()
    {
        SaveManager.Instance.SaveAllData();
        GameManager.Instance.GamePlaying(false);
        SceneManager.LoadScene(0);
    }

    #endregion

    public void OpenSettings()
    {
        if (!GameManager.Instance.isPlaying)
            CanvasManager.Instance.CloseUI(UIObject.UIName.MenuHome);
        else
            CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGamePlay);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Settings);
        Open("Audio");
    }

    public void CloseSettings()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        if(!GameManager.Instance.isPlaying)
            CanvasManager.Instance.OpenUI(UIObject.UIName.MenuHome);
        else
            CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGamePlay);
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

    
}