using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticle : MonoBehaviour
{
    [SerializeField] private string objName;

    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isStopped)
        {
            if (transform.parent == null)
                ObjectPool.Instance.AddInPool(objName, this.gameObject);
            else
                ObjectPool.Instance.AddInPool(objName, this.transform.parent.gameObject);
        }
    }
}
