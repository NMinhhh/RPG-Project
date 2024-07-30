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
        dialog.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (dialog.isClose)
        {
            FinishTaskStep();
        }
    }
}
