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

    Player player;
    void Start()
    {
        player = GetComponent<Player>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        EquippedRightWeapons("Hand");
        EquippedLeftWeapons("Hand");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquippedRightWeapons("Hand");
            EquippedLeftWeapons("Hand");
            playerAnimatorController.SetAnimator(TypeAnimator.AttackType.Hand);
            player.SetAmoutOfCombo(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquippedRightWeapons("Axe");
            playerAnimatorController.SetAnimator(TypeAnimator.AttackType.WeaponsInRightHand);
            player.SetAmoutOfCombo(3);

        }

    }

    public void SetAmountOfCombo(int combo)
    {
        player.SetAmoutOfCombo(combo);
    }

    public void EquippedLeftWeapons(string weaponsName)
    {
        if(currentLeftWeapons != null)
        {
            DestroyImmediate(currentLeftWeapons.gameObject);
            currentLeftWeapons = null;
        }
        EquippedWeapons weapons = Resources.Load<EquippedWeapons>(weaponsName);
        currentLeftWeapons = Instantiate(weapons, weapons.transform.localPosition, weapons.transform.rotation) as EquippedWeapons;
        currentLeftWeapons.transform.SetParent(leftWeaponsHolder.transform, false);

    }

    public void EquippedRightWeapons(string weaponsName)
    {
        if (currentRightWeapons != null)
        {
            DestroyImmediate(currentRightWeapons.gameObject);
            currentRightWeapons = null;
        }
        EquippedWeapons weapons = Resources.Load<EquippedWeapons>(weaponsName);
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
