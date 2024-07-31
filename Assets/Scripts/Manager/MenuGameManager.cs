using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameManager : Singleton<MenuGameManager>
{
    public bool isOpen {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            else
            {
                isOpen = false;
                CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
                CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
                CanvasManager.Instance.CursorLock();
                InputManager.Instance.CanGetInput();

            }
        }
    }

    public void OpenSettings()
    {
        CanvasManager.Instance.CloseUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Settings);
    }

    public void CloseSettings()
    {
        CanvasManager.Instance.OpenUI(UIObject.UIName.MenuGame);
        CanvasManager.Instance.CloseUI(UIObject.UIName.Settings);
    }
}
