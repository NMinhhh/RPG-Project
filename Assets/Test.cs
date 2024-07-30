using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Dialog dialog;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            dialog.SetDialogInfo("Minh dep trai");
            dialog.gameObject.SetActive(true);
        }
    }

}
