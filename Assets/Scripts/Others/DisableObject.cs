using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    private ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p.isStopped)
        {
            ObjectPool.Instance.AddInPool(Pool.Type.BloodParticle, this.gameObject);
        }
    }
}
