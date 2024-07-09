using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public TypeAnimator.AttackType animator;
    private bool canPickup;
    public int combo;

    private PlayerAnimatorController playerAnimatorController;
    private WeaponsController weaponsController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canPickup && InventorySystem.Instance.CheckForAvailableSlot()) 
        {
            InventorySystem.Instance.AddToInventory("Axe");
            playerAnimatorController.SetAnimator(animator);
            weaponsController.EquippedRightWeapons("Axe");
            weaponsController.SetAmountOfCombo(combo);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponsController weaponController = other.GetComponent<WeaponsController>();
            PlayerAnimatorController playerAnimator = other.GetComponent<PlayerAnimatorController>();
            if(weaponController != null)
            {
                this.weaponsController = weaponController;
            }
            if(playerAnimator != null)
            {
                this.playerAnimatorController = playerAnimator;
            }
            canPickup = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponsController = null;
            playerAnimatorController = null;
            canPickup = false;
        }
    }
}
