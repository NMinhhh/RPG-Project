using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOpenChest : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private BoxCollider boxCollider;
 
    private Door door;

    void Start()
    {
        boxCollider.enabled = false;
        door = GetComponent<Door>();
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
            door.OpenDoor();
            TaskManager.Instance.ChangeTask(Task.TaskName.KillEnemy);
        }
    }
}
