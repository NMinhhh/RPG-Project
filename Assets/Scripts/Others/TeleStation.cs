using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleStation : MonoBehaviour,IDamageable
{
    [SerializeField] private ParticleSystem particleTele;
    [SerializeField] private ParticleSystem particleStation;
    [SerializeField] private Transform telePos;
    [SerializeField] private BoxCollider triggerColliderPos;

    private Transform player;

    public bool isTriggerTele;

    void Start()
    {
        triggerColliderPos.enabled = false;
        player = GameObject.Find("Player").transform;
    }

    public void Teleport()
    {
        player.transform.position = telePos.position;
    }

    public void TriggerParticleStation()
    {
        particleStation.gameObject.SetActive(true);
        triggerColliderPos.enabled = true;
    }

    public void StartEffect()
    {
        particleTele.Stop();
        particleTele.Play();
        particleTele.transform.position = player.position;
        particleTele.gameObject.SetActive(true);
    }

    public void FinishTele()
    {
        particleTele.gameObject.SetActive(false);
    }

    public void Damage(float damage)
    {
        Die();
    }

    public void Die()
    {
        isTriggerTele = true;
        triggerColliderPos.enabled = false;
    }

    public void RestTele()
    {
        particleStation.gameObject.SetActive(false);
        triggerColliderPos.enabled = false;
        isTriggerTele = false;
    }
   
}
