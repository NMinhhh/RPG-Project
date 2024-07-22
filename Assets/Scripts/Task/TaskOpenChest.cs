using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOpenChest : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private GameObject door;
    [SerializeField] private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chest.isOpen && !boxCollider.enabled)
        {
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
