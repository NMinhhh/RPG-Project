using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskNoticeUI : MonoBehaviour
{
    [SerializeField] private Text info;
    [SerializeField] private float appearEffectTime;
    [SerializeField] private float appearTime;
    public void SetText(string text)
    {
        info.text = text;
    }

    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, appearEffectTime);
        LeanTween.scale(gameObject, Vector3.zero, appearEffectTime).setDelay(appearTime).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


    private void OnDisable()
    {
        
    }
}
