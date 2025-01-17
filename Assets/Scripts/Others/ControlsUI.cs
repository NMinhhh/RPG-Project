using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [Header("Sensitivity Slider")]
    [SerializeField] private Slider sensetivitySlider;

    public static event Action<float> setSensetivity;

    [Header("Control")]
    [SerializeField] private GameObject controlTemplate;
    [SerializeField] private Transform templateHolder;
    [SerializeField] private List<ControlInfo> controlInfos;
    void Start()
    {
        GenerateControlUI();
        sensetivitySlider.value = SaveManager.Instance.GetSesetivity() / 100;
    }

    void GenerateControlUI()
    {
        for (int i = 0; i < controlInfos.Count; i++)
        {
            int idx = i;
            ControlInfo controlInfo = controlInfos[idx];
            ControlTemplateUI controlTemplateUI = Instantiate(controlTemplate, templateHolder).GetComponent<ControlTemplateUI>();
            controlTemplateUI.SetInfo(controlInfo.keyText, controlInfo.infoText, (idx % 2 == 0 ? true : false));
        }
    }

    public void SetSensitivity(float sensitivity)
    {
        sensetivitySlider.value = sensitivity;
        setSensetivity?.Invoke(sensitivity * 100);
        SaveManager.Instance.SaveSensetivity(sensitivity * 100);
    }
}

[System.Serializable]
public class ControlInfo
{
    public string keyText;
    public string infoText;
}
