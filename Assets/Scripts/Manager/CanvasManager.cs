using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager> 
{
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private List<UIObject> uIObjects;
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        CursorUnLock();
        InputManager.Instance.CanNotGetInput();
        foreach (UIObject UIObj in uIObjects)
        {
            UIObj.obj.SetActive(false);
            if (UIObj.name == UIObject.UIName.MenuHome)
                UIObj.obj.SetActive(true);
        }
    }

    public void LoadUIGamePlay()
    {
        foreach (UIObject UIObj in uIObjects)
        {
            if (UIObj.isActive)
            {
                UIObj.obj.SetActive(true);
            }
            else
            {
                UIObj.obj.SetActive(false);
            }
        }
        CursorLock();
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void CursorUnLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenUI(UIObject.UIName name)
    {
        GetUIObject(name).obj.SetActive(true);
    }

    public void CloseUI(UIObject.UIName name)
    {
        GetUIObject(name).obj.SetActive(false);
    }

    public UIObject GetUIObject(UIObject.UIName name)
    {
        foreach (UIObject UIObj in uIObjects)
        {
            if(UIObj.name == name)
            {
                return UIObj;
            }
        }
        return null;
    }

    public void SetCanvasState(bool state)
    {
        canvasUI.SetActive(state);
    }
}

[System.Serializable]
public class UIObject
{
    public enum UIName
    {
        BossHealthBar,
        Inventory,
        Item3DViewer,
        CrossHair,
        QuickSlot,
        Soul,
        PlayerStatsBar,
        NoticePickUp,
        Dialog,
        MenuHome,
        MenuGamePlay,
        Settings,
    }

    public UIName name;
    public GameObject obj;
    public bool isActive;

}