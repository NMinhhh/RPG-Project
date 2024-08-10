using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Animator[] doorAnims;
    private List<bool> doorOpen = new List<bool>();

    private void Start()
    {
        foreach(Animator door in doorAnims)
        {
            doorOpen.Add(door.GetBool("Open"));
        }
    }

    public virtual void OpenDoor()
    {
        foreach (Animator door in doorAnims)
        {
            door.SetBool("Open", true);
        }
    }

    public virtual void CloseDoor()
    {
        foreach (Animator door in doorAnims)
        {
            door.SetBool("Open", false);
        }
    }

    public void ResetDoor()
    {
        if (doorOpen.Count == 0) return;
        for (int i = 0; i < doorAnims.Length; i++)
        {
            doorAnims[i].SetBool("Open", doorOpen[i]);
        }
    }

}
