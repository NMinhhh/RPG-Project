using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    [SerializeField] private float radiusInteracted;
    [SerializeField] private LayerMask whatIsInteract;
    [Header("Gizmos")]
    [SerializeField] private bool isDrawGizmos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckToInteract())
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                IInteracable interacable = GetObj()[0].gameObject.GetComponent<IInteracable>();
                if (interacable != null)
                {
                    interacable.Interact();
                }
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }

    public Collider[] GetObj()
    {
        return Physics.OverlapSphere(transform.position, radiusInteracted, whatIsInteract);
    }

    public bool CheckToInteract()
    {
        return Physics.CheckSphere(transform.position, radiusInteracted, whatIsInteract);
    }

    private void OnDrawGizmos()
    {
        if (isDrawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, radiusInteracted);
        }
    }
}
