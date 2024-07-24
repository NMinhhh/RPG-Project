using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowIntruct : MonoBehaviour
{
    [SerializeField] private Button btn;
    private void Start()
    {
        btn.onClick.AddListener(() => CloseUI());
        InputManager.Instance.SetCanGetInput(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
        IntroManager.Instance.PlayCutSceen(Intro.IntroType.DummyIntro);
        InputManager.Instance.SetCanGetInput(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Is pick up bow!!!");

    }
}
