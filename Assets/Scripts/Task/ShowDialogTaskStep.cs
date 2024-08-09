using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogTaskStep : TaskStep
{
    [SerializeField] private Dialog dialog;
    [TextArea(5,10)]
    [SerializeField] protected string textInfo;

    private void Start()
    {
        dialog.SetDialogInfo(textInfo);
        CanvasManager.Instance.OpenUI(UIObject.UIName.Dialog);
    }

    private void Update()
    {
        if (dialog.isClose)
        {
            FinishTaskStep();
        }
    }

   
}
