using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectInTask : MonoBehaviour
{
    [SerializeField] private List<GameObject> objs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObj()
    {
        foreach (GameObject obj in objs)
        {
            IResetable resetable = obj.GetComponent<IResetable>();
            if(resetable == null)
                resetable = obj.GetComponentInChildren<IResetable>();
            if (resetable != null)
            {
                resetable.ResetBaseState();
            }
        }
    }
}
