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



    public void DrawWeapon() // K�l�c� �ekti�imizde ger�ekle�ecek olan event.
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        weaponTrails = Instantiate(trails, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon() // K�l�c� geri koydu�umuzda ger�ekle�ecek olan event.
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
        Destroy(weaponTrails);
    }


    public void StartDealDamage() // DamageDealer scriptindeki fonksiyona event ile ula�t�k.
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage() // DamageDEaler scriptindeki fonksiyona event ile ula�t�k. Fonksiyonlar�n hepsini
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}