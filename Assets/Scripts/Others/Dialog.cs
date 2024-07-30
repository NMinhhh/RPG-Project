using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    [SerializeField] private float duration;
    [SerializeField] private float delayButton;
    [SerializeField] private Image image;

    public bool isClose {  get; private set; }

    void Start()
    {
        button.onClick.AddListener(() => CloseUI());
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.localEulerAngles = new Vector3(0, 0, 45);
        button.transform.localScale = Vector3.zero;
        image.color = new Color(1, 1, 1, 0);
        LeanTween.scale(gameObject, Vector2.one, duration);
        LeanTween.rotate(gameObject, new Vector3(0,0,0), duration);
        LeanTween.value(0, 1, duration).setOnUpdate((value) =>
        {
            image.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), value);
        });
        LeanTween.scale(button.gameObject, Vector2.one, duration).setDelay(delayButton);
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
        LeanTween.scale(button.gameObject, Vector2.zero, duration);
        LeanTween.scale(gameObject, Vector2.zero, duration).setDelay(0).setOnComplete(FinishClose);
    }

    public void FinishClose()
    {
        gameObject.SetActive(false);
        InputManager.Instance.CanGetInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isClose = true;
    }
}