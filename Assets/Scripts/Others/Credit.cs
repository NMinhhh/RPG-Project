using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    [SerializeField] private TransitionEffect transitionEffect;
    [SerializeField] private float delayTime;
    [SerializeField] private float effectTime;
    [SerializeField] private float showTime;

    public void ShowCredit()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(delayTime);
        InputManager.Instance.CanNotGetUIInput();
        transitionEffect.StartTransition();
        yield return new WaitForSeconds(effectTime);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Credit);
        transitionEffect.StopTransition();
        yield return new WaitForSeconds(showTime);
        SaveSystem.DeleteFile(SaveManager.Instance.GetSaveName(SaveManager.SaveType.GameData));
        SceneManager.LoadScene(0);
    }
}
