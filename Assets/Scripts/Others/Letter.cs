using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    [SerializeField] private Button btn;
    private void Start()
    {
        btn.onClick.AddListener(() => CloseUI());
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
        InputManager.Instance.SetCanGetInput(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
