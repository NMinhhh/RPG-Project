using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    public bool isClose {  get; private set; }

    void Start()
    {
        button.onClick.AddListener(() => CloseUI());
    }

    public void SetDialogInfo(string text)
    {
        isClose = false;
        this.text.text = "";
        this.text.text = text;
        InputManager.Instance.CanNotGetInput();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
        InputManager.Instance.CanGetInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isClose = true;
    }
}
