using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float rotationZ;
    [SerializeField] private float rotateTime;

    private void Start()
    {
        Rotation();
    }

    void Rotation()
    {
        rotationZ = -rotationZ;
        LeanTween.rotate(this.gameObject, new Vector3(transform.localRotation.x, transform.localRotation.y, rotationZ), rotateTime).setOnComplete(Rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }
    }
}
