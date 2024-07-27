using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerTask : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    private Task currentTask;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TaskManager.Instance.TriggerTaskStep();
            gameObject.SetActive(false);
        }
    }
}
