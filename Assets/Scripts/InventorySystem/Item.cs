using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;
    public TypeAnimator.AttackType Animator;
    private bool _canPickup;
    public int Combo;

    private PlayerAnimatorController _playerAnimatorController;
    private WeaponsController _weaponsController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _canPickup && InventorySystem.Instance.CheckForAvailableSlot()) 
        {
            InventorySystem.Instance.AddToInventory("Axe");
            _playerAnimatorController.SetAnimator(Animator);
            _weaponsController.EquippedRightWeapons("Axe");
            _weaponsController.SetAmountOfCombo(Combo);
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
                this._weaponsController = weaponController;
            }
            if(playerAnimator != null)
            {
                this._playerAnimatorController = playerAnimator;
            }
            _canPickup = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _weaponsController = null;
            _playerAnimatorController = null;
            _canPickup = false;
        }
    }

}
