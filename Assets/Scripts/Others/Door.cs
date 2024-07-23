using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator[] doorAnims;
    public virtual void OpenDoor()
    {
        foreach (Animator anim in doorAnims)
        {
            anim.SetBool("Open", true);
        }
    }

    public virtual void CloseDoor()
    {
        foreach (Animator anim in doorAnims)
        {
            anim.SetBool("Open", false);
        }
    }
}
