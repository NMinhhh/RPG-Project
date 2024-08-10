using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [Header("Holder Transform")]
    [SerializeField] private Transform rightWeaponsHolder;
    [SerializeField] private Transform leftWeaponsHolder;
    [SerializeField] private Transform bowHolder;
    [SerializeField] private Transform arrowHolder;
    [Space]

    [Header("Base Weapon Data")]
    [SerializeField] private ItemData weaponData;
    [Space]

    [Header("Bow")]
    [SerializeField] private Bow bow;
    [Space]


    private EquippedWeapons currentLeftWeapons;
    private EquippedWeapons currentRightWeapons;

    PlayerAnimatorController playerAnimatorController;

    Player player;

    private bool isRangeAttack;

    public bool isEquippedWeapon {  get; private set; }
    public bool isEquippedBow { get; private set; }


    void Start()
    {
        player = GetComponent<Player>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        InventorySystem.Instance.equippedBow += EquippedBow;
        //isEquippedBow = true;
    }

    private void Update()
    {
        if (isRangeAttack)
        {
            leftWeaponsHolder.gameObject.SetActive(false);
            rightWeaponsHolder.gameObject.SetActive(false);
            bowHolder.gameObject.SetActive(true);
        }
        else
        {
            leftWeaponsHolder.gameObject.SetActive(true);
            rightWeaponsHolder.gameObject.SetActive(true);
            bowHolder.gameObject.SetActive(false);
        }
    }
    public void SetIsRangeAttack(bool state)
    {
        isRangeAttack = state;
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
        isEquippedWeapon = true;
    }

    public void EquippedLeftWeapons(ItemData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelLeftHand.GetComponent<EquippedWeapons>();
        currentLeftWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentLeftWeapons.InitializeWeapon(weaponData.damage, weaponData.criticalRate);
        currentLeftWeapons.transform.SetParent(leftWeaponsHolder.transform, false);

    }


    public void EquippedRightWeapons(ItemData weaponData)
    {
        EquippedWeapons weapons = weaponData.modelRightHand.GetComponent<EquippedWeapons>();
        currentRightWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentRightWeapons.InitializeWeapon(weaponData.damage, weaponData.criticalRate);
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

    public void StrongAttack()
    {
        if (currentLeftWeapons != null)
            currentLeftWeapons.StrongAttack();
        if (currentRightWeapons != null)
            currentRightWeapons.StrongAttack();
    }

    public void SlashEffectRight(int order)
    {
        if(currentRightWeapons != null)
            currentRightWeapons.TriggerEffect(order ,player.transform.position, player.transform.rotation);
    }
    
    public void SlashEffectLeft(int order)
    {
        if(currentLeftWeapons != null)
            currentLeftWeapons.TriggerEffect(order ,player.transform.position, player.transform.rotation);
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

    public void EndDealDamageAll()
    {
        if(currentLeftWeapons != null)
            EndDealDamageToTheLeft();
        if(currentRightWeapons != null)
            EndDealDamageToTheRight();
    }

    public void EquippedBow(bool state)
    {
        isEquippedBow = state;
    }

    public void StartArrowShoot()
    {
        arrowHolder.gameObject.SetActive(true);
    }

    public void EndArrowShoot()
    {
        arrowHolder.gameObject.SetActive(false);
    }

    public void BowStringPull()
    {
        bow.BowStringPull();
    }

    public void BowStringNotPull()
    {
        bow.BowStringNotPull();
    }
}
