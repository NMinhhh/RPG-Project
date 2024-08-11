using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBrokenTrap : MonoBehaviour, IResetable
{
    [SerializeField] private float brokenDelayTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Invoke(nameof(Broken), brokenDelayTime);
        }
    }

    public void Broken()
    {
        gameObject.SetActive(false);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("GroundBroken"), transform.position);
        ObjectPool.Instance.SpawnFromPool("GroundBroken", transform.position, transform.rotation);
    }

    public void ResetBaseState()
    {
        GetComponent<Collider>().enabled = true;
        gameObject.SetActive(true);
    }
}
