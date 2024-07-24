using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPickUpBow : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] protected Door door;
    [Header("Bow Intruct UI")]
    [SerializeField] private GameObject bowIntructUI;

    [Header("Dummy")]
    [SerializeField] private Dummy[] dummys;
    [SerializeField] private Material material;
    [SerializeField] private float appearSpeed = 0.2f;
    private bool isAppear;
    private float appearAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAppear)
        {
            appearAmount = Mathf.Clamp01(appearAmount - appearSpeed * Time.deltaTime);
            material.SetFloat("_Dissolve", appearAmount);
            if (appearAmount == 0)
            {
                isAppear = false;
            }
        }
        if (CheckToChangeTask())
        {
            door.OpenDoor();
            gameObject.SetActive(false);
        }
    }

    public void StartAppear()
    {
        this.isAppear = true;
        appearAmount = 1;
    }

    public void BowIntructUIActive()
    {
        bowIntructUI.SetActive(true);
    }

    public bool CheckToChangeTask()
    {
        int count = 0;
        foreach (Dummy dummy in dummys)
        {
            if (dummy.isDie)
            {
                count++;
            }
        }
        return count == dummys.Length;
    }
}
