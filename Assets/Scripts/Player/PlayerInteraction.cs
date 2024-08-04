using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float radiusInteracted;
    [SerializeField] private LayerMask whatIsInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckToInteract() && Input.GetKeyDown(KeyCode.F))
        {
            IInteracable interacable = GetObj()[0].gameObject.GetComponent<IInteracable>();
            if(interacable != null)
            {
                interacable.Interact();
            }
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
        //Gizmos.DrawWireSphere(transform.position, radiusInteracted);
    }
}
