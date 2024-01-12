using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    private CameraLook cam;
    public bool windowOpened;
    [HideInInspector] public InventoryManager inventoryManager;

    private void Start()
    {
        cam = GetComponentInChildren<CameraLook>();

        inventoryManager = GetComponentInChildren<InventoryManager>();
    }

    private void Update()
    {
        if (windowOpened)
        {
            cam.lockCursor = false;
            cam.canMove = false;
        }
        else
        {
            cam.lockCursor = true;
            cam.canMove = true;
        }

        if (inventoryManager.opened)
            windowOpened = true;
        else
            windowOpened = false;
    }
}
