using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private List<WeaponsObject> weaponsObjects;
    [SerializeField] private EquippedWeapons weapon;
    [SerializeField] private EquippedWeapons weapon2;
    [SerializeField] private Transform rightWeaponsHolder;
    [SerializeField] private Transform leftWeaponsHolder;
    private EquippedWeapons currentLeftWeapons;
    private EquippedWeapons currentRightWeapons;

    public WeaponsObject weaponsObject;
    Player player;
    void Start()
    {
        player = GetComponent<Player>();
        EquippedRightWeapons(weapon.gameObject);
        EquippedLeftWeapons(weapon.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && CheckWeaponObject(0))
        {
            player.ChangeWeapons(weaponsObjects[0]);
            EquippedRightWeapons(weapon.gameObject);
            EquippedLeftWeapons(weapon.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && CheckWeaponObject(1))
        {
            player.ChangeWeapons(weaponsObjects[1]);
            EquippedRightWeapons(weapon2.gameObject);
        }

    }

    bool CheckWeaponObject(int i)
    {
        if(weaponsObject == weaponsObjects[i])
        {
            return false;
        }
        return true;
    }

    public void EquippedLeftWeapons(GameObject weapon)
    {
        if(currentLeftWeapons != null)
        {
            DestroyImmediate(currentLeftWeapons.gameObject);
            currentLeftWeapons = null;
        }
        EquippedWeapons weapons = Resources.Load<EquippedWeapons>(weapon.name);
        currentLeftWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentLeftWeapons.transform.SetParent(leftWeaponsHolder.transform, false);

    }

    public void EquippedRightWeapons(GameObject weapon)
    {
        if (currentRightWeapons != null)
        {
            DestroyImmediate(currentRightWeapons.gameObject);
            currentRightWeapons = null;
        }
        EquippedWeapons weapons = Resources.Load<EquippedWeapons>(weapon.name);
        currentRightWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentRightWeapons.transform.SetParent(rightWeaponsHolder.transform, false);

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
