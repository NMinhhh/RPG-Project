using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBridgeFallTask : TaskStep
{
    [Header("Bridge")]
    [SerializeField] private Bridge bridge;

   

    // Start is called before the first frame update
    void Start()
    {
        bridge.StartAppear();
        bridge.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bridge.isFinishFall)
        {
            FinishTaskStep();
        }
    }

}
