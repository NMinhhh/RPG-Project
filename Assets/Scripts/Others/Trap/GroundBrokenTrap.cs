using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBrokenTrap : MonoBehaviour
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

    void Broken()
    {
        gameObject.SetActive(false);
        SoundFXManager.Instance.PlaySound(SoundFXManager.Instance.GetSound("GroundBroken"), transform.position, 0.5f);
        ObjectPool.Instance.SpawnFromPool("GroundBroken", transform.position, transform.rotation);
    }
}
