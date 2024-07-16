using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{

    [SerializeField] private Transform rightWeaponsHolder;
    [SerializeField] private Transform leftWeaponsHolder;
    private EquippedWeapons currentLeftWeapons;
    private EquippedWeapons currentRightWeapons;
    PlayerAnimatorController playerAnimatorController;
    [SerializeField] private ItemData weaponData;


    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        EquippedWeapons(weaponData);
    }

    public void EquippedWeapons(ItemData itemData)
    {
        RemoveWeapons();
        if (itemData.isRightHand)
        {
            EquippedRightWeapons(itemData);
        }
        if (itemData.isLeftHand)
        {
            EquippedLeftWeapons(itemData);    
        }
        playerAnimatorController.SetAnimator(itemData.attackType);
        player.ChangeWeapon();
    }

    public void EquippedLeftWeapons(ItemData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelLeftHand.GetComponent<EquippedWeapons>();
        currentLeftWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentLeftWeapons.SetDamage(weaponData.damage);
        currentLeftWeapons.transform.SetParent(leftWeaponsHolder.transform, false);

    }


    public void EquippedRightWeapons(ItemData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelRightHand.GetComponent<EquippedWeapons>();
        currentRightWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentRightWeapons.SetDamage(weaponData.damage);
        currentRightWeapons.transform.SetParent(rightWeaponsHolder.transform, false);

    }

    void RemoveWeapons()
    {
        if (currentLeftWeapons != null)
        {
            DestroyImmediate(currentLeftWeapons.gameObject);
            currentLeftWeapons = null;
        }
        if (currentRightWeapons != null)
        {
            DestroyImmediate(currentRightWeapons.gameObject);
            currentRightWeapons = null;
        }
    }

    public void StartDealDamageToTheLeft()
    {
        currentLeftWeapons.StartDealDamage();
    }

    public void EndDealDamageToTheLeft()
    {
        currentLeftWeapons.EndDealDamage();
    }

    public void StartDealDamageToTheRight()
    {
        currentRightWeapons.StartDealDamage();
    }

    public void EndDealDamageToTheRight()
    {
        currentRightWeapons.EndDealDamage();
    }

}