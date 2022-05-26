using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;
    [SerializeField] GameObject trails;


    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;
    GameObject weaponTrails;
    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        
    }



    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        weaponTrails = Instantiate(trails, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
        Destroy(weaponTrails);
    }


    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}