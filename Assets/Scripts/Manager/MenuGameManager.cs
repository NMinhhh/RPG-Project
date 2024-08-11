using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIObject;

public class MenuGameManager : Singleton<MenuGameManager>
{
    [Header("Home")]
    [SerializeField] private GameObject menuIdle;
    [SerializeField] private GameObject menuPlay;
    [SerializeField] private GameObject vcam;
    [Header("Gameplay")]

    [SerializeField] private List<GameObject> settingObj;

    public bool isOpen {  get; private set; }

    public bool isPlaying { get; private set; }

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
        SceneManager.Instance.LoadScene(StartNewGame, true);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void StartNewGame()
    {
        isPlaying = true;
        isOpen = false;
        SaveManager.Instance.LoadGame();
        menuPlay.SetActive(true);
        menuIdle.SetActive(false);
        SetCameraHome(false);
        CanvasManager.Instance.LoadUIGamePlay();
        GameManager.Instance.RespawnPlayer();
        MenuGameManager.Instance.SetCameraHome(false);
    }

    public void OnButtonContinueHome()
    {
        SceneManager.Instance.LoadScene(ContinueHome, true);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    public void ContinueHome()
    {
        isOpen = false;
        isPlaying = true;
        Time.timeScale = 1;
        SetCameraHome(false);
        SaveManager.Instance.LoadGame();
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuHome);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
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
        Time.timeScale = 1;
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
        SceneManager.Instance.LoadScene(ExitGamePlay, false);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
    }

    void ExitGamePlay()
    {
        isPlaying = false;
        Time.timeScale = 1;
        CanvasManager.Instance.CursorUnLock();
        InputManager.Instance.CanNotGetInput();
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGamePlay);
        CanvasManager.Instance.OpenUI(UIObject.UIName.MenuHome);
        menuPlay.SetActive(true);
        SetCameraHome(true);
        TaskManager.Instance.GetMainTask().ResetMainTask();
        GameManager.Instance.RespawnPlayer();
        SaveManager.Instance.SaveAllData();
        InventorySystem.Instance.ResetInventory();
        EquipSystem.Instance.ResetEquip();
    }

    #endregion

    public void OpenSettings()
    {
        if (!isPlaying)
            CanvasManager.Instance.CloseUI(UIObject.UIName.MenuHome);
        else
            CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGamePlay);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Settings);
        Open("Audio");
    }

    public void CloseSettings()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("Click"), transform.position);
        if(!isPlaying)
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