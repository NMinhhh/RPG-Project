using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTemplateUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Text keyText;
    [SerializeField] private Text infoText;
    
    public void SetInfo(string keyText, string infoText, bool isBG)
    {
        this.keyText.text = keyText;
        this.infoText.text = infoText;
        if (!isBG)
            background.SetActive(false);
    }
}
